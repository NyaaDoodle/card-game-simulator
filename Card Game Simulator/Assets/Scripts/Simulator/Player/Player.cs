using System;
using System.Text;
using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SyncVar] private string id;
    [SyncVar] private new string name;
    [SyncVar(hook = nameof(OnScoreChanged))] private int score;
    [SyncVar(hook = nameof(OnIsSpectatingChanged))] private bool isSpectating;

    public event Action<int, int> ScoreChanged;
    public event Action<bool> IsSpectatingChanged;
    
    public string Id
    {
        get => id;
        set => CmdUpdateId(value);
    }

    public string Name
    {
        get => name;
        set => CmdUpdateName(value);
    }

    public int Score
    {
        get => score;
        set => CmdUpdateScore(value);
    }

    public bool IsSpectating
    {
        get => isSpectating;
        set => CmdUpdateIsSpectating(value);
    }

    public void Setup(string id, string name)
    {
        this.id = id;
        this.name = name;
        this.score = 0;
        this.isSpectating = false;
    }

    public void Setup(PlayerData playerData)
    {
        this.id = playerData.Id;
        this.name = playerData.Name;
        this.score = playerData.Score;
        this.isSpectating = playerData.IsSpectating;
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
    public void CmdUpdateId(string newValue)
    {
        id = newValue;
    }

    [Command]
    public void CmdUpdateName(string newValue)
    {
        name = newValue;
    }

    [Command]
    public void CmdUpdateScore(int newValue)
    {
        score = newValue;
    }

    [Command]
    public void CmdUpdateIsSpectating(bool newValue)
    {
        isSpectating = newValue;
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

    protected virtual void OnScoreChanged(int oldValue, int newValue)
    {
        ScoreChanged?.Invoke(oldValue, newValue);
    }

    protected virtual void OnIsSpectatingChanged(bool oldValue, bool newValue)
    {
        IsSpectatingChanged?.Invoke(newValue);
    }
}
