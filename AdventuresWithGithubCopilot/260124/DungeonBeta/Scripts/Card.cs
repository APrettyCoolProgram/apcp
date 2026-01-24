using Godot;
using System;

public enum CardType { Offensive, Defensive, Spell, Item }

public partial class Card : RefCounted
{
    public string Name { get; set; } = "Unnamed";
    public CardType Type { get; set; } = CardType.Offensive;
    public int Cost { get; set; } = 1;
    public int Attack { get; set; } = 0;
    public int Durability { get; set; } = 1;
    public string Description { get; set; } = "";

    public Card() { }

    public Card(string name, CardType type, int cost, int attack, int durability, string desc = "")
    {
        Name = name;
        Type = type;
        Cost = cost;
        Attack = attack;
        Durability = durability;
        Description = desc;
    }
}
