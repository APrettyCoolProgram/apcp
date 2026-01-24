using Godot;
using System;

public enum CardType { Offensive, Defensive, Spell, Item }

public class Card
{
    public string Id { get; set; }
    public string Name { get; set; }
    public CardType Type { get; set; }
    public int Cost { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public string EffectId { get; set; }

    public Card() { }

    public Card(string id, string name, CardType type, int cost, int attack = 0, int defense = 0, string effectId = "")
    {
        Id = id; Name = name; Type = type; Cost = cost; Attack = attack; Defense = defense; EffectId = effectId;
    }

    public override string ToString() => $"{Name} ({Type}) Cost:{Cost} ATK:{Attack} DEF:{Defense}";
}
