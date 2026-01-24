using Godot;
using System.Collections.Generic;
using DungeonCharlie.Cards;

namespace DungeonCharlie.UI
{
    /// <summary>
    /// Manages the visual display of cards in a player's hand
    /// </summary>
    public partial class HandManager : Control
    {
        [Export] public bool IsPlayerHand { get; set; } = true;
        
        private HBoxContainer _handContainer;
        private List<Card> _visualCards = new List<Card>();
        private int _selectedSlot = -1;

        [Signal]
        public delegate void CardSelectedEventHandler(Card card);

        public override void _Ready()
        {
            _handContainer = GetNode<HBoxContainer>("HandContainer");
        }

        /// <summary>
        /// Add a card to the visual hand display
        /// </summary>
        public void AddCard(CardData cardData, Gameplay.Player owner)
        {
            var cardScene = GD.Load<PackedScene>("res://Scenes/Card.tscn");
            if (cardScene == null)
            {
                GD.PrintErr("Could not load Card.tscn");
                return;
            }

            var card = cardScene.Instantiate<Card>();
            card.Initialize(cardData, owner.PlayerType);
            
            // Add click handler
            var area = card.GetNodeOrNull<Area2D>("Area2D");
            if (area != null)
            {
                area.InputEvent += (viewport, @event, shapeIdx) => OnCardClicked(card, @event);
            }

            _handContainer.AddChild(card);
            _visualCards.Add(card);
            
            GD.Print($"Added {cardData.CardName} to hand display");
        }

        /// <summary>
        /// Remove a card from the hand
        /// </summary>
        public void RemoveCard(Card card)
        {
            if (_visualCards.Contains(card))
            {
                _visualCards.Remove(card);
                card.QueueFree();
            }
        }

        /// <summary>
        /// Clear all cards from the hand
        /// </summary>
        public void ClearHand()
        {
            foreach (var card in _visualCards)
            {
                card.QueueFree();
            }
            _visualCards.Clear();
        }

        /// <summary>
        /// Update playable state for all cards based on available mana
        /// </summary>
        public void UpdatePlayableCards(int availableMana)
        {
            foreach (var card in _visualCards)
            {
                card.UpdatePlayableState(availableMana);
            }
        }

        /// <summary>
        /// Handle card click
        /// </summary>
        private void OnCardClicked(Card card, InputEvent @event)
        {
            if (@event is InputEventMouseButton mouseButton && mouseButton.Pressed && mouseButton.ButtonIndex == MouseButton.Left)
            {
                if (card.IsPlayable)
                {
                    GD.Print($"Card clicked: {card.Data.CardName}");
                    EmitSignal(SignalName.CardSelected, card);
                }
                else
                {
                    GD.Print($"Card not playable: {card.Data.CardName} (needs {card.Data.ManaCost} mana)");
                }
            }
        }

        /// <summary>
        /// Get card at index
        /// </summary>
        public Card GetCard(int index)
        {
            if (index >= 0 && index < _visualCards.Count)
                return _visualCards[index];
            return null;
        }

        /// <summary>
        /// Get all cards
        /// </summary>
        public List<Card> GetAllCards()
        {
            return new List<Card>(_visualCards);
        }
    }
}
