using System;

public readonly struct Card : IEquatable<Card>
{
    public CardData CardData { get; }
    public bool IsFaceUp { get; }

    public Card(CardData cardData)
    {
        CardData = cardData;
        IsFaceUp = false;
    }

    public bool Equals(Card other) => other.CardData == this.CardData && other.IsFaceUp == this.IsFaceUp;

    public override bool Equals(object obj) => obj is Card card && Equals(card);

    public override int GetHashCode() => HashCode.Combine(CardData, IsFaceUp);

    public static bool operator ==(Card card1, Card card2)
    {
        return card1.Equals(card2);
    }

    public static bool operator !=(Card card1, Card card2)
    {
        return !card1.Equals(card2);
    }
}
