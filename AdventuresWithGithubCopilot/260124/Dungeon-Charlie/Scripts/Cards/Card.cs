using Godot;
using System;

namespace DungeonCharlie.Cards
{
    /// <summary>
    /// Represents an instance of a card that can be played in the game
    /// </summary>
    public partial class Card : Node2D
    {
        public CardData Data { get; private set; }
        public Core.PlayerType Owner { get; set; }
        public bool IsPlayable { get; private set; }

        // UI references (will be set in the scene)
        private Sprite2D _cardSprite;
        private Label _nameLabel;
        private Label _costLabel;
        private Label _attackLabel;
        private Label _defenseLabel;
        private Label _descriptionLabel;

        public override void _Ready()
        {
            // Get UI element references
            _cardSprite = GetNodeOrNull<Sprite2D>("CardSprite");
            _nameLabel = GetNodeOrNull<Label>("NameLabel");
            _costLabel = GetNodeOrNull<Label>("CostLabel");
            _attackLabel = GetNodeOrNull<Label>("AttackLabel");
            _defenseLabel = GetNodeOrNull<Label>("DefenseLabel");
            _descriptionLabel = GetNodeOrNull<Label>("DescriptionLabel");
        }

        /// <summary>
        /// Initialize this card instance with card data
        /// </summary>
        public void Initialize(CardData cardData, Core.PlayerType owner)
        {
            Data = cardData;
            Owner = owner;
            UpdateCardVisuals();
        }

        /// <summary>
        /// Update the card's visual representation
        /// </summary>
        public void UpdateCardVisuals()
        {
            if (Data == null) return;

            if (_nameLabel != null) _nameLabel.Text = Data.CardName;
            if (_costLabel != null) _costLabel.Text = Data.ManaCost.ToString();
            if (_attackLabel != null) _attackLabel.Text = Data.AttackPower.ToString();
            if (_defenseLabel != null) _defenseLabel.Text = Data.DefensePower.ToString();
            if (_descriptionLabel != null) _descriptionLabel.Text = Data.Description;
            if (_cardSprite != null && Data.CardArt != null) _cardSprite.Texture = Data.CardArt;
        }

        /// <summary>
        /// Check if this card can be played with the given mana
        /// </summary>
        public void UpdatePlayableState(int availableMana)
        {
            IsPlayable = Data != null && Data.ManaCost <= availableMana;
            UpdateCardAppearance();
        }

        /// <summary>
        /// Update card appearance based on playable state
        /// </summary>
        private void UpdateCardAppearance()
        {
            if (_cardSprite == null) return;

            // Dim the card if not playable
            _cardSprite.Modulate = IsPlayable ? new Color(1, 1, 1, 1) : new Color(0.5f, 0.5f, 0.5f, 1);
        }

        /// <summary>
        /// Play this card (will be handled by the game manager)
        /// </summary>
        public void Play()
        {
            if (!IsPlayable) return;
            
            GD.Print($"Playing card: {Data.CardName}");
            EmitSignal(SignalName.CardPlayed, this);
        }

        [Signal]
        public delegate void CardPlayedEventHandler(Card card);

        [Signal]
        public delegate void CardHoveredEventHandler(Card card);

        [Signal]
        public delegate void CardUnhoveredEventHandler();
    }
}
