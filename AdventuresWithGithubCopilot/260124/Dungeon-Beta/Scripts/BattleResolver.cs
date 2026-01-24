using Godot;
using System;

public static class BattleResolver
{
    // Resolve fights for three slots between attacker and defender
    public static void ResolveCombat(Player a, Player b)
    {
        for (int i = 0; i < 3; i++)
        {
            var ca = a.Slots[i];
            var cb = b.Slots[i];

            if (ca == null && cb == null)
                continue;

            if (ca != null && cb == null)
            {
                // a's card deals damage to b directly
                b.Health -= ca.Attack;
                ca.Durability = 0; // one-time attack weapon
                continue;
            }

            if (cb != null && ca == null)
            {
                a.Health -= cb.Attack;
                cb.Durability = 0;
                continue;
            }

            // both present: exchange damage between cards
            ca.Durability -= cb.Attack;
            cb.Durability -= ca.Attack;

            // if a card survives and opponent destroyed, remaining card does not spill damage in this simple model
        }

        a.ClearSlotsDestroyed();
        b.ClearSlotsDestroyed();
    }
}
