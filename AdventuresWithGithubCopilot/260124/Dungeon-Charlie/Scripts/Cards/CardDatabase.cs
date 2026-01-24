using Godot;
using System.Collections.Generic;

namespace DungeonCharlie.Cards
{
    /// <summary>
    /// Database of all available cards in the game
    /// </summary>
    public partial class CardDatabase : Node
    {
        private Dictionary<string, CardData> _allCards = new Dictionary<string, CardData>();

        public override void _Ready()
        {
            InitializeCards();
        }

        /// <summary>
        /// Initialize all card definitions
        /// </summary>
        private void InitializeCards()
        {
            // OFFENSIVE CARDS - Weapons
            AddCard(new CardData(
                "sword_1", "Iron Sword", "A basic sword", 2, 3, 0,
                Core.CardType.Offensive, Core.CardSubType.Sword, Core.CardRarity.Common
            ));

            AddCard(new CardData(
                "sword_2", "Steel Sword", "A sturdy blade", 3, 4, 0,
                Core.CardType.Offensive, Core.CardSubType.Sword, Core.CardRarity.Common
            ));

            AddCard(new CardData(
                "sword_3", "Legendary Blade", "A mythical weapon", 5, 7, 0,
                Core.CardType.Offensive, Core.CardSubType.Sword, Core.CardRarity.Legendary
            ));

            AddCard(new CardData(
                "axe_1", "Battle Axe", "Heavy and powerful", 3, 5, 0,
                Core.CardType.Offensive, Core.CardSubType.Axe, Core.CardRarity.Common
            ));

            AddCard(new CardData(
                "axe_2", "War Axe", "Devastating strikes", 4, 6, 0,
                Core.CardType.Offensive, Core.CardSubType.Axe, Core.CardRarity.Uncommon
            ));

            AddCard(new CardData(
                "hammer_1", "Iron Hammer", "Crushes armor", 4, 5, 0,
                Core.CardType.Offensive, Core.CardSubType.Hammer, Core.CardRarity.Common
            ));

            AddCard(new CardData(
                "hammer_2", "Thunder Hammer", "Lightning infused", 6, 8, 0,
                Core.CardType.Offensive, Core.CardSubType.Hammer, Core.CardRarity.Epic
            ));

            // DEFENSIVE CARDS - Armor
            AddCard(new CardData(
                "shield_1", "Wooden Shield", "Basic protection", 2, 0, 3,
                Core.CardType.Defensive, Core.CardSubType.Shield, Core.CardRarity.Common
            ));

            AddCard(new CardData(
                "shield_2", "Iron Shield", "Solid defense", 3, 0, 4,
                Core.CardType.Defensive, Core.CardSubType.Shield, Core.CardRarity.Common
            ));

            AddCard(new CardData(
                "shield_3", "Tower Shield", "Immovable defense", 5, 0, 6,
                Core.CardType.Defensive, Core.CardSubType.Shield, Core.CardRarity.Rare
            ));

            AddCard(new CardData(
                "armor_1", "Leather Armor", "Light protection", 2, 0, 2,
                Core.CardType.Defensive, Core.CardSubType.Platemail, Core.CardRarity.Common
            ));

            AddCard(new CardData(
                "armor_2", "Platemail", "Heavy armor", 4, 0, 5,
                Core.CardType.Defensive, Core.CardSubType.Platemail, Core.CardRarity.Uncommon
            ));

            // SPELL CARDS
            AddCard(new CardData(
                "spell_fireball", "Fireball", "Blazing magic attack", 3, 4, 0,
                Core.CardType.Spell, Core.CardSubType.Fireball, Core.CardRarity.Common
            ));

            AddCard(new CardData(
                "spell_lightning", "Lightning Strike", "Electric devastation", 4, 5, 0,
                Core.CardType.Spell, Core.CardSubType.LightningStrike, Core.CardRarity.Uncommon
            ));

            AddCard(new CardData(
                "spell_dodge", "Dodge", "Evade incoming attack", 2, 0, 3,
                Core.CardType.Spell, Core.CardSubType.Dodge, Core.CardRarity.Common
            ));

            AddCard(new CardData(
                "spell_parry", "Parry", "Counter and deflect", 3, 2, 3,
                Core.CardType.Spell, Core.CardSubType.Parry, Core.CardRarity.Uncommon
            ));

            // ITEM CARDS
            AddCard(new CardData(
                "item_bomb", "Explosive Bomb", "Massive blast damage", 3, 6, 0,
                Core.CardType.Item, Core.CardSubType.Bomb, Core.CardRarity.Uncommon
            ));

            AddCard(new CardData(
                "item_trap", "Bear Trap", "Surprise attack", 2, 3, 0,
                Core.CardType.Item, Core.CardSubType.Trap, Core.CardRarity.Common
            ));

            AddCard(new CardData(
                "item_potion", "Healing Potion", "Restore health", 2, 0, 0,
                Core.CardType.Item, Core.CardSubType.HealingPotion, Core.CardRarity.Common
            ));

            AddCard(new CardData(
                "item_talisman", "Protection Talisman", "Ward off harm", 3, 0, 4,
                Core.CardType.Item, Core.CardSubType.Talisman, Core.CardRarity.Rare
            ));

            GD.Print($"Card Database initialized with {_allCards.Count} cards");
        }

