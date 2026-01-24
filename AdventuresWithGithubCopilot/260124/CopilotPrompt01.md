# Copilot prompt 01

This prompt was used for:

- Dungeon: Alpha
- Dungeon: Beta
- Dungeon: Charlie

```text
I would like to create a single-player 2D card game called "Dungeon: %Name%"that takes inspiration from the following existing games:

- Magic: the Gathering
- Hearthstone
- Dual Masters
- Slay the Spire
- GWENT
- Monster Train

I would like to use Godot 4.5.1, and play the game on Windows and MacOS.

When generating code, I would prefer .NET 10 C# over GDScript, when possible.

The goal of the game is for the player to work their way through a 10 level dungeon by playing a game of cards against an opponent.

After all 10 levels have been completed, the player encounters a final boss. If the player beats the boss, they win the game.

Gameplay is as such:

- Each player has a deck of 40 cards
- Each player starts with 10 points of health
- Each player starts with a hand of 5 cards, and draws a card every turn
- A player can play as many cards per turn as they are able to
- Each player gets 1 mana on their first turn, and 1 additional mana on subsequent turns
- Each player has three "slots" to place a card
- Once both players have ended their turn, cards in each "slot" fight each other
- If only one player has a card in a slot, their opponent is the target

Card design:

Cards are one of the following types:

1. Offensive - weapons (swords, axes, hammers, etc.) that cause damage to the opponent
2. Defensive - armor (sheilds, platemaile, etc.) that prevent damage to the player
3. Spells - both offensive (fireball, lightning strike, etc.) and defensive (dodge, parry)
4. Items - both offensive (bombs, traps, etc.) and defensive (healing potions, talismans, etc.)

Please document the entire process, including all of my prompts and all of your responses after you have completed work, in a file named "Dungeon-%Name%-Copilot-Documentation.md"
```