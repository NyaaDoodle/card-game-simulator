using System.Text;
using Mirror;

public class Player : NetworkBehaviour
{
    [SyncVar] private int id;
    [SyncVar] private new string name;
    [SyncVar] private int score;
    
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

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("Player");
        stringBuilder.AppendLine($"Id: {Id}");
        stringBuilder.AppendLine($"Name: {Name}");
        stringBuilder.AppendLine($"Score: {Score}");
        return stringBuilder.ToString();
    }

    public override void OnStartLocalPlayer()
    {
        LoggerReferences.Instance.PlayerLogger.LogMethod();
        base.OnStartLocalPlayer();
        ManagerReferences.Instance.SelectionManager.Setup(this);
    }

    [Command]
    public void CmdDrawCard(Stackable stackable)
    {
        LoggerReferences.Instance.PlayerLogger.LogMethod();
        Card drawnCard = stackable.RemoveCardAtStart();
        GetComponent<PlayerHand>().AddCardAtStart(drawnCard);
    }

    [Command]
    public void CmdFlipCard(Stackable stackable)
    {
        LoggerReferences.Instance.PlayerLogger.LogMethod();
        stackable.FlipFirstCard();
    }

    [Command]
    public void CmdShuffleStackable(Stackable stackable)
    {
        LoggerReferences.Instance.PlayerLogger.LogMethod();
        stackable.ShuffleCards();
    }

    [Command]
    public void CmdSearchDeck(Deck deck)
    {
        LoggerReferences.Instance.PlayerLogger.LogMethod();
    }

    [Command]
    public void CmdPlaceCardFaceUp(Card cardToPlace, Stackable destinationStackable)
    {
        LoggerReferences.Instance.PlayerLogger.LogMethod();
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
        LoggerReferences.Instance.PlayerLogger.LogMethod();
        PlayerHand playerHand = GetComponent<PlayerHand>();
        if (playerHand.RemoveCard(cardToPlace))
        {
            destinationStackable.AddCardAtStart(cardToPlace);
            destinationStackable.FlipFirstCardFaceDown();
        }
    }
}
