using Godot;

namespace DungeonCharlie.UI
{
    /// <summary>
    /// Defeat screen shown when player loses
    /// </summary>
    public partial class DefeatScreen : Control
    {
        private Label _titleLabel;
        private Label _levelLabel;
        private Label _messageLabel;
        private Button _retryButton;
        private Button _mainMenuButton;

        [Signal]
        public delegate void RetryRequestedEventHandler();

        [Signal]
        public delegate void MainMenuRequestedEventHandler();

        public override void _Ready()
        {
            _titleLabel = GetNodeOrNull<Label>("%TitleLabel");
            _levelLabel = GetNodeOrNull<Label>("%LevelLabel");
            _messageLabel = GetNodeOrNull<Label>("%MessageLabel");
            _retryButton = GetNodeOrNull<Button>("%RetryButton");
            _mainMenuButton = GetNodeOrNull<Button>("%MainMenuButton");

            if (_retryButton != null)
            {
                _retryButton.Pressed += OnRetryPressed;
            }

            if (_mainMenuButton != null)
            {
                _mainMenuButton.Pressed += OnMainMenuPressed;
            }
        }

        /// <summary>
        /// Show defeat screen with level info
        /// </summary>
        public void Show(int levelFailed, bool isBossFight)
        {
            Visible = true;

            if (_titleLabel != null)
            {
                _titleLabel.Text = "DEFEAT";
            }

            if (_levelLabel != null)
            {
                _levelLabel.Text = isBossFight ? 
                    "Defeated by the Boss" : 
                    $"Defeated on Level {levelFailed}";
            }

            if (_messageLabel != null)
            {
                string[] messages = new[]
                {
                    "The dungeon has claimed another adventurer...",
                    "Your journey ends here... for now.",
                    "Defeat is but a lesson in disguise.",
                    "Even the best warriors fall sometimes.",
                    "The dungeon proves too challenging... this time."
                };
                
                _messageLabel.Text = messages[GD.RandRange(0, messages.Length - 1)];
            }
        }

        private void OnRetryPressed()
        {
            Visible = false;
            EmitSignal(SignalName.RetryRequested);
        }

        private void OnMainMenuPressed()
        {
            Visible = false;
            EmitSignal(SignalName.MainMenuRequested);
        }
    }
}
