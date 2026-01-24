using Godot;
using System;
using System.Collections.Generic;

public partial class Deck : RefCounted
{
    public List<Card> Cards { get; private set; } = new List<Card>();

    private Random _rng = new Random();

    public Deck() { }

    public Deck(IEnumerable<Card> cards)
    {
        Cards = new List<Card>(cards);
    }

    public void Shuffle()
    {
        int n = Cards.Count;
        while (n > 1)
        {
            n--;
            int k = _rng.Next(n + 1);
            var value = Cards[k];
            Cards[k] = Cards[n];
            Cards[n] = value;
        }
    }

    public Card Draw()
    {
        if (Cards.Count == 0)
            return null;
        var c = Cards[0];
        Cards.RemoveAt(0);
        return c;
    }

    public void Add(Card card)
    {
        Cards.Add(card);
    }
}
