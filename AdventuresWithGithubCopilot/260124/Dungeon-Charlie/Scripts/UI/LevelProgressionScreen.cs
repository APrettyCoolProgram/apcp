using Godot;

namespace DungeonCharlie.UI
{
    /// <summary>
    /// Screen shown between levels with progression info
    /// </summary>
    public partial class LevelProgressionScreen : Control
    {
        private Label _titleLabel;
        private Label _currentLevelLabel;
        private Label _nextLevelLabel;
        private Label _progressLabel;
        private Button _startButton;
        private ProgressBar _progressBar;

        [Signal]
        public delegate void StartLevelRequestedEventHandler();

        public override void _Ready()
        {
            _titleLabel = GetNodeOrNull<Label>("%TitleLabel");
            _currentLevelLabel = GetNodeOrNull<Label>("%CurrentLevelLabel");
            _nextLevelLabel = GetNodeOrNull<Label>("%NextLevelLabel");
            _progressLabel = GetNodeOrNull<Label>("%ProgressLabel");
            _startButton = GetNodeOrNull<Button>("%StartButton");
            _progressBar = GetNodeOrNull<ProgressBar>("%ProgressBar");

            if (_startButton != null)
            {
                _startButton.Pressed += OnStartPressed;
            }
        }

        /// <summary>
        /// Show progression screen for next level
        /// </summary>
        public void Show(int nextLevel, int playerHealth)
        {
            Visible = true;
            bool isBossLevel = (nextLevel == Core.GameConstants.BOSS_LEVEL);

            if (_titleLabel != null)
            {
                _titleLabel.Text = isBossLevel ? "BOSS FIGHT AHEAD!" : "LEVEL UP!";
            }

            if (_currentLevelLabel != null)
            {
                _currentLevelLabel.Text = $"Completed: Level {nextLevel - 1}";
            }

            if (_nextLevelLabel != null)
            {
                _nextLevelLabel.Text = isBossLevel ? 
                    "Next: FINAL BOSS" : 
                    $"Next: Level {nextLevel}";
            }

            if (_progressLabel != null)
            {
                _progressLabel.Text = $"Health: {playerHealth}\nProgress: {nextLevel - 1}/{Core.GameConstants.BOSS_LEVEL}";
            }

            if (_progressBar != null)
            {
                _progressBar.MaxValue = Core.GameConstants.BOSS_LEVEL;
                _progressBar.Value = nextLevel - 1;
            }

            if (_startButton != null)
            {
                _startButton.Text = isBossLevel ? "Face the Boss!" : "Begin Next Level";
            }
        }

        private void OnStartPressed()
        {
            Visible = false;
            EmitSignal(SignalName.StartLevelRequested);
        }
    }
}
