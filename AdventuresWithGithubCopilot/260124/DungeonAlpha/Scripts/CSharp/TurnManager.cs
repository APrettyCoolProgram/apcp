using Godot;
using System;

public class TurnManager : Node
{
    public int TurnNumber { get; private set; } = 1;
    public int PlayerMana { get; private set; } = 1;
    public int OpponentMana { get; private set; } = 1;

    public override void _Ready()
    {
        PlayerMana = 1;
        OpponentMana = 1;
    }

    // Called at the start of a player's turn. isPlayer true for human player.
    public void StartTurn(bool isPlayer)
    {
        // Mana ramps with turn number, capped at 10
        int mana = Math.Min(10, TurnNumber);
        if (isPlayer) PlayerMana = mana; else OpponentMana = mana;
    }

    // Called when the current turn ends; increments the turn counter for next start
    public void EndTurn()
    {
        TurnNumber++;
    }
}
