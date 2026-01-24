using Godot;
using DungeonCharlie.Cards;

namespace DungeonCharlie.UI
{
    /// <summary>
    /// Manages the visual display of card slots
    /// </summary>
    public partial class SlotManager : Control
    {
        [Export] public bool IsPlayerSlots { get; set; } = true;
        
        private Control[] _slots = new Control[Core.GameConstants.CARD_SLOTS];
        private Card[] _cards = new Card[Core.GameConstants.CARD_SLOTS];
        private int _selectedSlot = -1;

        [Signal]
        public delegate void SlotSelectedEventHandler(int slotIndex);

        public override void _Ready()
        {
            // Get slot references - they're inside HBoxContainer
            var container = GetNodeOrNull<HBoxContainer>("HBoxContainer");
            if (container == null)
            {
                GD.PrintErr("HBoxContainer not found in SlotManager!");
                return;
            }

            for (int i = 0; i < Core.GameConstants.CARD_SLOTS; i++)
            {
                _slots[i] = container.GetNodeOrNull<Control>($"Slot{i}");
                if (_slots[i] == null)
                {
                    GD.PrintErr($"Slot{i} not found!");
                }
                else
                {
                    // Add click handler to slot
                    int slotIndex = i; // Capture for lambda
                    var slotArea = _slots[i].GetNodeOrNull<Area2D>("Area2D");
                    if (slotArea != null)
                    {
                        slotArea.InputEvent += (viewport, @event, shapeIdx) => OnSlotClicked(slotIndex, @event);
                    }
                }
            }
        }

        /// <summary>
        /// Place a card in a slot
        /// </summary>
        public bool PlaceCard(Card card, int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= _slots.Length)
            {
                GD.PrintErr($"Invalid slot index: {slotIndex}");
                return false;
            }

            if (_cards[slotIndex] != null)
            {
                GD.Print($"Slot {slotIndex} is already occupied");
                return false;
            }

            // Remove card from its current parent
            if (card.GetParent() != null)
            {
                card.GetParent().RemoveChild(card);
            }

            // Add card to slot
            _slots[slotIndex].AddChild(card);
            _cards[slotIndex] = card;
            
            // Position card in center of slot
            card.Position = new Vector2(60, 90); // Center of 120x180 slot

            GD.Print($"Placed {card.Data.CardName} in slot {slotIndex}");
            return true;
        }

        /// <summary>
        /// Remove a card from a slot
        /// </summary>
        public void RemoveCard(int slotIndex)
        {
            if (slotIndex >= 0 && slotIndex < _cards.Length && _cards[slotIndex] != null)
            {
                _cards[slotIndex].QueueFree();
                _cards[slotIndex] = null;
            }
        }

        /// <summary>
        /// Clear all slots
        /// </summary>
        public void ClearAllSlots()
        {
            for (int i = 0; i < _cards.Length; i++)
            {
                if (_cards[i] != null)
                {
                    _cards[i].QueueFree();
                    _cards[i] = null;
                }
            }
        }

        /// <summary>
        /// Get card in slot
        /// </summary>
        public Card GetCard(int slotIndex)
        {
            if (slotIndex >= 0 && slotIndex < _cards.Length)
                return _cards[slotIndex];
            return null;
        }

        /// <summary>
        /// Check if slot is empty
        /// </summary>
        public bool IsSlotEmpty(int slotIndex)
        {
            if (slotIndex >= 0 && slotIndex < _cards.Length)
                return _cards[slotIndex] == null;
            return false;
        }

        /// <summary>
        /// Handle slot click
        /// </summary>
        private void OnSlotClicked(int slotIndex, InputEvent @event)
        {
            if (@event is InputEventMouseButton mouseButton && mouseButton.Pressed && mouseButton.ButtonIndex == MouseButton.Left)
            {
                GD.Print($"Slot {slotIndex} clicked");
                EmitSignal(SignalName.SlotSelected, slotIndex);
            }
        }

        /// <summary>
        /// Highlight slot for selection
        /// </summary>
        public void HighlightSlot(int slotIndex, bool highlight)
        {
            if (slotIndex >= 0 && slotIndex < _slots.Length && _slots[slotIndex] != null)
            {
                var colorRect = _slots[slotIndex].GetNodeOrNull<ColorRect>("Background");
                if (colorRect != null)
                {
                    colorRect.Color = highlight ? new Color(0.5f, 1.0f, 0.5f, 0.5f) : new Color(0.3f, 0.3f, 0.3f, 0.5f);
                }
            }
        }
    }
}
