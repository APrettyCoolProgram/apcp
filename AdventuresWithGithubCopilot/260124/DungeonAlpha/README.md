Dungeon: Alpha — Godot prototype

Requirements:
- Godot 4.5.1 (C# enabled)
- .NET runtime supported by Godot (e.g., .NET 7 or .NET 8, depending on your Godot build)

Opening the project:
1. Launch Godot and open the folder containing `project.godot`.
2. Open `Scenes/TestScene.tscn` and run the scene to exercise the basic deck/hand flow.

Notes:
- C# scripts are under `Scripts/CSharp/`.
- Card templates are in `Data/cards.json` (not yet fully wired to runtime loader).
- Next steps: hook UI elements to `GameController`, implement drag-and-drop for cards, and load card data from JSON.
