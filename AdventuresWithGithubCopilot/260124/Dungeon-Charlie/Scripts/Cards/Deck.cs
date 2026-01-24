using Godot;
using System.Collections.Generic;
using System.Linq;

namespace DungeonCharlie.Cards
{
    /// <summary>
    /// Manages a deck of cards
    /// </summary>
    public partial class Deck : Node
    {
        private List<CardData> _cards = new List<CardData>();
        private List<CardData> _drawPile = new List<CardData>();
        private List<CardData> _discardPile = new List<CardData>();

        public int CardsInDeck => _drawPile.Count;
        public int CardsInDiscard => _discardPile.Count;

        /// <summary>
        /// Initialize the deck with a list of card data
        /// </summary>
        public void Initialize(List<CardData> cards)
        {
            _cards = new List<CardData>(cards);
            _drawPile = new List<CardData>(cards);
            _discardPile.Clear();
            Shuffle();
        }

        /// <summary>
        /// Shuffle the draw pile
        /// </summary>
        public void Shuffle()
        {
            var rng = new System.Random();
            _drawPile = _drawPile.OrderBy(x => rng.Next()).ToList();
            GD.Print($"Deck shuffled. Cards in draw pile: {_drawPile.Count}");
        }

        /// <summary>
        /// Draw a card from the deck
        /// </summary>
        public CardData DrawCard()
        {
            if (_drawPile.Count == 0)
            {
                GD.Print("Draw pile empty. Shuffling discard pile into draw pile.");
                ReshuffleDiscardIntoDraw();
            }

            if (_drawPile.Count == 0)
            {
                GD.Print("No cards available to draw.");
                return null;
            }

            var card = _drawPile[0];
            _drawPile.RemoveAt(0);
            return card;
        }

        /// <summary>
        /// Add a card to the discard pile
        /// </summary>
        public void Discard(CardData card)
        {
            _discardPile.Add(card);
        }

        /// <summary>
        /// Reshuffle the discard pile into the draw pile
        /// </summary>
        private void ReshuffleDiscardIntoDraw()
        {
            _drawPile.AddRange(_discardPile);
            _discardPile.Clear();
            Shuffle();
        }

        /// <summary>
        /// Reset the deck to its initial state
        /// </summary>
        public void Reset()
        {
            _drawPile = new List<CardData>(_cards);
            _discardPile.Clear();
            Shuffle();
        }
    }
}
