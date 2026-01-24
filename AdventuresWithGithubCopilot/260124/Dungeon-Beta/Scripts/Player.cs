using Godot;
using System;
using System.Collections.Generic;

public partial class Player : RefCounted
{
    public string Name { get; set; } = "Player";
    public Deck Deck { get; set; }
    public List<Card> Hand { get; private set; } = new List<Card>();
    public Card[] Slots { get; private set; } = new Card[3];
    public int Health { get; set; } = 10;
    public int MaxMana { get; set; } = 0;
    public int CurrentMana { get; private set; } = 0;

    public Player() { }

    public Player(string name, Deck deck)
    {
        Name = name;
        Deck = deck;
    }

    public void DrawCard()
    {
        var c = Deck?.Draw();
        if (c != null)
            Hand.Add(c);
    }

    public void StartTurn()
    {
        MaxMana = Math.Min(10, MaxMana + 1);
        CurrentMana = MaxMana;
        DrawCard();
    }

    public bool PlayCardToSlot(int handIndex, int slotIndex)
    {
        if (handIndex < 0 || handIndex >= Hand.Count)
            return false;
        if (slotIndex < 0 || slotIndex >= Slots.Length)
            return false;
        var card = Hand[handIndex];
        if (card.Cost > CurrentMana)
            return false;
        if (Slots[slotIndex] != null)
            return false;

        CurrentMana -= card.Cost;
        Slots[slotIndex] = card;
        Hand.RemoveAt(handIndex);
        return true;
    }

    public void ClearSlotsDestroyed()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i] != null && Slots[i].Durability <= 0)
                Slots[i] = null;
        }
    }
}
