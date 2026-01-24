# Dungeon: Alpha — Copilot Documentation

Date: 2026-01-24

This file captures the project spec, conversation history (user prompts and assistant responses), and the steps taken so far while building the single-player 2D card game "Dungeon: Alpha".

---

**Project Summary**

- Title: Dungeon: Alpha
- Engine: Godot 4.5.1
- Target platforms: Windows and macOS
- Scripting preference: C# (preferred over GDScript)
- Game type: Single-player 2D card game inspired by Magic: The Gathering, Hearthstone, Dual Masters, Slay the Spire, GWENT, Monster Train
- Win condition: Player completes 10 dungeon levels and defeats a final boss

**High-level Gameplay Rules (as provided by user)**

- Deck size: 40 cards per player
- Health: Each player starts with 10 health points
- Hand: Each player starts with 5 cards, and draws 1 card at the start of their turn
- Mana: Players get 1 mana on their first turn, and +1 mana on subsequent turns
- Actions: Player may play as many cards per turn as they are able (limited by mana/resources and slot availability)
- Board: Each player has three "slots" to place a card
- Combat resolution: After both players end their turn, cards in each slot fight. If only one player has a card in a slot, that card targets the opposing player directly

**Card Types**

1. Offensive — weapons (swords, axes, hammers) that cause damage
2. Defensive — armor (shields, plate mail) that prevent or absorb damage
3. Spells — offensive (fireball, lightning) and defensive (dodge, parry)
4. Items — offensive (bombs, traps) and defensive (healing potions, talismans)

**Developer Preferences / Constraints**

- Godot version: 4.5.1
- Use C# for gameplay code when possible
- Target: Desktop builds for Windows and macOS

---

**Conversation Log (prompts and assistant responses)**

1) User: "Hi Copilot, how are you?"

Assistant: "Hi — I'm good, thanks! What would you like help with in the project?"

2) User: "I would like to make a card game using Godot. I have written a description, if you would like to see it."

Assistant: "I'm going to create a concise TODO plan to track the card-game project steps." (Created initial TODO list)

3) Assistant (after TODO creation): "Progress: Created the project TODOs and started with reviewing the description. Next: please paste your game description here and tell me which Godot version you want to use (Godot 3.x or 4.x) plus any platform/style constraints."

4) User provided the full game specification (deck size, health, hand size, mana progression, slots, card types, etc.), and requested C# where possible and Godot 4.5.1 targeting Windows/macOS. Also requested: "Please document the entire process, including my prompts and your responses, in a file named \"Dungeon-Alpha-Copilot-Documentation.md\""

Assistant: (this file was created to satisfy that request)

---

**Todo Progress (current)**

- Completed: Review user's game description
- In-progress: Propose features and tech choices
- Pending: Scaffold Godot project and scenes; Implement card data model and deck; Implement turn system and rules; Build UI and input controls; Add placeholder art and audio; Test, iterate, and polish
- Completed: Create project documentation file (`Dungeon-Alpha-Copilot-Documentation.md`)

---

**Next Steps (proposed)**

