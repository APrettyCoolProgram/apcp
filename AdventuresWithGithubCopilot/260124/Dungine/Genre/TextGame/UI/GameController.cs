using Godot;
using Dungine.Core;
using Dungine.Genre.TextGame.Commands;

namespace Dungine.Genre.TextGame.UI;

/// <summary>
/// Main game controller that manages the text adventure interface
/// </summary>
public partial class GameController : Control
{
    public const string EngineVersion = "0.0.0.1";
    
    private RichTextLabel? _outputText;
    private LineEdit? _inputField;
    private GameState? _gameState;
    private CommandProcessor? _commandProcessor;

    public override void _Ready()
    {
        // Create the user interface
        SetupUI();
        
        // Initialize game state and systems
        _gameState = new GameState();
        AddChild(_gameState);
        
        _commandProcessor = new CommandProcessor();
        AddChild(_commandProcessor);
        
        // Load the demo world
        LoadDemoWorld();
        
        // Start the game
        _gameState.StartGame();
        
        // Display welcome message and initial location
        OutputText($"\n=== DUNGINE TEXT ADVENTURE v{EngineVersion} ===");
        OutputText("A modular text adventure engine for Godot\n");
        
        var lookCommand = new LookCommand();
        var initialLocationDescription = lookCommand.Execute(
            new Genre.TextGame.Parser.ParsedCommand(), 
            _gameState
        );
        
        OutputText(initialLocationDescription);
        OutputText("\nType 'help' for commands.\n");
    }

    private void SetupUI()
    {
        // Main vertical layout container
        var mainVerticalContainer = new VBoxContainer
        {
            AnchorRight = 1,
            AnchorBottom = 1
        };
        AddChild(mainVerticalContainer);

        // Output text area (scrollable)
        var outputScrollContainer = new ScrollContainer
        {
            CustomMinimumSize = new Vector2(0, 400),
            SizeFlagsVertical = SizeFlags.ExpandFill
        };
        mainVerticalContainer.AddChild(outputScrollContainer);

        _outputText = new RichTextLabel
        {
            BbcodeEnabled = true,
            ScrollFollowing = true,
            SizeFlagsHorizontal = SizeFlags.ExpandFill,
            SizeFlagsVertical = SizeFlags.ExpandFill
        };
        outputScrollContainer.AddChild(_outputText);

        // Input area (horizontal container for prompt and input field)
        var inputHorizontalContainer = new HBoxContainer();
        mainVerticalContainer.AddChild(inputHorizontalContainer);

        var commandPromptLabel = new Label
        {
            Text = "> "
        };
        inputHorizontalContainer.AddChild(commandPromptLabel);

        _inputField = new LineEdit
        {
            SizeFlagsHorizontal = SizeFlags.ExpandFill,
            PlaceholderText = "Enter command..."
        };
        _inputField.TextSubmitted += OnCommandSubmitted;
        inputHorizontalContainer.AddChild(_inputField);
    }

    private void OnCommandSubmitted(string userInput)
    {
        if (_inputField == null || _gameState == null || _commandProcessor == null)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(userInput))
        {
            return;
        }

        // Echo the player's input
        OutputText($"\n> {userInput}");

        // Process the command and get the result
        string commandResult = _commandProcessor.ProcessInput(userInput, _gameState);
        OutputText(commandResult);

        // Increment the turn counter
        _gameState.IncrementTurn();

        // Clear the input field for the next command
        _inputField.Text = string.Empty;

        // Check if the game should exit
        if (!_gameState.IsRunning)
        {
            _inputField.Editable = false;
            OutputText("\n[Press Alt+F4 to close]");
        }
    }

    private void OutputText(string textToDisplay)
    {
        if (_outputText != null)
        {
            _outputText.Text += $"\n{textToDisplay}";
        }
    }

    private void LoadDemoWorld()
    {
        if (_gameState?.World == null)
        {
            return;
        }

        // Create a simple demo world inspired by Zork
        var demoWorldData = new WorldData
        {
            StartLocationId = "west_of_house",
            Locations = new()
            {
                new LocationData
                {
                    Id = "west_of_house",
                    Name = "West of House",
                    Description = "You are standing in an open field west of a white house, with a boarded front door.",
                    Exits = new()
                    {
                        { "north", "north_of_house" },
                        { "south", "south_of_house" },
                        { "east", "behind_house" }
                    },
                    Items = new()
                    {
                        new ItemData
                        {
                            Id = "mailbox",
                            Name = "Mailbox",
                            Description = "A small mailbox. It appears to be closed.",
                            IsPortable = false,
                            IsContainer = true,
                            Contents = new()
                            {
                                new ItemData
                                {
                                    Id = "leaflet",
                                    Name = "Leaflet",
                                    Description = "The leaflet reads: 'Welcome to Dungine! This is a demo world showing the text adventure capabilities.'",
                                    Aliases = new() { "paper", "note" }
                                }
                            }
                        }
                    }
                },
                new LocationData
                {
                    Id = "north_of_house",
                    Name = "North of House",
                    Description = "You are facing the north side of a white house. There is no door here, and all the windows are boarded up.",
                    Exits = new()
                    {
                        { "south", "west_of_house" },
                        { "east", "behind_house" }
                    }
                },
                new LocationData
                {
                    Id = "south_of_house",
                    Name = "South of House",
                    Description = "You are facing the south side of a white house. There is no door here, and all the windows are boarded up.",
                    Exits = new()
                    {
                        { "north", "west_of_house" },
                        { "east", "behind_house" }
                    }
                },
                new LocationData
                {
                    Id = "behind_house",
                    Name = "Behind House",
                    Description = "You are behind the white house. A path leads into the forest to the east. In one corner of the house there is a small window which is slightly ajar.",
                    Exits = new()
                    {
                        { "west", "west_of_house" },
                        { "north", "north_of_house" },
                        { "south", "south_of_house" },
                        { "east", "forest_path" }
                    },
                    Items = new()
                    {
                        new ItemData
                        {
                            Id = "window",
                            Name = "Window",
                            Description = "The window is slightly ajar, but not enough to allow entry.",
                            IsPortable = false
                        }
                    }
                },
                new LocationData
                {
                    Id = "forest_path",
                    Name = "Forest Path",
                    Description = "This is a path winding through a dimly lit forest. The path continues to the north and south, and west towards the house.",
                    Exits = new()
                    {
                        { "west", "behind_house" },
                        { "north", "clearing" },
                        { "south", "forest_path" }
                    }
                },
                new LocationData
                {
                    Id = "clearing",
                    Name = "Clearing",
                    Description = "You are in a clearing in the forest. Sunlight streams down through the trees.",
                    Exits = new()
                    {
                        { "south", "forest_path" }
                    },
                    Items = new()
                    {
                        new ItemData
                        {
                            Id = "lamp",
                            Name = "Brass Lantern",
                            Description = "A shiny brass lantern. It appears to be off.",
                            Aliases = new() { "lantern", "lamp", "light" }
                        },
                        new ItemData
                        {
                            Id = "sword",
                            Name = "Sword",
                            Description = "A rusty but serviceable sword.",
                            Aliases = new() { "blade", "weapon" }
                        }
                    }
                }
            }
        };

        _gameState.World.LoadWorld(demoWorldData);
    }
}
