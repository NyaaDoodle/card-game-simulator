using System.Text;
using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SyncVar] private int id;
    [SyncVar] private new string name;
    [SyncVar] private int score;
    [SyncVar] private bool isSpectating;
    
    public int Id
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }

    public bool IsSpectating
    {
        get
        {
            return isSpectating;
        }
        set
        {
            isSpectating = value;
        }
    }

    public void Setup(int id, string name, int startingScore, bool isSpectating)
    {
        this.id = id;
        this.name = name;
        this.score = startingScore;
        this.isSpectating = isSpectating;
    }

    public void Setup(int id, string name, int startingScore)
    {
        this.id = id;
        this.name = name;
        this.score = startingScore;
        this.isSpectating = false;
    }

    public void Setup(int id, string name)
    {
        this.id = id;
        this.name = name;
        this.score = 0;
        this.isSpectating = false;
    }

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
        GetComponent<PlayerHand>().AddCardAtEnd(drawnCard);
    }

    [Command]
    public void CmdFlipCard(Stackable stackable)
    {
        stackable.FlipFirstCard();
    }

    [Command]
    public void CmdShuffleStackable(Stackable stackable)
    {
        stackable.ShuffleCards();
    }

    [Command]
    public void CmdTakeCard(Stackable stackable, Card selectedCard)
    {
        if (stackable.RemoveCard(selectedCard))
        {
            GetComponent<PlayerHand>().AddCardAtEnd(selectedCard);
        }
    }

    [Command]
    public void CmdPlaceCardFaceUp(Card cardToPlace, Stackable destinationStackable)
    {
        PlayerHand playerHand = GetComponent<PlayerHand>();
        if (playerHand.RemoveCard(cardToPlace))
        {
            destinationStackable.AddCardAtStart(cardToPlace);
            destinationStackable.FlipFirstCardFaceUp();
        }
    }

    [Command]
    public void CmdPlaceCardFaceDown(Card cardToPlace, Stackable destinationStackable)
    {
        PlayerHand playerHand = GetComponent<PlayerHand>();
        if (playerHand.RemoveCard(cardToPlace))
        {
            destinationStackable.AddCardAtStart(cardToPlace);
            destinationStackable.FlipFirstCardFaceDown();
        }
    }

    [Command]
    public void CmdTakeAllCards(Stackable stackable)
    {
        Debug.Log("Take all on server");
        PlayerHand playerHand = GetComponent<PlayerHand>();
        while (stackable.Cards.Count > 0)
        {
            Card takenCard = stackable.RemoveCardAtStart();
            playerHand.AddCardAtEnd(takenCard);
        }
    }

    [Command]
    public void CmdTransferCards(Stackable origin, Stackable destination)
    {
        Debug.Log("Transfer on server");
        while (origin.Cards.Count > 0)
        {
            destination.AddCardAtStart(origin.RemoveCardAtEnd());
        }
    }
}
