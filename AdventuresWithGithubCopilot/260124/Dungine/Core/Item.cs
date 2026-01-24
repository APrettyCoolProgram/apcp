using Godot;
using System.Collections.Generic;

namespace Dungine.Core;

/// <summary>
/// Represents an item that can be picked up, examined, and used
/// </summary>
public partial class Item : GameObject
{
    public bool IsPortable { get; set; } = true;
    public bool IsContainer { get; set; } = false;
    public List<Item> Contents { get; set; } = new();
    public List<string>? Aliases { get; set; }
    public bool IsVisible { get; set; } = true;

    public override string Examine()
    {
        string examinationResult = Description;
        
        if (IsContainer && Contents.Count > 0)
        {
            examinationResult += "\n\nIt contains:";
            
            foreach (var containedItem in Contents)
            {
                examinationResult += $"\n  - {containedItem.Name}";
            }
        }

        return examinationResult;
    }

    public void AddToContainer(Item item)
    {
        if (IsContainer)
        {
            Contents.Add(item);
        }
    }

    public Item? RemoveFromContainer(string itemName)
    {
        Item? foundItem = Contents.Find(containedItem => 
            containedItem.Name.Equals(itemName, System.StringComparison.OrdinalIgnoreCase));
        
        if (foundItem != null)
        {
            Contents.Remove(foundItem);
        }
        
        return foundItem;
    }
}
