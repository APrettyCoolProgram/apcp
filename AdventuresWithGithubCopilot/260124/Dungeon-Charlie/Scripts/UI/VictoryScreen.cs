using Godot;

namespace DungeonCharlie.UI
{
    /// <summary>
    /// Victory screen shown when player wins a level
    /// </summary>
    public partial class VictoryScreen : Control
    {
        private Label _titleLabel;
        private Label _levelLabel;
        private Label _statsLabel;
        private Button _continueButton;
        private Button _mainMenuButton;

        [Signal]
        public delegate void ContinueRequestedEventHandler();

        [Signal]
        public delegate void MainMenuRequestedEventHandler();

        public override void _Ready()
        {
            _titleLabel = GetNodeOrNull<Label>("%TitleLabel");
            _levelLabel = GetNodeOrNull<Label>("%LevelLabel");
            _statsLabel = GetNodeOrNull<Label>("%StatsLabel");
            _continueButton = GetNodeOrNull<Button>("%ContinueButton");
            _mainMenuButton = GetNodeOrNull<Button>("%MainMenuButton");

            if (_continueButton != null)
            {
                _continueButton.Pressed += OnContinuePressed;
            }

            if (_mainMenuButton != null)
            {
                _mainMenuButton.Pressed += OnMainMenuPressed;
            }
        }

        /// <summary>
        /// Show victory screen with level info
        /// </summary>
        public void Show(int levelCompleted, int playerHealth, int opponentHealth, bool isBossFight)
        {
            Visible = true;

            if (_titleLabel != null)
            {
                _titleLabel.Text = isBossFight ? "BOSS DEFEATED!" : "VICTORY!";
            }

            if (_levelLabel != null)
            {
                _levelLabel.Text = isBossFight ? 
                    "You have conquered the dungeon!" : 
                    $"Level {levelCompleted} Complete";
            }

            if (_statsLabel != null)
            {
                _statsLabel.Text = $"Your Health: {playerHealth}\nOpponent Health: {opponentHealth}";
            }

            // If boss was defeated, change button text
            if (_continueButton != null)
            {
                _continueButton.Text = isBossFight ? "Finish Game" : "Next Level";
            }
        }

        private void OnContinuePressed()
        {
            Visible = false;
            EmitSignal(SignalName.ContinueRequested);
        }

        private void OnMainMenuPressed()
        {
            Visible = false;
            EmitSignal(SignalName.MainMenuRequested);
        }
    }
}
