using Godot;
using System.Collections.Generic;

namespace Dungine.Core;

/// <summary>
/// Represents the game world containing all locations
/// </summary>
public partial class World : Node
{
    private Dictionary<string, Location> _locations = new();
    public string? StartLocationId { get; set; }

    public void AddLocation(Location location)
    {
        _locations[location.Id] = location;
        AddChild(location);
    }

    public Location? GetLocation(string locationId)
    {
        if (_locations.TryGetValue(locationId, out var foundLocation))
        {
            return foundLocation;
        }
        
        return null;
    }

    public Location? GetStartLocation()
    {
        if (string.IsNullOrEmpty(StartLocationId))
        {
            return null;
        }
        
        return GetLocation(StartLocationId);
    }

    public void LoadWorld(WorldData worldData)
    {
        // Clear existing world
        foreach (var existingLocation in _locations.Values)
        {
            existingLocation.QueueFree();
        }
        _locations.Clear();

        // Load locations from world data
        foreach (var locationData in worldData.Locations)
        {
            var newLocation = new Location
            {
                Id = locationData.Id,
                Name = locationData.Name,
                Description = locationData.Description,
                Exits = new Dictionary<string, string>(locationData.Exits)
            };

            // Load items in this location
            foreach (var itemData in locationData.Items)
            {
                var newItem = CreateItemFromData(itemData);
                newLocation.AddItem(newItem);
            }

            AddLocation(newLocation);
        }

        StartLocationId = worldData.StartLocationId;
    }

    private Item CreateItemFromData(ItemData itemData)
    {
        var newItem = new Item
        {
            Id = itemData.Id,
            Name = itemData.Name,
            Description = itemData.Description,
            IsPortable = itemData.IsPortable,
            IsContainer = itemData.IsContainer,
            IsVisible = itemData.IsVisible,
            Aliases = itemData.Aliases != null ? new List<string>(itemData.Aliases) : null
        };

        // Load items contained within this item (if it's a container)
        if (itemData.Contents != null)
        {
            foreach (var containedItemData in itemData.Contents)
            {
                var containedItem = CreateItemFromData(containedItemData);
                newItem.AddToContainer(containedItem);
            }
        }

        return newItem;
    }
}

/// <summary>
/// Data structures for loading world data
/// </summary>
public class WorldData
{
    public string StartLocationId { get; set; } = string.Empty;
    public List<LocationData> Locations { get; set; } = new();
}

public class LocationData
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Dictionary<string, string> Exits { get; set; } = new();
    public List<ItemData> Items { get; set; } = new();
}

public class ItemData
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsPortable { get; set; } = true;
    public bool IsContainer { get; set; } = false;
    public bool IsVisible { get; set; } = true;
    public List<string>? Aliases { get; set; }
    public List<ItemData>? Contents { get; set; }
}
