using Godot;
using System;
using System.Collections.Generic;
using DungeonCharlie.Cards;

namespace DungeonCharlie.Gameplay
{
    /// <summary>
    /// Main game manager that controls the flow of the game
    /// </summary>
    public partial class GameManager : Node
    {
        private static GameManager _instance;
        public static GameManager Instance => _instance;

        [Export] public Player Player { get; private set; }
        [Export] public Player Opponent { get; private set; }
        [Export] public CombatSystem CombatSystem { get; private set; }
        [Export] public OpponentAI OpponentAI { get; private set; }

        private UI.VictoryScreen _victoryScreen;
        private UI.DefeatScreen _defeatScreen;
        private UI.LevelProgressionScreen _levelProgressionScreen;

        public Core.GameState CurrentState { get; private set; }
        public Core.TurnPhase CurrentPhase { get; private set; }
        public int CurrentLevel { get; private set; } = 1;
        public bool IsBossFight { get; private set; }

        private Player _currentTurnPlayer;

        [Signal]
        public delegate void GameStateChangedEventHandler(Core.GameState newState);

        [Signal]
        public delegate void TurnPhaseChangedEventHandler(Core.TurnPhase newPhase);

        [Signal]
        public delegate void TurnStartedEventHandler(Core.PlayerType playerType);

        [Signal]
        public delegate void LevelCompleteEventHandler(int level);

        public override void _EnterTree()
        {
            _instance = this;
        }

        public override void _Ready()
        {
            // Get references
            Player = GetNodeOrNull<Player>("Player");
            Opponent = GetNodeOrNull<Player>("Opponent");
            CombatSystem = GetNodeOrNull<CombatSystem>("CombatSystem");
            OpponentAI = GetNodeOrNull<OpponentAI>("OpponentAI");

            // Get screen references
            _victoryScreen = GetNodeOrNull<UI.VictoryScreen>("../VictoryScreen");
            _defeatScreen = GetNodeOrNull<UI.DefeatScreen>("../DefeatScreen");
            _levelProgressionScreen = GetNodeOrNull<UI.LevelProgressionScreen>("../LevelProgressionScreen");

            // Connect screen signals
            if (_victoryScreen != null)
            {
                _victoryScreen.ContinueRequested += OnVictoryContinue;
                _victoryScreen.MainMenuRequested += OnMainMenuRequested;
            }

            if (_defeatScreen != null)
            {
                _defeatScreen.RetryRequested += OnDefeatRetry;
                _defeatScreen.MainMenuRequested += OnMainMenuRequested;
            }

            if (_levelProgressionScreen != null)
            {
                _levelProgressionScreen.StartLevelRequested += OnProgressionStart;
            }

            // Set player types
            if (Player != null) Player.PlayerType = Core.PlayerType.Player;
            if (Opponent != null) Opponent.PlayerType = Core.PlayerType.Opponent;

            // Auto-start the game when this scene loads
            CallDeferred(MethodName.StartNewGame);
        }

        /// <summary>
        /// Start a new game
        /// </summary>
        public void StartNewGame()
        {
            CurrentLevel = 1;
            StartLevel(1);
        }

        /// <summary>
        /// Start a specific level
        /// </summary>
        public void StartLevel(int level)
        {
            CurrentLevel = level;
            IsBossFight = (level == Core.GameConstants.FINAL_BOSS_LEVEL);

            GD.Print($"Starting Level {level}" + (IsBossFight ? " (BOSS FIGHT)" : ""));

            // Get CardDatabase
            var cardDatabase = GetNode<CardDatabase>("/root/Game/CardDatabase");
            if (cardDatabase == null)
            {
                GD.PrintErr("CardDatabase not found!");
                return;
            }

            // Initialize players
            var playerDeck = cardDatabase.GetStarterDeck();
            var opponentDeck = cardDatabase.GetOpponentDeck(level);

            Player.Initialize(playerDeck, false);
            Opponent.Initialize(opponentDeck, IsBossFight);

            // Initialize AI
            OpponentAI?.Initialize(Opponent, Player, level);

            // Start the game
            ChangeState(Core.GameState.GameSetup);
            SetupGame();
        }

        /// <summary>
        /// Setup the game
        /// </summary>
        private void SetupGame()
        {
            // Draw starting hands
            Player.DrawStartingHand();
            Opponent.DrawStartingHand();

            // Start with player turn
            _currentTurnPlayer = Player;
            ChangeState(Core.GameState.PlayerTurn);
            StartTurn();
        }

