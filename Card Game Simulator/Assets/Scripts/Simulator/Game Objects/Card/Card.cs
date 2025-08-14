using System;
using System.Text;

public readonly struct Card : IEquatable<Card>
{
    public CardData CardData { get; }
    public bool IsFaceUp { get; }
    public float Rotation { get; }

    public Card(CardData cardData, bool isFaceUp, float rotation)
    {
        CardData = cardData;
        IsFaceUp = isFaceUp;
        Rotation = rotation;
    }

    public Card(CardData cardData, bool isFaceUp)
    {
        CardData = cardData;
        IsFaceUp = isFaceUp;
        Rotation = 0;
    }

    public Card(CardData cardData)
    {
        CardData = cardData;
        IsFaceUp = false;
        Rotation = 0;
    }

    public Card Flipped()
    {
        return new Card(this.CardData, !this.IsFaceUp, this.Rotation);
    }

    public Card FaceDown()
    {
        return new Card(this.CardData, false, this.Rotation);
    }

    public Card FaceUp()
    {
        return new Card(this.CardData, true, this.Rotation);
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

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine(CardData.ToString());
        stringBuilder.AppendLine("Card");
        stringBuilder.AppendLine($"IsFaceUp: {IsFaceUp}");
        stringBuilder.AppendLine($"Rotation: {Rotation}");
        return stringBuilder.ToString();
    }
}
