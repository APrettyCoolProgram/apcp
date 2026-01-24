using Godot;

namespace Dungine.Core;

/// <summary>
/// Manages the current game state
/// </summary>
public partial class GameState : Node
{
    public World World { get; set; } = null!;
    public Location? CurrentLocation { get; set; }
    public Inventory PlayerInventory { get; set; } = null!;
    public bool IsRunning { get; set; } = true;
    public int TurnCount { get; set; } = 0;
    public int Score { get; set; } = 0;

    public override void _Ready()
    {
        // Initialize the game world
        World = new World();
        AddChild(World);
        
        // Initialize the player's inventory
        PlayerInventory = new Inventory();
        AddChild(PlayerInventory);
    }

    public void StartGame()
    {
        CurrentLocation = World.GetStartLocation();
        IsRunning = true;
        TurnCount = 0;
    }

    public void IncrementTurn()
    {
        TurnCount++;
    }
    }
}
