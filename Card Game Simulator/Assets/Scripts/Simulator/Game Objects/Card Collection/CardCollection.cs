using System;
using System.Collections.Generic;
using System.Text;
using Mirror;
using UnityEngine;

public class CardCollection : NetworkBehaviour
{
    private readonly SyncList<Card> cards = new SyncList<Card>();

    // Events
    public event Action<CardCollection, Card, int> CardAdded;
    public event Action<CardCollection, Card, int> CardRemoved;
    public event Action<CardCollection> CardsCleared;

    public SyncList<Card> Cards => cards;
    public bool IsEmpty => cards.Count <= 0;
    public Card FirstCard => cards[0];

    public override void OnStartClient()
    {
        LoggingManager.Instance.CardCollectionLogger.LogMethod();
        SubscribeToCardsEvents();
    }

    public override void OnStopClient()
    {
        LoggingManager.Instance.CardCollectionLogger.LogMethod();
        UnsubscribeFromCardsEvents();
    }

    protected virtual void SubscribeToCardsEvents()
    {
        LoggingManager.Instance.CardCollectionLogger.LogMethod();
        cards.OnAdd += OnCardsItemAdded;
        cards.OnInsert += OnCardsItemInserted;
        cards.OnSet += OnCardsItemSet;
        cards.OnRemove += OnCardsItemRemoved;
        cards.OnClear += OnCardsCleared;
    }

    protected virtual void UnsubscribeFromCardsEvents()
    {
        LoggingManager.Instance.CardCollectionLogger.LogMethod();
        cards.OnAdd -= OnCardsItemAdded;
        cards.OnInsert -= OnCardsItemInserted;
        cards.OnSet -= OnCardsItemSet;
        cards.OnRemove -= OnCardsItemRemoved;
        cards.OnClear -= OnCardsCleared;
    }

    protected virtual void OnCardsItemAdded(int index)
    {
        // When a card is added to the end of cards, using Add()
        LoggingManager.Instance.CardCollectionLogger.LogMethod();
        OnCardAdded(cards[index], index);
    }

    protected virtual void OnCardsItemInserted(int index)
    {
        // When a card is inserted/added at index in cards, using Insert()
        LoggingManager.Instance.CardCollectionLogger.LogMethod();
        OnCardAdded(cards[index], index);
    }

    protected virtual void OnCardsItemSet(int index, Card oldCard)
    {
        // When a card is replaced at index in cards, using [], gives the old/replaced card
        LoggingManager.Instance.CardCollectionLogger.LogMethod();
        OnCardRemoved(oldCard, index);
        OnCardAdded(cards[index], index);
    }

    protected virtual void OnCardsItemRemoved(int index, Card oldCard)
    {
        // When a card is removed at index in cards, using Remove(), gives the removed card
        LoggingManager.Instance.CardCollectionLogger.LogMethod();
        OnCardRemoved(oldCard, index);
    }

    protected virtual void OnCardsCleared()
    {
        // When cards is cleared, using Clear()
        LoggingManager.Instance.CardCollectionLogger.LogMethod();
        CardsCleared?.Invoke(this);
    }

    protected virtual void OnCardAdded(Card cardAdded, int index)
    {
        LoggingManager.Instance.CardCollectionLogger.LogMethod();
        CardAdded?.Invoke(this, cardAdded, index);
    }

    protected virtual void OnCardRemoved(Card cardRemoved, int index)
    {
        LoggingManager.Instance.CardCollectionLogger.LogMethod();
        CardRemoved?.Invoke(this, cardRemoved, index);
    }

    [Server]
    public virtual void AddCard(Card card, int index)
    {
        LoggingManager.Instance.CardCollectionLogger.LogMethod();
        try
        {
            cards.Insert(index, card);
        }
        catch (ArgumentOutOfRangeException)
        {
            Debug.LogWarning($"Insertion index {index} is out of bounds in Cards");
        }
    }

    [Server]
    public virtual void AddCardAtStart(Card card)
    {
        LoggingManager.Instance.CardCollectionLogger.LogMethod();
        AddCard(card, 0);
    }

    [Server]
    public virtual void AddCardAtEnd(Card card)
    {
        LoggingManager.Instance.CardCollectionLogger.LogMethod();
        AddCard(card, cards.Count);
    }

    [Server]
    public virtual void AddCards(IEnumerable<Card> cardsToAdd)
    {
        LoggingManager.Instance.CardCollectionLogger.LogMethod();
        foreach (Card card in cardsToAdd)
        {
            AddCardAtEnd(card);
        }
    }

    [Server]
    public virtual bool RemoveCard(Card card)
    {
        LoggingManager.Instance.CardCollectionLogger.LogMethod();
        int cardIndex = cards.IndexOf(card);
        if (cardIndex < 0)
        {
            Debug.LogWarning("Specified card for deletion was not found");
            return false;
        }
        cards.RemoveAt(cardIndex);
        return true;
    }

    [Server]
    public virtual Card RemoveCard(int index)
    {
        LoggingManager.Instance.CardCollectionLogger.LogMethod();
        try
        {
            Card card = cards[index];
            cards.RemoveAt(index);
            return card;
        }
        catch (ArgumentOutOfRangeException)
        {
            Debug.LogWarning($"Deletion index {index} is out of bounds in Cards");
        }
        return new Card();
    }

    [Server]
    public virtual Card RemoveCardAtStart()
    {
        LoggingManager.Instance.CardCollectionLogger.LogMethod();
        return RemoveCard(0);
    }

    [Server]
    public virtual Card RemoveCardAtEnd()
    {
        LoggingManager.Instance.CardCollectionLogger.LogMethod();
        return RemoveCard(cards.Count - 1);
    }

    [Server]
    public virtual void FlipCard(int index)
    {
        LoggingManager.Instance.CardCollectionLogger.LogMethod();
        try
        {
            cards[index] = cards[index].Flipped();
        }
        catch (ArgumentOutOfRangeException)
        {
            Debug.LogWarning($"Index {index} is out of bounds in Cards");
        }
    }

    public virtual void FlipCardFaceUp(int index)
    {
        LoggingManager.Instance.CardCollectionLogger.LogMethod();
        try
        {
            if (!cards[index].IsFaceUp)
            {
                cards[index] = cards[index].Flipped();
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            Debug.LogWarning($"Index {index} is out of bounds in Cards");
        }
    }

    public virtual void FlipCardFaceDown(int index)
    {
        LoggingManager.Instance.CardCollectionLogger.LogMethod();
        try
        {
            if (cards[index].IsFaceUp)
            {
                cards[index] = cards[index].Flipped();
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            Debug.LogWarning($"Index {index} is out of bounds in Cards");
        }
    }

    public virtual void FlipFirstCard()
    {
        LoggingManager.Instance.CardCollectionLogger.LogMethod();
        FlipCard(0);
    }

    public virtual void FlipFirstCardFaceUp()
    {
        LoggingManager.Instance.CardCollectionLogger.LogMethod();
        FlipCardFaceUp(0);
    }

    public virtual void FlipFirstCardFaceDown()
    {
        LoggingManager.Instance.CardCollectionLogger.LogMethod();
        FlipCardFaceDown(0);
    }

    public virtual void ShuffleCards()
    {
        LoggingManager.Instance.CardCollectionLogger.LogMethod();
        if (Cards.Count <= 1) return;

        for (int i = Cards.Count - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            (Cards[i], Cards[randomIndex]) = (Cards[randomIndex], Cards[i]);
        }
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (Card card in cards)
        {
            stringBuilder.AppendLine(card.ToString());
            stringBuilder.AppendLine();
        }
        return stringBuilder.ToString();
    }
}