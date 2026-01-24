<div align="center">

  ![Logo](https://github.com/APrettyCoolProgram/APCP/blob/main/.github/awgc/AdventuresWithGithubCopilot-432x172.png)

  # January 24, 2026

</div>

Adventures:

- [Dungeon: Alpha](#dungeonAlpha)
- [Dungeon: Beta](#dungeonBeta)
- [Dungeon: Charlie](#dungeonCharlie)
- [Dungine]()

## Dungeon: Alpha

### v0.0.0.1

- AI model: GPT-5 mini
- [Initial prompt](CopilotPrompt01.md)
- [Chat documentation](./DungeonAlpha/Dungeon-Alpha-Copilot-Documentation.md)
- [Project files](./DungeonAlpha/)

GPT-5 mini could not fix multiple errors with C#, and the game never ran.

## Dungeon: Beta

### v0.0.0.1

- AI model: GPT-5 mini
- [Initial prompt](CopilotPrompt01.md)
- [Chat documentation](./DungeonAlpha/Dungeon-Beta-Copilot-Documentation.md)
- [Project files](./DungeonBeta/)

Like Dungeon: Alpha, GPT-5 mini could not fix multiple errors with C#, and the game never ran.

## Dungeon: Charlie

### v0.0.0.1

- AI model: Claude Sonnet 4.5
- [Initial prompt](CopilotPrompt01.md)
- [Copilot prompts](./Dungeon-Charlie/Dungeon-Charlie-Copilot-Prompts.md)
- [Chat documentation](./DungeonAlpha/Dungeon-Charlie-Copilot-Documentation.md)
- [Project files](./DungeonCharlie/)

> This used 6.0% of premium requests (7.3% -> 13.3%)

I decided to use Claude Sonnet 4.5 to create the initial framework, and it took longer than GPT-5 mini, but in the end
created something that worked...mostly. The "Start New Game" and "Quit" buttons didnt' work, so Claude walked me
through building the C# project, after which the game started and the UI looked good.

This time I had Copilot keep track of all of my prompts.

Claude was having issues getting the hands of cards to show, and the UI was a little wonky, so I abandoned this attempt.

## Dungine

###  v0.0.0.1

- AI model: Claude Sonnet 4.5, GPT-5 mini
- [Initial prompt](CopilotPrompt02.md)
- [Chat documentation](./Dungine/DungineCopilot-0.0.0.1.md)
- [Project files](./Dungine/)

> This used 2.0% of premium requests (13.3% -> 15.3)

I used Claude Sonnet 4.5 for the heavy lifting, and GPT-5 mini for documentation an minor updates/refactors.

Claude used the initial prompt, and created exactly what was asked for the first time.

As of v0.0.0.2, this has been moved to [here](https://github.com/CalistadalaneGames/Dungine) for further development.
