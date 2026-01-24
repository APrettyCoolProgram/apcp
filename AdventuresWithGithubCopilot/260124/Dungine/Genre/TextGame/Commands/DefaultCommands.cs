using Dungine.Core;
using Dungine.Genre.TextGame.Parser;

namespace Dungine.Genre.TextGame.Commands;

public class LookCommand : Command
{
    public override string[] Verbs => new[] { "look", "l" };
    public override string Description => "Look around the current location";

    public override string Execute(ParsedCommand parsedCommand, GameState gameState)
    {
        Location? currentLocation = gameState.CurrentLocation;
        
        if (currentLocation == null)
        {
            return "You are nowhere.";
        }

        string locationDescription = $"\n{currentLocation.Name}\n{currentLocation.Description}\n";

        if (currentLocation.Items.Count > 0)
        {
            locationDescription += "\nYou can see:\n";
            
            foreach (var visibleItem in currentLocation.Items)
            {
                if (visibleItem.IsVisible)
                {
                    locationDescription += $"  - {visibleItem.Name}\n";
                }
            }
        }

        locationDescription += $"\n{currentLocation.GetExitDescription()}";
        
        return locationDescription;
    }
}

public class GoCommand : Command
{
    public override string[] Verbs => new[] { "go", "move", "walk", "n", "s", "e", "w", "north", "south", "east", "west", "up", "down", "u", "d" };
    public override string Description => "Move in a direction (go north, north, n)";

    public override string Execute(ParsedCommand parsedCommand, GameState gameState)
    {
        string? directionToMove = parsedCommand.Verb;
        
        // If the verb is "go" or "move", the direction is in DirectObject
        if (directionToMove == "go" || directionToMove == "move" || directionToMove == "walk")
        {
            directionToMove = parsedCommand.DirectObject?.ToLower();
        }

        // Expand common direction abbreviations
        directionToMove = directionToMove switch
        {
            "n" => "north",
            "s" => "south",
            "e" => "east",
            "w" => "west",
            "u" => "up",
            "d" => "down",
            _ => directionToMove
        };

        if (string.IsNullOrEmpty(directionToMove))
        {
            return "Go where?";
        }

        string? destinationLocationId = gameState.CurrentLocation?.GetDestination(directionToMove);
        
        if (destinationLocationId == null)
        {
            return $"You can't go {directionToMove} from here.";
        }

        Location? newLocation = gameState.World.GetLocation(destinationLocationId);
        
        if (newLocation == null)
        {
            return "That way leads nowhere.";
        }

        gameState.CurrentLocation = newLocation;
        
        // Show the new location description
        var lookCommand = new LookCommand();
        return lookCommand.Execute(parsedCommand, gameState);
    }
}

public class TakeCommand : Command
{
    public override string[] Verbs => new[] { "take", "get", "grab", "pick" };
    public override string Description => "Take an item (take lamp)";

    public override string Execute(ParsedCommand parsedCommand, GameState gameState)
    {
        if (string.IsNullOrEmpty(parsedCommand.DirectObject))
        {
            return "Take what?";
        }

        Item? itemToTake = gameState.CurrentLocation?.FindItem(parsedCommand.DirectObject);
        
        if (itemToTake == null)
        {
            return $"You don't see any '{parsedCommand.DirectObject}' here.";
        }

        if (!itemToTake.IsPortable)
        {
            return $"You can't take the {itemToTake.Name}.";
        }

        gameState.CurrentLocation?.RemoveItem(itemToTake.Name);
        gameState.PlayerInventory.AddItem(itemToTake);

        return $"Taken: {itemToTake.Name}";
    }
}

public class DropCommand : Command
{
    public override string[] Verbs => new[] { "drop", "put" };
    public override string Description => "Drop an item (drop lamp)";

    public override string Execute(ParsedCommand parsedCommand, GameState gameState)
    {
        if (string.IsNullOrEmpty(parsedCommand.DirectObject))
        {
            return "Drop what?";
        }

        Item? itemToDrop = gameState.PlayerInventory.RemoveItem(parsedCommand.DirectObject);
        
        if (itemToDrop == null)
        {
            return $"You don't have any '{parsedCommand.DirectObject}'.";
        }

        gameState.CurrentLocation?.AddItem(itemToDrop);

        return $"Dropped: {itemToDrop.Name}";
    }
}

public class InventoryCommand : Command
{
    public override string[] Verbs => new[] { "inventory", "i", "inv" };
    public override string Description => "Show your inventory";

    public override string Execute(ParsedCommand parsed, GameState state)
    {
        return state.PlayerInventory.GetInventoryList();
    }
}

public class ExamineCommand : Command
{
    public override string[] Verbs => new[] { "examine", "x", "inspect", "read" };
    public override string Description => "Examine something closely (examine lamp)";

    public override string Execute(ParsedCommand parsedCommand, GameState gameState)
    {
        if (string.IsNullOrEmpty(parsedCommand.DirectObject))
        {
            return "Examine what?";
        }

        // Check player's inventory first
        Item? itemToExamine = gameState.PlayerInventory.FindItem(parsedCommand.DirectObject);
        
        // If not in inventory, check current location
        if (itemToExamine == null)
        {
            itemToExamine = gameState.CurrentLocation?.FindItem(parsedCommand.DirectObject);
        }

        if (itemToExamine == null)
        {
            return $"You don't see any '{parsedCommand.DirectObject}' here.";
        }

        return itemToExamine.Examine();
    }
}

public class HelpCommand : Command
{
    public override string[] Verbs => new[] { "help", "?" };
    public override string Description => "Show available commands";

    public override string Execute(ParsedCommand parsed, GameState state)
    {
        return @"Available Commands:
  look (l) - Look around
  go <direction> - Move (north, south, east, west, up, down)
  take <item> - Pick up an item
  drop <item> - Drop an item
  inventory (i) - Show what you're carrying
  examine <item> - Look at something closely
  help - Show this help
  quit - Exit the game";
    }
}

public class QuitCommand : Command
{
    public override string[] Verbs => new[] { "quit", "exit", "q" };
    public override string Description => "Exit the game";

    public override string Execute(ParsedCommand parsed, GameState state)
    {
        state.IsRunning = false;
        return "Thanks for playing!";
    }
}