        /// <summary>
        /// Add a card to the database
        /// </summary>
        private void AddCard(CardData card)
        {
            if (!_allCards.ContainsKey(card.CardId))
            {
                _allCards[card.CardId] = card;
            }
        }

        /// <summary>
        /// Get a card by ID
        /// </summary>
        public CardData GetCard(string cardId)
        {
            return _allCards.ContainsKey(cardId) ? _allCards[cardId] : null;
        }

        /// <summary>
        /// Get all cards
        /// </summary>
        public List<CardData> GetAllCards()
        {
            return new List<CardData>(_allCards.Values);
        }

        /// <summary>
        /// Get a starter deck for the player
        /// </summary>
        public List<CardData> GetStarterDeck()
        {
            var deck = new List<CardData>();

            // Balanced starter deck (40 cards)
            // 15 Offensive cards
            for (int i = 0; i < 6; i++) deck.Add(GetCard("sword_1"));
            for (int i = 0; i < 4; i++) deck.Add(GetCard("sword_2"));
            for (int i = 0; i < 3; i++) deck.Add(GetCard("axe_1"));
            for (int i = 0; i < 2; i++) deck.Add(GetCard("hammer_1"));

            // 10 Defensive cards
            for (int i = 0; i < 5; i++) deck.Add(GetCard("shield_1"));
            for (int i = 0; i < 3; i++) deck.Add(GetCard("shield_2"));
            for (int i = 0; i < 2; i++) deck.Add(GetCard("armor_1"));

            // 10 Spell cards
            for (int i = 0; i < 4; i++) deck.Add(GetCard("spell_fireball"));
            for (int i = 0; i < 3; i++) deck.Add(GetCard("spell_dodge"));
            for (int i = 0; i < 2; i++) deck.Add(GetCard("spell_lightning"));
            for (int i = 0; i < 1; i++) deck.Add(GetCard("spell_parry"));

            // 5 Item cards
            for (int i = 0; i < 2; i++) deck.Add(GetCard("item_potion"));
            for (int i = 0; i < 2; i++) deck.Add(GetCard("item_trap"));
            for (int i = 0; i < 1; i++) deck.Add(GetCard("item_bomb"));

            return deck;
        }

        /// <summary>
        /// Get an opponent deck scaled to the level
        /// </summary>
        public List<CardData> GetOpponentDeck(int level)
        {
            var deck = new List<CardData>();

            // Scale difficulty by level
            float difficultyMultiplier = 1.0f + (level * 0.1f);

            // Build opponent deck based on level
            if (level <= 3)
            {
                // Early levels - basic cards
                for (int i = 0; i < 8; i++) deck.Add(GetCard("sword_1"));
                for (int i = 0; i < 5; i++) deck.Add(GetCard("axe_1"));
                for (int i = 0; i < 7; i++) deck.Add(GetCard("shield_1"));
                for (int i = 0; i < 5; i++) deck.Add(GetCard("shield_2"));
                for (int i = 0; i < 6; i++) deck.Add(GetCard("spell_fireball"));
                for (int i = 0; i < 4; i++) deck.Add(GetCard("spell_dodge"));
                for (int i = 0; i < 3; i++) deck.Add(GetCard("item_trap"));
                for (int i = 0; i < 2; i++) deck.Add(GetCard("item_potion"));
            }
            else if (level <= 7)
            {
                // Mid levels - stronger cards
                for (int i = 0; i < 5; i++) deck.Add(GetCard("sword_2"));
                for (int i = 0; i < 5; i++) deck.Add(GetCard("axe_1"));
                for (int i = 0; i < 4; i++) deck.Add(GetCard("axe_2"));
                for (int i = 0; i < 4; i++) deck.Add(GetCard("hammer_1"));
                for (int i = 0; i < 5; i++) deck.Add(GetCard("shield_2"));
                for (int i = 0; i < 4; i++) deck.Add(GetCard("armor_2"));
                for (int i = 0; i < 5; i++) deck.Add(GetCard("spell_lightning"));
                for (int i = 0; i < 3; i++) deck.Add(GetCard("spell_parry"));
                for (int i = 0; i < 3; i++) deck.Add(GetCard("item_bomb"));
                for (int i = 0; i < 2; i++) deck.Add(GetCard("item_talisman"));
            }
            else
            {
                // Late levels and boss - powerful cards
                for (int i = 0; i < 5; i++) deck.Add(GetCard("sword_3"));
                for (int i = 0; i < 5; i++) deck.Add(GetCard("axe_2"));
                for (int i = 0; i < 5; i++) deck.Add(GetCard("hammer_2"));
                for (int i = 0; i < 5; i++) deck.Add(GetCard("shield_3"));
                for (int i = 0; i < 5; i++) deck.Add(GetCard("armor_2"));
                for (int i = 0; i < 5; i++) deck.Add(GetCard("spell_lightning"));
                for (int i = 0; i < 4; i++) deck.Add(GetCard("spell_parry"));
                for (int i = 0; i < 4; i++) deck.Add(GetCard("item_bomb"));
                for (int i = 0; i < 2; i++) deck.Add(GetCard("item_talisman"));
            }

            return deck;
        }
    }
}
