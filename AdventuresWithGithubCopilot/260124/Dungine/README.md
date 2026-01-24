# Dungine

A modular text adventure game engine built with Godot 4.5.1 and C#.

## Overview

Dungine is designed to be an extensible game engine that can eventually support various game types in both 2D and 3D. The initial implementation focuses on text-based adventure games in the style of classic interactive fiction like Zork.

## Features

### Current Implementation (v0.0.0.1)
- **Text Adventure Engine**: Full-featured parser for natural language commands
- **World System**: Location-based navigation with dynamic room descriptions
- **Inventory Management**: Pick up, drop, and examine items
- **Item System**: Portable and non-portable items, containers, item aliases
- **Command System**: Extensible command processor with built-in commands
- **Cross-Platform**: Runs on Windows, MacOS, and Web (via HTML5 export)

### Architecture

```
Dungine/
├── Core/                      # Core engine components (shared)
│   ├── GameObject.cs          # Base class for all game objects
│   ├── Location.cs            # Room/location system
│   ├── Item.cs                # Item and container system
│   ├── Inventory.cs           # Player inventory management
│   ├── World.cs               # World management and data loading
│   └── GameState.cs           # Global game state
└── Genre/                     # Genre-specific implementations
    └── TextGame/              # Text adventure game implementation
        ├── Parser/            # Text parsing system
        │   └── TextParser.cs  # Natural language parser
        ├── Commands/          # Command system
        │   ├── CommandProcessor.cs  # Command execution engine
        │   └── DefaultCommands.cs   # Built-in commands
        ├── UI/                # User interface
        │   └── GameController.cs    # Main game UI controller
        └── Scenes/            # Godot scenes
            └── Main.tscn      # Main game scene
```

## Built-in Commands

- `look` (l) - Examine your surroundings
- `go <direction>` - Move in a direction (north, south, east, west, up, down)
  - Shortcuts: n, s, e, w, u, d
- `take <item>` - Pick up an item
- `drop <item>` - Drop an item from your inventory
- `inventory` (i, inv) - View your inventory
- `examine <item>` (x) - Look closely at an item
- `help` (?) - Display available commands
- `quit` (q) - Exit the game

## Getting Started

### Prerequisites
- Godot 4.5.1 with .NET support
- .NET 10.0 SDK

### Running the Game

1. Open the project in Godot 4.5.1
2. Ensure C# tools are installed (Project → Tools → C# → Install C# Support)
3. Build the project (Build → Build Solution)
4. Run the project (F5)

### Demo World

The engine includes a demo world inspired by Zork, featuring:
- Multiple interconnected locations
- Interactive items (mailbox, lantern, sword)
- Container items (mailbox contains a leaflet)
- Navigation between rooms

## Extending Dungine

### Adding New Commands

Create a new class inheriting from `Command`:

```csharp
public class MyCommand : Command
{
    public override string[] Verbs => new[] { "mycommand", "mc" };
    public override string Description => "Description of my command";

    public override string Execute(ParsedCommand parsed, GameState state)
    {
        // Your command logic here
        return "Command executed!";
    }
}
```

Register it in the CommandProcessor:

```csharp
commandProcessor.RegisterCommand(new MyCommand());
```

### Creating Custom Worlds

Define world data using the `WorldData` structure:

```csharp
var worldData = new WorldData
{
    StartLocationId = "start_room",
    Locations = new List<LocationData>
    {
        new LocationData
        {
            Id = "start_room",
            Name = "Starting Room",
            Description = "A description of the room.",
            Exits = new Dictionary<string, string>
            {
                { "north", "next_room" }
            },
            Items = new List<ItemData>
            {
                new ItemData
                {
                    Id = "item1",
                    Name = "Item Name",
                    Description = "Item description",
                    IsPortable = true
                }
            }
        }
    }
};

gameState.World.LoadWorld(worldData);
```

## Future Roadmap

- [ ] Save/Load system
- [ ] JSON-based world definition files
- [ ] Action system for item interactions
- [ ] NPC system with dialogue
- [ ] Puzzle system
- [ ] Sound effects and music
- [ ] Multiple UI themes
- [ ] 2D graphical mode with text overlay
- [ ] Full 3D exploration mode
- [ ] Visual world editor
- [ ] Scripting system for complex interactions

## Platform Support

### Windows
Full support - native executable

### MacOS
Full support - native executable

### Web (HTML5)
Full support via Godot's HTML5 export
Note: May require specific .NET WASM configuration

## License

[Choose your license]

## Contributing

Contributions are welcome! Please ensure:
- Code follows C# naming conventions
- Commands are well-documented
- World data is properly structured
- Cross-platform compatibility is maintained
