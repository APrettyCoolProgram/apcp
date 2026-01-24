using Godot;
using System;
using System.Collections.Generic;
using DungeonCharlie.Cards;

namespace DungeonCharlie.Gameplay
{
    /// <summary>
    /// Handles combat resolution between players
    /// </summary>
    public partial class CombatSystem : Node
    {
        [Signal]
        public delegate void CombatStartedEventHandler();

        [Signal]
        public delegate void CombatSlotResolvedEventHandler(int slotIndex, int playerDamage, int opponentDamage);

        [Signal]
        public delegate void CombatEndedEventHandler();

        [Signal]
        public delegate void DamageDealtEventHandler(Core.PlayerType target, int amount, string source);

        [Signal]
        public delegate void CombatResolvedEventHandler();

        /// <summary>
        /// Resolve combat between two players
        /// </summary>
        public void ResolveCombat(Player player, Player opponent)
        {
            GD.Print("=== Combat Phase Started ===");
            EmitSignal(SignalName.CombatStarted);

            // Resolve each slot
            for (int i = 0; i < Core.GameConstants.CARD_SLOTS; i++)
            {
                ResolveSlot(i, player, opponent);
            }

            // Clear slots after combat
            player.ClearSlots();
            opponent.ClearSlots();

            GD.Print("=== Combat Phase Ended ===");
            EmitSignal(SignalName.CombatEnded);
            EmitSignal(SignalName.CombatResolved);
        }

        /// <summary>
        /// Resolve a single combat slot
        /// </summary>
        private void ResolveSlot(int slotIndex, Player player, Player opponent)
        {
            var playerCard = player.CardSlots[slotIndex];
            var opponentCard = opponent.CardSlots[slotIndex];

            int playerDamage = 0;
            int opponentDamage = 0;

            if (playerCard != null && opponentCard != null)
            {
                // Both players have cards in this slot
                GD.Print($"Slot {slotIndex}: {playerCard.Data.CardName} vs {opponentCard.Data.CardName}");

                // Calculate damage
                opponentDamage = CalculateDamage(playerCard, opponentCard);
                playerDamage = CalculateDamage(opponentCard, playerCard);

                GD.Print($"  Player deals {opponentDamage} damage, Opponent deals {playerDamage} damage");
            }
            else if (playerCard != null)
            {
                // Only player has a card - direct damage to opponent
                opponentDamage = playerCard.Data.AttackPower;
                GD.Print($"Slot {slotIndex}: {playerCard.Data.CardName} attacks directly for {opponentDamage} damage");
            }
            else if (opponentCard != null)
            {
                // Only opponent has a card - direct damage to player
                playerDamage = opponentCard.Data.AttackPower;
                GD.Print($"Slot {slotIndex}: {opponentCard.Data.CardName} attacks directly for {playerDamage} damage");
            }

            // Apply damage
            if (playerDamage > 0)
            {
                player.TakeDamage(playerDamage);
                string source = opponentCard != null ? opponentCard.Data.CardName : "Unknown";
                EmitSignal(SignalName.DamageDealt, (int)Core.PlayerType.Player, playerDamage, source);
            }

            if (opponentDamage > 0)
            {
                opponent.TakeDamage(opponentDamage);
                string source = playerCard != null ? playerCard.Data.CardName : "Unknown";
                EmitSignal(SignalName.DamageDealt, (int)Core.PlayerType.Opponent, opponentDamage, source);
            }

            EmitSignal(SignalName.CombatSlotResolved, slotIndex, playerDamage, opponentDamage);
        }

        /// <summary>
        /// Calculate damage dealt by attacker to defender
        /// </summary>
        private int CalculateDamage(Card attacker, Card defender)
        {
            int damage = attacker.Data.AttackPower;

            // Apply defensive cards
            if (defender.Data.CardType == Core.CardType.Defensive)
            {
                damage = Mathf.Max(0, damage - defender.Data.DefensePower);
            }

            // Apply special effects for spells and items
            damage = ApplySpecialEffects(attacker, defender, damage);

            return damage;
        }

        /// <summary>
        /// Apply special effects from spells and items
        /// </summary>
        private int ApplySpecialEffects(Card attacker, Card defender, int baseDamage)
        {
            int damage = baseDamage;

            // Example special effects
            switch (attacker.Data.CardType)
            {
                case Core.CardType.Spell:
                    // Spells ignore half of defense
                    damage = baseDamage + (attacker.Data.AttackPower - baseDamage) / 2;
                    break;

                case Core.CardType.Item:
                    // Items have bonus effects
                    if (attacker.Data.CardSubType == Core.CardSubType.Bomb)
                    {
                        damage = (int)(damage * 1.5f); // Bombs do 50% more damage
                    }
                    break;
            }

            return damage;
        }
    }
}
