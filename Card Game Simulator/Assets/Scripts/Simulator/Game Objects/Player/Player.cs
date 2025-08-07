using System;

public readonly struct Player : IEquatable<Player>
{
    public int Id { get; }
    public PlayerHand PlayerHand { get; }

    public Player(int id, PlayerHand playerHand)
    {
        Id = id;
        PlayerHand = playerHand;
    }

    public bool Equals(Player other)
    {
        return other.Id == this.Id && other.PlayerHand == this.PlayerHand;
    }

    public override bool Equals(object obj)
    {
        return obj is Player player && this.Equals(player);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, PlayerHand);
    }

    public static bool operator ==(Player player1, Player player2)
    {
        return player1.Equals(player2);
    }

    public static bool operator !=(Player player1, Player player2)
    {
        return !player1.Equals(player2);
    }
}
