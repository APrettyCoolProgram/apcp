using Godot;
using System.Collections.Generic;
using System;

public class GameController : Node2D
{
    private Deck playerDeck;
    private Deck enemyDeck;
    private Hand playerHand;
    private Hand enemyHand;
    private Slot[] playerSlots = new Slot[3];
    private Slot[] enemySlots = new Slot[3];
    private TurnManager turnManager;
    private UIController ui;
    private int playerHealth = 10;
    private int enemyHealth = 10;

    public override void _Ready()
    {
        turnManager = new TurnManager();
        AddChild(turnManager);

        // Load card templates from Data/cards.json
        var templates = CardLoader.LoadAll("res://Data/cards.json");
        if (templates.Count == 0)
        {
            GD.Print("No card templates found; using fallback sample.");
            templates = new List<Card>
            {
                new Card("c_sword","Iron Sword",CardType.Offensive,1,attack:2),
                new Card("c_shield","Leather Shield",CardType.Defensive,1,attack:0,defense:2),
                new Card("c_fire","Fireball",CardType.Spell,2,attack:3),
                new Card("c_potion","Minor Potion",CardType.Item,1,attack:0,defense:1)
            };
        }

        // Build decks (40 cards each) by repeating templates
        var pCards = new List<Card>();
        var eCards = new List<Card>();
        for (int i = 0; i < 40; i++) pCards.Add(CloneCard(templates[i % templates.Count]));
        for (int i = 0; i < 40; i++) eCards.Add(CloneCard(templates[(i + 1) % templates.Count]));

        playerDeck = new Deck(pCards);
        enemyDeck = new Deck(eCards);
        playerDeck.Shuffle();
        enemyDeck.Shuffle();

        playerHand = new Hand();
        enemyHand = new Hand();

        for (int i = 0; i < 3; i++) { playerSlots[i] = new Slot(); enemySlots[i] = new Slot(); }

        for (int i = 0; i < 5; i++) { playerHand.Add(playerDeck.Draw()); enemyHand.Add(enemyDeck.Draw()); }

        // instantiate UI and wire
        var uiScene = (PackedScene)ResourceLoader.Load("res://Scenes/UI.tscn");
        var uiNode = uiScene.Instantiate();
        AddChild(uiNode);
        ui = uiNode as UIController;
        ui.RefreshHand(GetPlayerHand());
        ui.UpdateHealth(playerHealth, enemyHealth);

        GD.Print($"Player hand count: {playerHand.Count}, deck: {playerDeck.Count}");
        GD.Print($"Enemy hand count: {enemyHand.Count}, deck: {enemyDeck.Count}");

        // Start first player turn
        StartPlayerTurn();
    }

    private Card CloneCard(Card template)
    {
        return new Card(template.Id, template.Name, template.Type, template.Cost, template.Attack, template.Defense, template.EffectId);
    }

    public List<Card> GetPlayerHand() => playerHand.Cards;
    public int GetPlayerMana() => turnManager.PlayerMana;
    public int GetOpponentMana() => turnManager.OpponentMana;
    public int GetPlayerHealth() => playerHealth;
    public int GetOpponentHealth() => enemyHealth;

    public bool PlayCardFromHandToSlot(int handIndex, int slotIndex)
    {
        if (handIndex < 0 || handIndex >= playerHand.Count) return false;
        if (slotIndex < 0 || slotIndex >= playerSlots.Length) return false;
        var card = playerHand.Cards[handIndex];
        // mana check
        if (card.Cost > turnManager.PlayerMana)
        {
            GD.Print("Not enough mana to play this card.");
            return false;
        }
        if (!playerSlots[slotIndex].IsEmpty)
        {
            GD.Print("Slot is occupied.");
            return false;
        }
        // Pay mana and place
        turnManager.PlayerMana -= card.Cost;
        playerHand.RemoveAt(handIndex);
        playerSlots[slotIndex].Place(card);
        GD.Print($"Played {card.Name} to slot {slotIndex}. Remaining mana: {turnManager.PlayerMana}");
        ui.RefreshHand(GetPlayerHand());
        ui.UpdateMana(turnManager.PlayerMana, turnManager.OpponentMana);
        return true;
    }

    public void StartPlayerTurn()
    {
        // notify turnManager; draw 1 card
        turnManager.StartTurn(true);
        var drawn = playerDeck.Draw();
        if (drawn != null) playerHand.Add(drawn);
        ui.RefreshHand(GetPlayerHand());
        ui.UpdateMana(turnManager.PlayerMana, turnManager.OpponentMana);
        GD.Print($"Start Player Turn {turnManager.TurnNumber} - Mana: {turnManager.PlayerMana} - Hand: {playerHand.Count}");
    }

    public void EndPlayerTurn()
    {
        // start enemy turn: set enemy mana and draw
        turnManager.StartTurn(false);
        var edrawn = enemyDeck.Draw();
        if (edrawn != null) enemyHand.Add(edrawn);
        ui.UpdateMana(turnManager.PlayerMana, turnManager.OpponentMana);

        // simple AI plays then resolve combat
        AIPlay();
        bool gameOver = ResolveCombat();
        if (gameOver)
        {
            // game over handled in ResolveCombat/ShowEndGame
            return;
        }

        // end enemy turn and advance to next player turn
        turnManager.EndTurn();
        StartPlayerTurn();
    }

    private void AIPlay()
    {
        // simple rule: play first playable card to a random empty slot
        for (int i = 0; i < enemyHand.Count; i++)
        {
            var c = enemyHand.Cards[i];
            if (c.Cost <= turnManager.OpponentMana)
            {
                int slot = -1;
                for (int s = 0; s < enemySlots.Length; s++) if (enemySlots[s].IsEmpty) { slot = s; break; }
                if (slot >= 0)
                {
                    turnManager.OpponentMana -= c.Cost;
                    enemyHand.RemoveAt(i);
                    enemySlots[slot].Place(c);
                    GD.Print($"Enemy played {c.Name} to slot {slot}");
                    break;
                }
            }
        }
    }

    // Returns true if the game ended as a result of combat
    public bool ResolveCombat()
    {
        for (int i = 0; i < 3; i++)
        {
            var p = playerSlots[i].OccupiedCard;
            var e = enemySlots[i].OccupiedCard;
            if (p != null && e != null)
            {
                e.Defense -= p.Attack;
                p.Defense -= e.Attack;
                GD.Print($"Slot {i}: {p.Name} vs {e.Name} -> pDEF:{p.Defense}, eDEF:{e.Defense}");
                if (p.Defense <= 0) playerSlots[i].Clear();
                if (e.Defense <= 0) enemySlots[i].Clear();
            }
            else if (p != null && e == null)
            {
                enemyHealth -= p.Attack;
                GD.Print($"Slot {i}: {p.Name} hits enemy for {p.Attack} -> enemyHealth:{enemyHealth}");
            }
            else if (e != null && p == null)
            {
                playerHealth -= e.Attack;
                GD.Print($"Slot {i}: {e.Name} hits player for {e.Attack} -> playerHealth:{playerHealth}");
            }
        }
        // update UI health after combat
        ui.UpdateHealth(playerHealth, enemyHealth);

        // win/lose detection
        if (enemyHealth <= 0 && playerHealth <= 0)
        {
            ui.ShowEndGame("Draw! Both combatants fell.");
            return true;
        }
        else if (enemyHealth <= 0)
        {
            ui.ShowEndGame("You Win!");
            return true;
        }
        else if (playerHealth <= 0)
        {
            ui.ShowEndGame("You Lose!");
            return true;
        }

        return false;
    }
}

