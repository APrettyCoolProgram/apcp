using Godot;
using System.Collections.Generic;

namespace Dungine.Core;

/// <summary>
/// Represents a location/room in the game world
/// </summary>
public partial class Location : GameObject
{
    public Dictionary<string, string> Exits { get; set; } = new();
    public List<Item> Items { get; set; } = new();
    public List<string> Visited { get; set; } = new();
    public bool IsVisited { get; set; } = false;

    public string GetExitDescription()
    {
        if (Exits.Count == 0)
        {
            return "There are no obvious exits.";
        }

        var availableDirections = new List<string>();
        
        foreach (var exitDirection in Exits.Keys)
        {
            availableDirections.Add(exitDirection);
        }

        string exitListText = string.Join(", ", availableDirections);
        return $"Exits: {exitListText}";
    }

    public string? GetDestination(string direction)
    {
        string normalizedDirection = direction.ToLower();
        
        if (Exits.TryGetValue(normalizedDirection, out var destinationLocationId))
        {
            return destinationLocationId;
        }
        
        return null;
    }

    public void AddItem(Item item)
    {
        Items.Add(item);
    }

    public Item? RemoveItem(string itemName)
    {
        Item? foundItem = Items.Find(currentItem => 
            currentItem.Name.Equals(itemName, System.StringComparison.OrdinalIgnoreCase));
        
        if (foundItem != null)
        {
            Items.Remove(foundItem);
        }
        
        return foundItem;
    }

    public Item? FindItem(string itemName)
    {
        string searchTermLowercase = itemName.ToLower();
        
        Item? foundItem = Items.Find(currentItem => 
        {
            bool nameMatches = currentItem.Name.Equals(itemName, System.StringComparison.OrdinalIgnoreCase);
            bool aliasMatches = currentItem.Aliases?.Contains(searchTermLowercase) ?? false;
            
            return nameMatches || aliasMatches;
        });
        
        return foundItem;
    }
}
