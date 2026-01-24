using Godot;
using System;
using System.Collections.Generic;
using DungeonCharlie.Cards;

namespace DungeonCharlie.Gameplay
{
    /// <summary>
    /// AI controller for the opponent
    /// </summary>
    public partial class OpponentAI : Node
    {
        private Player _opponent;
        private Player _player;
        private int _difficultyLevel = 1;

        public void Initialize(Player opponent, Player player, int level)
        {
            _opponent = opponent;
            _player = player;
            _difficultyLevel = level;
        }

        /// <summary>
        /// Make AI decisions for the opponent's turn
        /// </summary>
        public void TakeTurn()
        {
            GD.Print($"Opponent AI taking turn (Level {_difficultyLevel})");

            // Simple AI logic:
            // 1. Play the highest cost cards that fit the available mana
            // 2. Fill slots from left to right
            // 3. Prioritize offensive cards

            var playableCards = new List<Card>();
            foreach (var card in _opponent.Hand)
            {
                if (card.Data.ManaCost <= _opponent.CurrentMana)
                {
                    playableCards.Add(card);
                }
            }

            // Sort by priority: offensive first, then by cost (descending)
            playableCards.Sort((a, b) =>
            {
                int priorityA = GetCardPriority(a);
                int priorityB = GetCardPriority(b);

                if (priorityA != priorityB)
                    return priorityB.CompareTo(priorityA);

                return b.Data.ManaCost.CompareTo(a.Data.ManaCost);
            });

            // Try to play cards
            int slotIndex = 0;
            foreach (var card in playableCards)
            {
                if (slotIndex >= Core.GameConstants.CARD_SLOTS)
                    break;

                // Find next available slot
                while (slotIndex < Core.GameConstants.CARD_SLOTS && _opponent.CardSlots[slotIndex] != null)
                {
                    slotIndex++;
                }

                if (slotIndex < Core.GameConstants.CARD_SLOTS)
                {
                    _opponent.PlayCard(card, slotIndex);
                    slotIndex++;
                }
            }

            // End turn after a short delay
            var timer = GetTree().CreateTimer(1.0f);
            timer.Timeout += () => _opponent.EndTurn();
        }

        /// <summary>
        /// Get priority value for a card (higher is better)
        /// </summary>
        private int GetCardPriority(Card card)
        {
            int priority = 0;

            switch (card.Data.CardType)
            {
                case Core.CardType.Offensive:
                    priority = 100 + card.Data.AttackPower;
                    break;
                case Core.CardType.Spell:
                    priority = 80 + card.Data.AttackPower;
                    break;
                case Core.CardType.Item:
                    priority = 60 + card.Data.AttackPower;
                    break;
                case Core.CardType.Defensive:
                    // Defensive cards get higher priority if we're low on health
                    float healthPercent = (float)_opponent.Health / _opponent.MaxHealth;
                    if (healthPercent < 0.5f)
                        priority = 90 + card.Data.DefensePower;
                    else
                        priority = 40 + card.Data.DefensePower;
                    break;
            }

            // Scale by difficulty
            priority = (int)(priority * (1.0f + (_difficultyLevel * 0.1f)));

            return priority;
        }
    }
}
