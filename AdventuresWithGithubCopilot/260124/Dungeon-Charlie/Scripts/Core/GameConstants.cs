namespace DungeonCharlie.Core
{
    /// <summary>
    /// Contains all constant values used throughout the game
    /// </summary>
    public static class GameConstants
    {
        // Game progression
        public const int TOTAL_LEVELS = 10;
        public const int BOSS_LEVEL = 11;
        public const int FINAL_BOSS_LEVEL = 11;

        // Player stats
        public const int STARTING_HEALTH = 10;
        public const int STARTING_MANA = 1;
        public const int MANA_INCREMENT = 1;

        // Deck and hand management
        public const int DECK_SIZE = 40;
        public const int STARTING_HAND_SIZE = 5;
        public const int MAX_HAND_SIZE = 10;

        // Battlefield
        public const int CARD_SLOTS = 3;

        // Difficulty scaling (for opponent AI)
        public const float BOSS_HEALTH_MULTIPLIER = 2.0f;
        public const float BOSS_DAMAGE_MULTIPLIER = 1.5f;
    }
}
