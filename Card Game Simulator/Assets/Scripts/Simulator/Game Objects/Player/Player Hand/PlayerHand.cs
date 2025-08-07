using System;
using System.Collections.Generic;
using System.Linq;

public readonly struct PlayerHand : IEquatable<PlayerHand>
{
    public Card[] Cards { get; }

    public PlayerHand(IEnumerable<Card> cards)
    {
        Cards = cards.ToArray();
    }

    public bool Equals(PlayerHand other)
    {
        return other.Cards.SequenceEqual(this.Cards);
    }

    public override bool Equals(object obj)
    {
        return obj is PlayerHand playerHand && this.Equals(playerHand);
    }

    public override int GetHashCode()
    {
        HashCode hashCode = new HashCode();
        if (Cards != null)
        {
            foreach (Card card in Cards)
            {
                hashCode.Add(card);
            }
        }
        return hashCode.ToHashCode();
    }

    public static bool operator ==(PlayerHand playerHand1, PlayerHand playerHand2)
    {
        return playerHand1.Equals(playerHand2);
    }

    public static bool operator !=(PlayerHand playerHand1, PlayerHand playerHand2)
    {
        return !playerHand1.Equals(playerHand2);
    }
}