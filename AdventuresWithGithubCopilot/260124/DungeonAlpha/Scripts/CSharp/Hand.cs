using Godot;
using System.Collections.Generic;

public class Hand
{
    public List<Card> Cards { get; private set; } = new List<Card>();
    public void Add(Card card) { if (card!=null) Cards.Add(card); }
    public Card RemoveAt(int index) { var c=Cards[index]; Cards.RemoveAt(index); return c; }
    public int Count => Cards.Count;
}
