using Godot;
using System;
using System.Collections.Generic;

public class Deck
{
    public List<Card> Cards { get; private set; } = new List<Card>();
    private Random _rng = new Random();

    public Deck() { }

    public Deck(List<Card> cards)
    {
        Cards = new List<Card>(cards);
    }

    public void Shuffle()
    {
        for (int i = Cards.Count - 1; i > 0; i--)
        {
            int j = _rng.Next(i + 1);
            var tmp = Cards[i];
            Cards[i] = Cards[j];
            Cards[j] = tmp;
        }
    }

    public Card Draw()
    {
        if (Cards.Count == 0) return null;
        var c = Cards[0];
        Cards.RemoveAt(0);
        return c;
    }

    public int Count => Cards.Count;
}
