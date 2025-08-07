using System;
using System.Collections.Generic;
using System.Linq;

public readonly struct Space : IEquatable<Space>
{
    public SpaceData SpaceData { get; }
    public Card[] Cards { get; }

    public Space(SpaceData spaceData, IEnumerable<Card> cards)
    {
        SpaceData = spaceData;
        Cards = cards.ToArray();
    }

    public bool Equals(Space other)
    {
        return other.SpaceData == this.SpaceData && other.Cards.SequenceEqual(this.Cards);
    }

    public override bool Equals(object obj)
    {
        return obj is Space space && this.Equals(space);
    }

    public override int GetHashCode()
    {
        HashCode hashCode = new HashCode();
        hashCode.Add(SpaceData);
        if (Cards != null)
        {
            foreach (Card card in Cards)
            {
                hashCode.Add(card);
            }
        }
        return hashCode.ToHashCode();
    }

    public static bool operator ==(Space space1, Space space2)
    {
        return space1.Equals(space2);
    }

    public static bool operator !=(Space space1, Space space2)
    {
        return !space1.Equals(space2);
    }
}
