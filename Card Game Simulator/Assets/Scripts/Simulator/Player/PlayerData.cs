using System.Collections.Generic;
using System.Linq;

public readonly struct PlayerData
{
    public string Id { get; }
    public string Name { get; }
    public int Score { get; }
    public bool IsSpectating { get; }
    public Card[] CardsInHand { get; }

    public PlayerData(string id, string name, int score, bool isSpectating, IEnumerable<Card> cardsInHand)
    {
        Id = id;
        Name = name;
        Score = score;
        IsSpectating = isSpectating;
        CardsInHand = cardsInHand.ToArray();
    }

    public PlayerData(Player player, PlayerHand playerHand)
    {
        Id = player.Id;
        Name = player.Name;
        Score = player.Score;
        IsSpectating = player.IsSpectating;
        CardsInHand = playerHand.Cards.ToArray();
    }
}
