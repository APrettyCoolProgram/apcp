using Godot;
using System;

namespace DungeonCharlie.Cards
{
    /// <summary>
    /// Represents the data for a single card in the game
    /// </summary>
    public partial class CardData : Resource
    {
        [Export] public string CardName { get; set; } = string.Empty;
        [Export] public string Description { get; set; } = string.Empty;
        [Export] public int ManaCost { get; set; }
        [Export] public int AttackPower { get; set; }
        [Export] public int DefensePower { get; set; }
        [Export] public Core.CardType CardType { get; set; }
        [Export] public Core.CardSubType CardSubType { get; set; }
        [Export] public Core.CardRarity Rarity { get; set; }
        [Export] public Texture2D CardArt { get; set; }
        [Export] public string CardId { get; set; } = string.Empty;

        /// <summary>
        /// Special effect description for spells and items
        /// </summary>
        [Export] public string SpecialEffect { get; set; } = string.Empty;

        public CardData() { }

        public CardData(string id, string name, string description, int manaCost, 
                       int attack, int defense, Core.CardType type, Core.CardSubType subType, 
                       Core.CardRarity rarity)
        {
            CardId = id;
            CardName = name;
            Description = description;
            ManaCost = manaCost;
            AttackPower = attack;
            DefensePower = defense;
            CardType = type;
            CardSubType = subType;
            Rarity = rarity;
        }
    }
}
