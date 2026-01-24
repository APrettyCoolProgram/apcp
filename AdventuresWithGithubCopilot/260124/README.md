# January 24, 2026

- [Dungeon: Alpha](#dungeon-alpha)
- [Dungeon: Beta](#dungeon-beta)
- [Dungeon: Charlie](#dungeon-charlie)
- [Dungine]()

## Project setup

1. Created a new empty Godot project
2. Opened the project folder in Visual Studio Code
3. Confirmed connection to Godot

## Dungeon: Alpha

- AI model: GPT-5 mini
- [Initial prompt](CopilotPrompt01.md)
- [Chat documentation](./DungeonAlpha/Dungeon-Alpha-Copilot-Documentation.md)
- [Project files](./DungeonAlpha/)

GPT-5 mini could not fix multiple errors with C#, and the game never ran.

## Dungeon: Beta

- AI model: GPT-5 mini
- [Initial prompt](CopilotPrompt01.md)
- [Chat documentation](./DungeonAlpha/Dungeon-Beta-Copilot-Documentation.md)
- [Project files](./DungeonBeta/)

Like Dungeon: Alpha, GPT-5 mini could not fix multiple errors with C#, and the game never ran.

## Dungeon: Charlie

- AI model: Claude Sonnet 4.5
- [Initial prompt](CopilotPrompt01.md)
- [Copilot prompts]()
- [Chat documentation](./DungeonAlpha/Dungeon-Charlie-Copilot-Documentation.md)
- [Project files](./DungeonCharlie/)

This used 6.0% of premium requests (7.3% -> 13.3%)

I decided to use Claude Sonnet 4.5 to create the initial framework, and it took longer than GPT-5 mini, but in the end
created something that worked...mostly. The "Start New Game" and "Quit" buttons didnt' work, so Claude walked me
through building the C# project, after which the game started and the UI looked good.

This time I had Copilot keep track of all of my prompts.

Claude was having issues getting the hands of cards to show, and the UI was a little wonky, so I abandoned this attempt.


