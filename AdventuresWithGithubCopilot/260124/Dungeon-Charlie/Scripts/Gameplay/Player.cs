using Godot;
using System.Collections.Generic;
using DungeonCharlie.Cards;

namespace DungeonCharlie.Gameplay
{
    /// <summary>
    /// Represents a player in the game (human or AI)
    /// </summary>
    public partial class Player : Node
    {
        [Export] public Core.PlayerType PlayerType { get; set; }
        
        public int Health { get; private set; }
        public int MaxHealth { get; private set; }
        public int CurrentMana { get; private set; }
        public int MaxMana { get; private set; }

        public Deck Deck { get; private set; }
        public List<Card> Hand { get; private set; } = new List<Card>();
        public Card[] CardSlots { get; private set; } = new Card[Core.GameConstants.CARD_SLOTS];

        public bool HasEndedTurn { get; set; }

        [Signal]
        public delegate void HealthChangedEventHandler(int newHealth);

        [Signal]
        public delegate void ManaChangedEventHandler(int currentMana, int maxMana);

        [Signal]
        public delegate void CardDrawnEventHandler(CardData card);

        [Signal]
        public delegate void CardPlayedEventHandler(Card card, int slotIndex);

        public override void _Ready()
        {
            Deck = GetNode<Deck>("Deck");
        }

        /// <summary>
        /// Initialize the player for a new game
        /// </summary>
        public void Initialize(List<CardData> deckCards, bool isBoss = false)
        {
            MaxHealth = Core.GameConstants.STARTING_HEALTH;
            if (isBoss)
            {
                MaxHealth = (int)(MaxHealth * Core.GameConstants.BOSS_HEALTH_MULTIPLIER);
            }

            Health = MaxHealth;
            MaxMana = Core.GameConstants.STARTING_MANA;
            CurrentMana = MaxMana;

            Hand.Clear();
            for (int i = 0; i < CardSlots.Length; i++)
            {
                CardSlots[i] = null;
            }

            if (Deck != null)
            {
                Deck.Initialize(deckCards);
            }

            HasEndedTurn = false;
        }

        /// <summary>
        /// Draw the starting hand
        /// </summary>
        public void DrawStartingHand()
        {
            for (int i = 0; i < Core.GameConstants.STARTING_HAND_SIZE; i++)
            {
                DrawCard();
            }
        }

        /// <summary>
        /// Draw a card from the deck
        /// </summary>
        public CardData DrawCard()
        {
            if (Hand.Count >= Core.GameConstants.MAX_HAND_SIZE)
            {
                GD.Print($"{PlayerType}: Hand is full, cannot draw.");
                return null;
            }

            var cardData = Deck?.DrawCard();
            if (cardData != null)
            {
                // Create a Card instance
                var cardScene = GD.Load<PackedScene>("res://Scenes/Card.tscn");
                if (cardScene != null)
                {
                    var card = cardScene.Instantiate<Card>();
                    card.Initialize(cardData, PlayerType);
                    Hand.Add(card);
                    EmitSignal(SignalName.CardDrawn, cardData);
                    GD.Print($"{PlayerType} drew: {cardData.CardName}");
                }
            }

            return cardData;
        }

        /// <summary>
        /// Start a new turn
        /// </summary>
        public void StartTurn()
        {
            HasEndedTurn = false;
            
            // Increase max mana
            MaxMana = Mathf.Min(MaxMana + Core.GameConstants.MANA_INCREMENT, 10);
            CurrentMana = MaxMana;
            EmitSignal(SignalName.ManaChanged, CurrentMana, MaxMana);

            // Draw a card
            DrawCard();

            GD.Print($"{PlayerType} turn started. Mana: {CurrentMana}/{MaxMana}");
        }

        /// <summary>
        /// Play a card from hand to a slot
        /// </summary>
        public bool PlayCard(Card card, int slotIndex)
        {
            if (!Hand.Contains(card))
            {
                GD.Print($"Card not in hand: {card.Data.CardName}");
                return false;
            }

            if (slotIndex < 0 || slotIndex >= CardSlots.Length)
            {
                GD.Print($"Invalid slot index: {slotIndex}");
                return false;
            }

            if (CardSlots[slotIndex] != null)
            {
                GD.Print($"Slot {slotIndex} is already occupied");
                return false;
            }

            if (CurrentMana < card.Data.ManaCost)
            {
                GD.Print($"Not enough mana to play {card.Data.CardName}");
                return false;
            }

            // Play the card
            Hand.Remove(card);
            CardSlots[slotIndex] = card;
            CurrentMana -= card.Data.ManaCost;

            EmitSignal(SignalName.ManaChanged, CurrentMana, MaxMana);
            EmitSignal(SignalName.CardPlayed, card, slotIndex);

            GD.Print($"{PlayerType} played {card.Data.CardName} in slot {slotIndex}. Mana: {CurrentMana}/{MaxMana}");
            return true;
        }

        /// <summary>
        /// Take damage
        /// </summary>
        public void TakeDamage(int damage)
        {
            Health = Mathf.Max(0, Health - damage);
            EmitSignal(SignalName.HealthChanged, Health);
            GD.Print($"{PlayerType} took {damage} damage. Health: {Health}/{MaxHealth}");
        }

        /// <summary>
        /// Heal health
        /// </summary>
        public void Heal(int amount)
        {
            Health = Mathf.Min(MaxHealth, Health + amount);
            EmitSignal(SignalName.HealthChanged, Health);
            GD.Print($"{PlayerType} healed {amount}. Health: {Health}/{MaxHealth}");
        }

        /// <summary>
        /// Check if the player is defeated
        /// </summary>
        public bool IsDefeated()
        {
            return Health <= 0;
        }

        /// <summary>
        /// End the turn
        /// </summary>
        public void EndTurn()
        {
            HasEndedTurn = true;
            GD.Print($"{PlayerType} ended their turn.");
        }

        /// <summary>
        /// Clear all cards from slots (after combat)
        /// </summary>
        public void ClearSlots()
        {
            for (int i = 0; i < CardSlots.Length; i++)
            {
                if (CardSlots[i] != null)
                {
                    // Discard the card
                    Deck?.Discard(CardSlots[i].Data);
                    CardSlots[i].QueueFree();
                    CardSlots[i] = null;
                }
            }
        }
    }
}
