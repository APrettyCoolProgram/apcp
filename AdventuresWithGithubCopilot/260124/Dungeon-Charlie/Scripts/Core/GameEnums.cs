using Godot;

namespace DungeonCharlie.Core
{
    /// <summary>
    /// Defines the different types of cards in the game
    /// </summary>
    public enum CardType
    {
        Offensive,  // Weapons: swords, axes, hammers
        Defensive,  // Armor: shields, platemail
        Spell,      // Offensive and defensive spells
        Item        // Offensive and defensive items
    }

    /// <summary>
    /// Defines the subtypes for better card categorization
    /// </summary>
    public enum CardSubType
    {
        // Offensive subtypes
        Sword,
        Axe,
        Hammer,
        Fireball,
        LightningStrike,
        Bomb,
        Trap,
        
        // Defensive subtypes
        Shield,
        Platemail,
        Dodge,
        Parry,
        HealingPotion,
        Talisman
    }

    /// <summary>
    /// Represents the current state of the game
    /// </summary>
    public enum GameState
    {
        MainMenu,
        LevelSelect,
        GameSetup,
        PlayerTurn,
        OpponentTurn,
        Combat,
        LevelComplete,
        BossFight,
        Victory,
        Defeat
    }

    /// <summary>
    /// Represents the turn phase
    /// </summary>
    public enum TurnPhase
    {
        DrawPhase,
        MainPhase,
        CombatPhase,
        EndPhase
    }

    /// <summary>
    /// Identifies the player
    /// </summary>
    public enum PlayerType
    {
        Player,
        Opponent
    }

    /// <summary>
    /// Represents card rarity
    /// </summary>
    public enum CardRarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }
}
