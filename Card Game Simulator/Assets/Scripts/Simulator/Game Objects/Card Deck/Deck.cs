using System;
using System.Collections.Generic;
using System.Linq;

public readonly struct Deck : IEquatable<Deck>
{
    public DeckData DeckData { get; }
    public Card[] Cards { get; }

    public Deck(DeckData deckData, IEnumerable<Card> cards)
    {
        DeckData = deckData;
        Cards = cards.ToArray();
    }

    public bool Equals(Deck other)
    {
        return other.DeckData == this.DeckData && other.Cards.SequenceEqual(this.Cards);
    }

    public override bool Equals(object obj)
    {
        return obj is Deck deck && this.Equals(deck);
    }

    public override int GetHashCode()
    {
        HashCode hashCode = new HashCode();
        hashCode.Add(DeckData);
        if (Cards != null)
        {
            foreach (Card card in Cards)
            {
                hashCode.Add(card);
            }
        }
        return hashCode.ToHashCode();
    }

    public static bool operator ==(Deck deck1, Deck deck2)
    {
        return deck1.Equals(deck2);
    }

    public static bool operator !=(Deck deck1, Deck deck2)
    {
        return !deck1.Equals(deck2);
    }
}
