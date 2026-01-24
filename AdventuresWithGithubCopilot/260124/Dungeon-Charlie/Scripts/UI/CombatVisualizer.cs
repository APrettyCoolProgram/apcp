using Godot;
using System.Collections.Generic;
using DungeonCharlie.Cards;
using DungeonCharlie.Gameplay;

namespace DungeonCharlie.UI
{
    /// <summary>
    /// Handles visual feedback for combat events
    /// </summary>
    public partial class CombatVisualizer : Node
    {
        [Export] public PackedScene DamageNumberScene { get; set; }
        
        private Control _playerArea;
        private Control _opponentArea;
        private CombatSystem _combatSystem;
        private Player _player;
        private Player _opponent;
        private RichTextLabel _combatLog;
        private List<string> _logMessages = new List<string>();
        private const int MAX_LOG_MESSAGES = 10;

        public override void _Ready()
        {
            // Get references
            _playerArea = GetNodeOrNull<Control>("../GameUI/PlayerArea");
            _opponentArea = GetNodeOrNull<Control>("../GameUI/OpponentArea");
            _combatSystem = GetNodeOrNull<CombatSystem>("../../GameManager/CombatSystem");
            _player = GetNodeOrNull<Player>("../../GameManager/Player");
            _opponent = GetNodeOrNull<Player>("../../GameManager/Opponent");
            _combatLog = GetNodeOrNull<RichTextLabel>("../GameUI/CombatLog");

            // Connect to combat system signals
            if (_combatSystem != null)
            {
                _combatSystem.DamageDealt += OnDamageDealt;
                _combatSystem.CombatResolved += OnCombatResolved;
            }

            // Connect to player signals
            if (_player != null)
            {
                _player.CardPlayed += (card, slot) => OnCardPlayed(Core.PlayerType.Player, card.Data, slot);
            }

            if (_opponent != null)
            {
                _opponent.CardPlayed += (card, slot) => OnCardPlayed(Core.PlayerType.Opponent, card.Data, slot);
            }
        }

        /// <summary>
        /// Called when damage is dealt
        /// </summary>
        private void OnDamageDealt(Core.PlayerType target, int amount, string source)
        {
            // Spawn damage number
            Control targetArea = target == Core.PlayerType.Player ? _playerArea : _opponentArea;
            if (targetArea != null && DamageNumberScene != null)
            {
                var damageNumber = DamageNumberScene.Instantiate<DamageNumber>();
                targetArea.AddChild(damageNumber);
                
                // Position at center of area
                damageNumber.Position = new Vector2(
                    targetArea.Size.X / 2,
                    targetArea.Size.Y / 2
                );
                
                damageNumber.ShowDamage(amount);
            }

            // Add to log
            string targetName = target == Core.PlayerType.Player ? "Player" : "Opponent";
            AddLogMessage($"[color=red]{targetName} takes {amount} damage from {source}[/color]");
        }

        /// <summary>
        /// Called when a card is played
        /// </summary>
        private void OnCardPlayed(Core.PlayerType player, CardData card, int slot)
        {
            string playerName = player == Core.PlayerType.Player ? "You" : "Opponent";
            string verb = player == Core.PlayerType.Player ? "play" : "plays";
            AddLogMessage($"[color=yellow]{playerName} {verb} [b]{card.CardName}[/b] in slot {slot + 1}[/color]");
        }

        /// <summary>
        /// Called when combat is resolved
        /// </summary>
        private void OnCombatResolved()
        {
            AddLogMessage("[color=cyan]--- Combat Resolved ---[/color]");
        }

        /// <summary>
        /// Show healing number
        /// </summary>
        public void ShowHealing(Core.PlayerType target, int amount)
        {
            Control targetArea = target == Core.PlayerType.Player ? _playerArea : _opponentArea;
            if (targetArea != null && DamageNumberScene != null)
            {
                var damageNumber = DamageNumberScene.Instantiate<DamageNumber>();
                targetArea.AddChild(damageNumber);
                
                damageNumber.Position = new Vector2(
                    targetArea.Size.X / 2,
                    targetArea.Size.Y / 2
                );
                
                damageNumber.ShowHealing(amount);
            }

            string targetName = target == Core.PlayerType.Player ? "Player" : "Opponent";
            AddLogMessage($"[color=green]{targetName} heals {amount} HP[/color]");
        }

        /// <summary>
        /// Add message to combat log
        /// </summary>
        private void AddLogMessage(string message)
        {
            _logMessages.Add(message);
            
            // Keep only recent messages
            if (_logMessages.Count > MAX_LOG_MESSAGES)
            {
                _logMessages.RemoveAt(0);
            }

            // Update log display
            if (_combatLog != null)
            {
                _combatLog.Text = string.Join("\n", _logMessages);
            }
        }

        /// <summary>
        /// Clear combat log
        /// </summary>
        public void ClearLog()
        {
            _logMessages.Clear();
            if (_combatLog != null)
            {
                _combatLog.Text = "";
            }
        }

        /// <summary>
        /// Show effect message
        /// </summary>
        public void ShowEffectMessage(string message)
        {
            AddLogMessage($"[color=magenta]{message}[/color]");
        }
    }
}
