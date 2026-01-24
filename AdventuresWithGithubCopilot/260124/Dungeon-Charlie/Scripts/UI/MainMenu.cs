using Godot;

namespace DungeonCharlie.UI
{
    /// <summary>
    /// Controls the main menu UI
    /// </summary>
    public partial class MainMenu : Control
    {
        private Button _startButton;
        private Button _quitButton;
        private Label _titleLabel;

        public override void _Ready()
        {
            _startButton = GetNodeOrNull<Button>("%StartButton");
            _quitButton = GetNodeOrNull<Button>("%QuitButton");
            _titleLabel = GetNodeOrNull<Label>("%TitleLabel");

            if (_startButton != null)
                _startButton.Pressed += OnStartPressed;

            if (_quitButton != null)
                _quitButton.Pressed += OnQuitPressed;

            if (_titleLabel != null)
                _titleLabel.Text = "DUNGEON: CHARLIE";
        }

        private void OnStartPressed()
        {
            GD.Print("Start button pressed - Loading game scene...");
            
            // Load the main game scene
            var error = GetTree().ChangeSceneToFile("res://Scenes/Game.tscn");
            if (error != Error.Ok)
            {
                GD.PrintErr($"Failed to load game scene: {error}");
            }
        }

        private void OnQuitPressed()
        {
            GetTree().Quit();
        }
    }
}
