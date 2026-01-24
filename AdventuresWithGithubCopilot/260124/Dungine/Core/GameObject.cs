using Godot;
using System.Collections.Generic;

namespace Dungine.Core;

/// <summary>
/// Core interface for all game objects (items, locations, NPCs, etc.)
/// </summary>
public interface IGameObject
{
    string Id { get; }
    string Name { get; }
    string Description { get; }
    Dictionary<string, string> Properties { get; }
}

/// <summary>
/// Base class for all interactive game objects
/// </summary>
public abstract partial class GameObject : Node, IGameObject
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Dictionary<string, string> Properties { get; set; } = new();

    public virtual string Examine()
    {
        return Description;
    }
}
