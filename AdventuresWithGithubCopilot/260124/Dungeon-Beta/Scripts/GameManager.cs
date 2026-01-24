using Godot;
using System;
using System.Collections.Generic;

public partial class GameManager : Node2D
{
    [Export]
    public int TotalLevels { get; set; } = 10;

    private Player _player;
    private Player _opponent;
    private int _currentLevel = 0;
    private bool _playerTurn = true;

    public override void _Ready()
    {
        GD.Print("GameManager ready — initializing players and decks.");
        // Create sample decks
        var playerDeck = CreateSampleDeck();
        var oppDeck = CreateSampleDeck();
        playerDeck.Shuffle();
        oppDeck.Shuffle();

        _player = new Player("Hero", playerDeck);
        _opponent = new Player("Enemy", oppDeck);

        // initial draw
        for (int i = 0; i < 5; i++)
        {
            _player.DrawCard();
            _opponent.DrawCard();
        }

        StartLevel(1);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventKey keyEvent && keyEvent.Pressed && keyEvent.Keycode == Key.Space)
        {
            EndPlayerTurn();
        }
    }

    private void StartLevel(int level)
    {
        _currentLevel = level;
        GD.Print($"Starting level {level}");
        _player.MaxMana = 0;
        _opponent.MaxMana = 0;
        _player.StartTurn();
        _playerTurn = true;
    }

    // Example: call this to progress one player turn then resolve
    public void EndPlayerTurn()
    {
        _playerTurn = false;
        // Opponent simple AI: plays first available cards into slots
        _opponent.StartTurn();
        for (int i = _opponent.Hand.Count - 1; i >= 0; i--)
        {
            for (int s = 0; s < 3; s++)
            {
                if (_opponent.Slots[s] == null && _opponent.Hand.Count > 0)
                {
                    if (_opponent.PlayCardToSlot(i, s))
                        break;
                }
            }
        }

        // resolve combat
        BattleResolver.ResolveCombat(_player, _opponent);

        // Check health and progress
        if (_player.Health <= 0)
        {
            GD.Print("Player defeated — restart level or end game.");
            return;
        }
        if (_opponent.Health <= 0)
        {
            GD.Print("Opponent defeated — level cleared.");
            _currentLevel++;
            if (_currentLevel > TotalLevels)
            {
                GD.Print("Final boss unlocked — (placeholder)");
            }
            else
            {
                StartLevel(_currentLevel);
            }
        }
        else
        {
            // start next player turn
            _player.StartTurn();
            _playerTurn = true;
        }
    }

    private Deck CreateSampleDeck()
    {
        var d = new Deck();
        // Simple placeholder: create 40 basic cards
        for (int i = 0; i < 40; i++)
        {
            if (i % 5 == 0)
                d.Add(new Card($"Sword {i}", CardType.Offensive, 1 + (i % 3), 2 + (i % 3), 1));
            else if (i % 5 == 1)
                d.Add(new Card($"Shield {i}", CardType.Defensive, 1 + (i % 2), 0, 2 + (i % 2)));
            else if (i % 5 == 2)
                d.Add(new Card($"Fireball {i}", CardType.Spell, 2 + (i % 3), 3 + (i % 2), 0));
            else if (i % 5 == 3)
                d.Add(new Card($"Potion {i}", CardType.Item, 1, 0, 0, "Heals 2"));
            else
                d.Add(new Card($"Axe {i}", CardType.Offensive, 2, 3, 1));
        }
        return d;
    }
}