- Produce a concise list of core gameplay features and tech choices (data model, scene layout, C# project setup, input mapping, build targets)
- Scaffold a minimal Godot 4.5.1 project structure using C# scripts and minimal scenes that demonstrate deck, hand, and slot mechanics
- Implement a simple card model, deck shuffle, draw, and turn manager in C#
- Create a small test scene to verify card placement and slot-based combat resolution

---

If you want me to proceed now, I will propose the initial feature list and concrete implementation choices for the first development sprint.

(End of initial documentation snapshot)

---

**Recent Implementation Progress (added since initial snapshot)**

- Scaffold: created `Scenes/`, `Scripts/CSharp/`, `Data/`, and `Assets/` folders.
- Core C# gameplay scripts added under `Scripts/CSharp/`:
	- `Card.cs` — card data model and `CardType` enum.
	- `Deck.cs` — deck list, `Shuffle()` and `Draw()` methods.
	- `Hand.cs` — simple hand container.
	- `Slot.cs` — represents a single board slot.
	- `TurnManager.cs` — turn and mana management (`StartTurn`, `EndTurn`).
	- `GameController.cs` — orchestrates decks, hands, slots, draw/play flow, simple AI, and combat resolution.
	- `CardLoader.cs` — loads card templates from `Data/cards.json`.
	- `UIController.cs` — simple UI controller for click-to-play hand and slot interactions.

- Scenes and assets:
	- `Scenes/TestScene.tscn` — a minimal test scene wired to `GameController`.
	- `Scenes/UI.tscn` — HUD with hand container, three slot buttons, end-turn button, and mana labels.
	- `Data/cards.json` — sample card templates loaded at runtime.
	- `README.md` — quick run notes.

**Behavioral summary of the current prototype**

- Decks: each player deck is constructed from templates and contains 40 cards (templates repeated) and are shuffled on start.
- Hands: each player starts with 5 cards; one card is drawn at the start of each turn.
- Mana: `TurnManager` ramps mana by turn number (capped at 10). Mana is shown in the UI for both player and opponent.
- Play: click a card in the hand (buttons) to select it, then click a slot button to play the card if enough mana and the slot is empty.
- Combat: when the player ends their turn, the AI plays, then `GameController.ResolveCombat()` resolves each of the three slots (cards fight or hit the opposing player directly).
- AI: simple rule-based opponent that plays the first playable card to the first empty slot.

**Files changed or added (workspace paths)**

- `Scenes/TestScene.tscn` ([Scenes/TestScene.tscn](Scenes/TestScene.tscn))
- `Scenes/UI.tscn` ([Scenes/UI.tscn](Scenes/UI.tscn))
- `Scripts/CSharp/Card.cs` ([Scripts/CSharp/Card.cs](Scripts/CSharp/Card.cs))
- `Scripts/CSharp/Deck.cs` ([Scripts/CSharp/Deck.cs](Scripts/CSharp/Deck.cs))
- `Scripts/CSharp/Hand.cs` ([Scripts/CSharp/Hand.cs](Scripts/CSharp/Hand.cs))
- `Scripts/CSharp/Slot.cs` ([Scripts/CSharp/Slot.cs](Scripts/CSharp/Slot.cs))
- `Scripts/CSharp/TurnManager.cs` ([Scripts/CSharp/TurnManager.cs](Scripts/CSharp/TurnManager.cs))
- `Scripts/CSharp/GameController.cs` ([Scripts/CSharp/GameController.cs](Scripts/CSharp/GameController.cs))
- `Scripts/CSharp/CardLoader.cs` ([Scripts/CSharp/CardLoader.cs](Scripts/CSharp/CardLoader.cs))
- `Scripts/CSharp/UIController.cs` ([Scripts/CSharp/UIController.cs](Scripts/CSharp/UIController.cs))
- `Data/cards.json` ([Data/cards.json](Data/cards.json))
- `README.md` ([README.md](README.md))

**How to run the prototype (quick)**

1. Open the project folder containing `project.godot` in Godot 4.5.1 (with C# support enabled).
2. Open and run the scene `Scenes/TestScene.tscn` to exercise the basic deck/hand flow and UI.

Expected behavior: the scene will print log lines for deck/hand counts, mana values, card plays, and combat resolution. The `UI` shows the player's hand as clickable buttons, three slot buttons, and mana labels.

**Next recommended steps**

- Add health display to the UI and win/lose detection (short-term priority).
- Implement persistent card templates with more varied stats and special effects.
- Replace hand buttons with a visual card UI and add drag-and-drop if desired.
- Add placeholder art and sound effects for feedback.

This documentation file will be kept up-to-date as we progress; I can append new commits/changes or produce a changelog section on each update.

---

**Latest Status (Mono / UI instantiation)**

- The user is running Godot v4.5.1.stable.mono.official.
- After recent edits the `UI.tscn` scene initially failed to instantiate at runtime because Godot could not find the compiled C# classes (`UIController`, `GameController`). This produced errors like:
	- "Cannot instantiate C# script because the associated class could not be found. Script: 'res://Scripts/CSharp/GameController.cs'"
	- "Cannot instantiate C# script because the associated class could not be found. Script: 'res://Scripts/CSharp/UIController.cs'"
- Instantiation warnings followed: several "Parent path './UIRoot' for node '...' has vanished when instantiating" messages. These are secondary effects caused by the C# classes not being available.

Actions taken so far to diagnose and mitigate:

- I adjusted `Scenes/TestScene.tscn` to instance `Scenes/UI.tscn` directly so the UI layout can be inspected without requiring C# compilation.
- I recommended (and documented in chat) a short Mono rebuild procedure: remove the project's managed metadata folder `.godot/mono/metadata`, ensure a compatible .NET SDK is installed, reopen the project with the Godot Mono build, and watch the Output panel for the managed assembly build step. If compile errors appear, capture and paste the Output for analysis.
- The user reported the exact instantiation warnings from the Godot Output; those messages are recorded above.

Recommended next troubleshooting steps (if you want me to continue):

- Try the rebuild steps I suggested (delete `.godot/mono/metadata`, reopen in Godot Mono, wait for "Building managed assemblies..."). If compilation fails, copy the full Output log and paste it here.
- If you prefer an immediate runnable prototype without dealing with Mono now, I can convert the core C# scripts to GDScript so the full gameplay flow runs without C# dependencies. This is reversible — we can keep the C# sources and maintain both variants if desired.

I'll keep this document live and append the exact Output logs and any code changes made while we fix the Mono build or port to GDScript.

---

**User's .NET SDK Info (from `dotnet --info`)**

- Installed SDKs: `8.0.417`, `9.0.309`, `10.0.102`.
- Runtimes include `Microsoft.NETCore.App` versions 6.0.11, 8.0.23, 9.0.12, 10.0.2.
- Host: Version `10.0.2` (x64).
- Environment: Windows (reported OS Version: 10.0.26200).

This confirms a modern .NET SDK is present. Godot Mono should be able to build managed assemblies, but the project's managed metadata may need to be regenerated.

**Recommended immediate steps (rebuild managed metadata)**

1. Close Godot.
2. Delete the project's Mono metadata folder: `.godot/mono/metadata`.
3. Reopen the project with your Godot Mono executable (v4.5.1.stable.mono.official).
4. Watch the Output panel for "Building managed assemblies..." and any compiler errors. If errors appear, copy the compile output into this document or paste it in chat.

If you prefer a temporary visual check instead of fixing Mono now, I can remove the C# script reference from `Scenes/UI.tscn` so the UI will instantiate without compiled C#; this is reversible once Mono is fixed.
