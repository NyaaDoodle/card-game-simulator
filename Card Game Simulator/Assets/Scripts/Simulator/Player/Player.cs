using System;
using System.Text;
using Mirror;

public class Player : NetworkBehaviour
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Score { get; set; }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("Player");
        stringBuilder.AppendLine($"Id: {Id}");
        stringBuilder.AppendLine($"Name: {Name}");
        stringBuilder.AppendLine($"Score: {Score}");
        return stringBuilder.ToString();
    }

    [Command]
    public void CmdDrawCard(Stackable stackable)
    {
        Card drawnCard = stackable.RemoveCardAtStart();
        GetComponent<PlayerHand>().AddCardAtStart(drawnCard);
    }

    [Command]
    public void CmdFlipCard(Stackable stackable)
    {
        stackable.FlipFirstCard();
    }

    [Command]
    public void CmdShuffleStackable(Stackable stackable)
    {
        stackable.Shuffle();
    }

    [Command]
    public void CmdSearchDeck(Deck deck) {}

    [Command]
    public void CmdPlaceCardFaceUp(Card cardToPlace, Stackable destinationStackable)
    {
        TraceLogger.LogMethod();
        TraceLogger.LogVariable(nameof(cardToPlace), cardToPlace);
        TraceLogger.LogVariable(nameof(destinationStackable), destinationStackable);
        PlayerHand playerHand = GetComponent<PlayerHand>();
        TraceLogger.LogVariable(nameof(playerHand), playerHand);
        if (playerHand.RemoveCard(cardToPlace))
        {
            destinationStackable.AddCardAtStart(cardToPlace);
            destinationStackable.FlipFirstCardFaceUp();
        }
    }

    [Command]
    public void CmdPlaceCardFaceDown(Card cardToPlace, Stackable destinationStackable)
    {
        TraceLogger.LogMethod();
        TraceLogger.LogVariable(nameof(cardToPlace), cardToPlace);
        TraceLogger.LogVariable(nameof(destinationStackable), destinationStackable);
        PlayerHand playerHand = GetComponent<PlayerHand>();
        TraceLogger.LogVariable(nameof(playerHand), playerHand);
        if (playerHand.RemoveCard(cardToPlace))
        {
            destinationStackable.AddCardAtStart(cardToPlace);
            destinationStackable.FlipFirstCardFaceDown();
        }
    }
}
