using Godot;
using System.Collections.Generic;

namespace Dungine.Core;

/// <summary>
/// Manages the player's inventory
/// </summary>
public partial class Inventory : Node
{
    private List<Item> _items = new();
    public int MaxWeight { get; set; } = 100;
    public int CurrentWeight { get; private set; } = 0;

    public List<Item> Items => _items;

    public bool AddItem(Item item)
    {
        if (!item.IsPortable)
        {
            return false;
        }

        _items.Add(item);
        
        // Future: calculate weight
        
        return true;
    }

    public Item? RemoveItem(string itemName)
    {
        string searchTermLowercase = itemName.ToLower();
        
        Item? foundItem = _items.Find(currentItem =>
        {
            bool nameMatches = currentItem.Name.Equals(itemName, System.StringComparison.OrdinalIgnoreCase);
            bool aliasMatches = currentItem.Aliases?.Contains(searchTermLowercase) ?? false;
            
            return nameMatches || aliasMatches;
        });
        
        if (foundItem != null)
        {
            _items.Remove(foundItem);
        }
        
        return foundItem;
    }

    public Item? FindItem(string itemName)
    {
        string searchTermLowercase = itemName.ToLower();
        
        Item? foundItem = _items.Find(currentItem =>
        {
            bool nameMatches = currentItem.Name.Equals(itemName, System.StringComparison.OrdinalIgnoreCase);
            bool aliasMatches = currentItem.Aliases?.Contains(searchTermLowercase) ?? false;
            
            return nameMatches || aliasMatches;
        });
        
        return foundItem;
    }

    public bool HasItem(string itemName)
    {
        return FindItem(itemName) != null;
    }

    public string GetInventoryList()
    {
        if (_items.Count == 0)
        {
            return "You are carrying nothing.";
        }

        string inventoryDescription = "You are carrying:\n";
        
        foreach (var carriedItem in _items)
        {
            inventoryDescription += $"  - {carriedItem.Name}\n";
        }
        
        return inventoryDescription;
    }
}
