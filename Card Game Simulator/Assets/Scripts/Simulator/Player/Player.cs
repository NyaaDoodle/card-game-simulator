using Mirror;

public class Player : NetworkBehaviour
{
    [SyncVar] private int id;
    [SyncVar] private new string name = "Player";
    [SyncVar] private int score = 0;

    public int Id
    {
        get
        {
            return id;
        }
        private set
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
        private set
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
        private set
        {
            score = value;
        }
    }

    public void Setup(int id, string name, int startingScore)
    {
        Id = id;
        Name = name;
        Score = startingScore;
    }

    public void SetScore(int newScore)
    {
        Score = newScore;
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
    public void CmdSearchDeck(Deck deck)
    {

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
}
