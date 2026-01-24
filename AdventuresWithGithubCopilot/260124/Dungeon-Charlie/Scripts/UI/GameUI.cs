using Godot;
using DungeonCharlie.Cards;

namespace DungeonCharlie.UI
{
    /// <summary>
    /// Controls the main game UI
    /// </summary>
    public partial class GameUI : Control
    {
        // Player UI elements
        private Label _playerHealthLabel;
        private Label _playerManaLabel;
        private Label _turnIndicatorLabel;
        private Button _endTurnButton;

        // Opponent UI elements
        private Label _opponentHealthLabel;
        private Label _opponentManaLabel;

        // Game info
        private Label _levelLabel;
        private Label _phaseLabel;

        // Hand and slot managers
        private HandManager _playerHand;
        private HandManager _opponentHand;
        private SlotManager _playerSlots;
        private SlotManager _opponentSlots;

        private Gameplay.GameManager _gameManager;
        private Card _selectedCard = null;

        public override void _Ready()
        {
            // Get UI references
            _playerHealthLabel = GetNodeOrNull<Label>("%PlayerHealthLabel");
            _playerManaLabel = GetNodeOrNull<Label>("%PlayerManaLabel");
            _opponentHealthLabel = GetNodeOrNull<Label>("%OpponentHealthLabel");
            _opponentManaLabel = GetNodeOrNull<Label>("%OpponentManaLabel");
            _turnIndicatorLabel = GetNodeOrNull<Label>("%TurnIndicatorLabel");
            _endTurnButton = GetNodeOrNull<Button>("%EndTurnButton");
            _levelLabel = GetNodeOrNull<Label>("%LevelLabel");
            _phaseLabel = GetNodeOrNull<Label>("%PhaseLabel");

            // Get hand and slot managers
            _playerHand = GetNodeOrNull<HandManager>("%PlayerHand");
            _opponentHand = GetNodeOrNull<HandManager>("%OpponentHand");
            _playerSlots = GetNodeOrNull<SlotManager>("%PlayerSlots");
            _opponentSlots = GetNodeOrNull<SlotManager>("%OpponentSlots");

            // Connect signals
            if (_playerHand != null)
            {
                _playerHand.CardSelected += OnCardSelected;
            }

            if (_playerSlots != null)
            {
                _playerSlots.SlotSelected += OnSlotSelected;
            }

            // Connect button
            if (_endTurnButton != null)
            {
                _endTurnButton.Pressed += OnEndTurnPressed;
            }

            // Get game manager
            _gameManager = GetNodeOrNull<Gameplay.GameManager>("../GameManager");
            if (_gameManager != null)
            {
                _gameManager.GameStateChanged += OnGameStateChanged;
                _gameManager.TurnPhaseChanged += OnTurnPhaseChanged;
                _gameManager.TurnStarted += OnTurnStarted;

                if (_gameManager.Player != null)
                {
                    _gameManager.Player.HealthChanged += OnPlayerHealthChanged;
                    _gameManager.Player.ManaChanged += OnPlayerManaChanged;
                    _gameManager.Player.CardDrawn += OnPlayerCardDrawn;
                }

                if (_gameManager.Opponent != null)
                {
                    _gameManager.Opponent.HealthChanged += OnOpponentHealthChanged;
                    _gameManager.Opponent.ManaChanged += OnOpponentManaChanged;
                    _gameManager.Opponent.CardDrawn += OnOpponentCardDrawn;
                }
            }
            else
            {
                GD.PrintErr("GameUI: Could not find GameManager");
            }
        }

        private void OnPlayerHealthChanged(int newHealth)
        {
            if (_playerHealthLabel != null)
                _playerHealthLabel.Text = $"Health: {newHealth}";
        }

        private void OnPlayerManaChanged(int currentMana, int maxMana)
        {
            if (_playerManaLabel != null)
                _playerManaLabel.Text = $"Mana: {currentMana}/{maxMana}";
        }

        private void OnOpponentHealthChanged(int newHealth)
        {
            if (_opponentHealthLabel != null)
                _opponentHealthLabel.Text = $"Health: {newHealth}";
        }

        private void OnOpponentManaChanged(int currentMana, int maxMana)
        {
            if (_opponentManaLabel != null)
                _opponentManaLabel.Text = $"Mana: {currentMana}/{maxMana}";
        }

        private void OnGameStateChanged(Core.GameState newState)
        {
            GD.Print($"UI: Game state changed to {newState}");
            
            if (_levelLabel != null)
            {
                _levelLabel.Text = $"Level: {_gameManager.CurrentLevel}";
                if (_gameManager.IsBossFight)
                    _levelLabel.Text += " (BOSS)";
            }
        }

        private void OnTurnPhaseChanged(Core.TurnPhase newPhase)
        {
            if (_phaseLabel != null)
                _phaseLabel.Text = $"Phase: {newPhase}";
        }

        private void OnTurnStarted(Core.PlayerType playerType)
        {
            if (_turnIndicatorLabel != null)
            {
                _turnIndicatorLabel.Text = playerType == Core.PlayerType.Player ? 
                    "YOUR TURN" : "OPPONENT'S TURN";
            }

            if (_endTurnButton != null)
            {
                _endTurnButton.Disabled = (playerType != Core.PlayerType.Player);
            }
        }

        private void OnEndTurnPressed()
        {
            _selectedCard = null;
            _gameManager?.EndCurrentTurn();
        }

        private void OnPlayerCardDrawn(CardData cardData)
        {
            if (_playerHand != null && _gameManager?.Player != null)
            {
                _playerHand.AddCard(cardData, _gameManager.Player);
                _playerHand.UpdatePlayableCards(_gameManager.Player.CurrentMana);
            }
        }

        private void OnOpponentCardDrawn(CardData cardData)
        {
            // Don't show opponent's cards (or show card backs)
            GD.Print($"Opponent drew a card");
        }

        private void OnCardSelected(Card card)
        {
            _selectedCard = card;
            GD.Print($"Selected card: {card.Data.CardName}. Click a slot to play it.");
            
            // Highlight available slots
            for (int i = 0; i < Core.GameConstants.CARD_SLOTS; i++)
            {
                if (_playerSlots != null)
                {
                    bool isEmpty = _playerSlots.IsSlotEmpty(i);
                    _playerSlots.HighlightSlot(i, isEmpty);
                }
            }
        }

        private void OnSlotSelected(int slotIndex)
        {
            if (_selectedCard == null)
            {
                GD.Print("No card selected. Click a card in your hand first.");
                return;
            }

            if (_gameManager?.Player == null) return;

            // Try to play the card
            bool success = _gameManager.Player.PlayCard(_selectedCard, slotIndex);
            
            if (success)
            {
                // Remove from hand UI
                if (_playerHand != null)
                {
                    _playerHand.RemoveCard(_selectedCard);
                }

                // Add to slot UI
                if (_playerSlots != null)
                {
                    _playerSlots.PlaceCard(_selectedCard, slotIndex);
                }

                // Update playable cards
                if (_playerHand != null)
                {
                    _playerHand.UpdatePlayableCards(_gameManager.Player.CurrentMana);
                }
            }

            // Clear selection and highlights
            _selectedCard = null;
            for (int i = 0; i < Core.GameConstants.CARD_SLOTS; i++)
            {
                if (_playerSlots != null)
                {
                    _playerSlots.HighlightSlot(i, false);
                }
            }
        }
    }
}