        /// <summary>
        /// Change the game state
        /// </summary>
        private void ChangeState(Core.GameState newState)
        {
            CurrentState = newState;
            EmitSignal(SignalName.GameStateChanged, (int)newState);
            GD.Print($"Game State: {newState}");

            switch (newState)
            {
                case Core.GameState.Victory:
                    HandleVictory();
                    break;
                case Core.GameState.Defeat:
                    HandleDefeat();
                    break;
            }
        }

        /// <summary>
        /// Start a new turn
        /// </summary>
        private void StartTurn()
        {
            CurrentPhase = Core.TurnPhase.DrawPhase;
            EmitSignal(SignalName.TurnStarted, (int)_currentTurnPlayer.PlayerType);

            _currentTurnPlayer.StartTurn();

            // Move to main phase
            CurrentPhase = Core.TurnPhase.MainPhase;
            EmitSignal(SignalName.TurnPhaseChanged, (int)CurrentPhase);

            // If it's the opponent's turn, let the AI play
            if (_currentTurnPlayer.PlayerType == Core.PlayerType.Opponent)
            {
                OpponentAI?.TakeTurn();
            }
        }

        /// <summary>
        /// End the current turn
        /// </summary>
        public void EndCurrentTurn()
        {
            if (_currentTurnPlayer == null) return;

            _currentTurnPlayer.EndTurn();

            // Check if both players have ended their turn
            if (Player.HasEndedTurn && Opponent.HasEndedTurn)
            {
                // Move to combat phase
                ChangeState(Core.GameState.Combat);
                CurrentPhase = Core.TurnPhase.CombatPhase;
                EmitSignal(SignalName.TurnPhaseChanged, (int)CurrentPhase);

                ResolveCombat();
            }
            else
            {
                // Switch to the other player
                _currentTurnPlayer = (_currentTurnPlayer.PlayerType == Core.PlayerType.Player) ? Opponent : Player;
                ChangeState((_currentTurnPlayer.PlayerType == Core.PlayerType.Player) ? 
                           Core.GameState.PlayerTurn : Core.GameState.OpponentTurn);
                StartTurn();
            }
        }

        /// <summary>
        /// Resolve combat phase
        /// </summary>
        private void ResolveCombat()
        {
            CombatSystem?.ResolveCombat(Player, Opponent);

            // Check for victory/defeat conditions
            if (Player.IsDefeated())
            {
                ChangeState(Core.GameState.Defeat);
                return;
            }

            if (Opponent.IsDefeated())
            {
                if (IsBossFight)
                {
                    ChangeState(Core.GameState.Victory);
                }
                else
                {
                    ChangeState(Core.GameState.LevelComplete);
                    EmitSignal(SignalName.LevelComplete, CurrentLevel);
                }
                return;
            }

            // Start a new round
            Player.HasEndedTurn = false;
            Opponent.HasEndedTurn = false;
            _currentTurnPlayer = Player;
            ChangeState(Core.GameState.PlayerTurn);
            StartTurn();
        }

        /// <summary>
        /// Handle victory
        /// </summary>
        private void HandleVictory()
        {
            GD.Print("=== VICTORY! ===");
            if (_victoryScreen != null)
            {
                _victoryScreen.Show(CurrentLevel, Player.Health, Opponent.Health, IsBossFight);
            }
        }

        /// <summary>
        /// Handle defeat
        /// </summary>
        private void HandleDefeat()
        {
            GD.Print("=== DEFEAT ===");
            if (_defeatScreen != null)
            {
                _defeatScreen.Show(CurrentLevel, IsBossFight);
            }
        }

        /// <summary>
        /// Progress to the next level
        /// </summary>
        public void NextLevel()
        {
            if (CurrentLevel < Core.GameConstants.FINAL_BOSS_LEVEL)
            {
                StartLevel(CurrentLevel + 1);
            }
        }

        /// <summary>
        /// Restart the current level
        /// </summary>
        public void RestartLevel()
        {
            StartLevel(CurrentLevel);
        }

        /// <summary>
        /// Called when player continues after victory
        /// </summary>
        private void OnVictoryContinue()
        {
            if (IsBossFight)
            {
                // Game complete - return to main menu
                OnMainMenuRequested();
            }
            else
            {
                // Show progression screen
                if (_levelProgressionScreen != null)
                {
                    _levelProgressionScreen.Show(CurrentLevel + 1, Player.Health);
                }
            }
        }

        /// <summary>
        /// Called when player retries after defeat
        /// </summary>
        private void OnDefeatRetry()
        {
            RestartLevel();
        }

        /// <summary>
        /// Called when progression screen starts next level
        /// </summary>
        private void OnProgressionStart()
        {
            NextLevel();
        }

        /// <summary>
        /// Return to main menu
        /// </summary>
        private void OnMainMenuRequested()
        {
            GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
        }
    }
}