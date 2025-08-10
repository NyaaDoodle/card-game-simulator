using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class CardCollection : NetworkBehaviour
{
    private readonly SyncList<Card> cards = new SyncList<Card>();

    // Events
    public event Action<CardCollection, Card, int> CardAdded;
    public event Action<CardCollection, Card, int> CardRemoved;
    public event Action<CardCollection, Card> CardSelected;
    public event Action<CardCollection> CardsCleared;

    public SyncList<Card> Cards => cards;
    public bool IsEmpty => cards.Count <= 0;
    public Card FirstCard => cards[0];

    public override void OnStartClient()
    {
        cards.OnAdd += OnCardsItemAdded;
        cards.OnInsert += OnCardsItemInserted;
        cards.OnSet += OnCardsItemSet;
        cards.OnRemove += OnCardsItemRemoved;
        cards.OnClear += OnCardsCleared;
    }

    public override void OnStopClient()
    {
        cards.OnAdd -= OnCardsItemAdded;
        cards.OnInsert -= OnCardsItemInserted;
        cards.OnSet -= OnCardsItemSet;
        cards.OnRemove -= OnCardsItemRemoved;
        cards.OnClear -= OnCardsCleared;
    }

    protected virtual void OnCardsItemAdded(int index)
    {
        // When a card is added to the end of cards, using Add()
        OnCardAdded(cards[index], index);
    }

    protected virtual void OnCardsItemInserted(int index)
    {
        // When a card is inserted/added at index in cards, using Insert()
        OnCardAdded(cards[index], index);
    }

    protected virtual void OnCardsItemSet(int index, Card oldCard)
    {
        // When a card is replaced at index in cards, using [], gives the old/replaced card
        OnCardRemoved(oldCard, index);
        OnCardAdded(cards[index], index);
    }

    protected virtual void OnCardsItemRemoved(int index, Card oldCard)
    {
        // When a card is removed at index in cards, using Remove(), gives the removed card
        OnCardRemoved(oldCard, index);
    }

    protected virtual void OnCardsCleared()
    {
        // When cards is cleared, using Clear()
        CardsCleared?.Invoke(this);
    }

    protected virtual void OnCardAdded(Card cardAdded, int index)
    {
        CardAdded?.Invoke(this, cardAdded, index);
    }

    protected virtual void OnCardRemoved(Card cardRemoved, int index)
    {
        CardRemoved?.Invoke(this, cardRemoved, index);
    }

    protected virtual void OnCardSelected(Card cardSelected)
    {
        CardSelected?.Invoke(this, cardSelected);
    }


    [Server]
    public virtual void AddCard(Card card, int index)
    {
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
        AddCard(card, 0);
    }

    [Server]
    public virtual void AddCardAtEnd(Card card)
    {
        AddCard(card, cards.Count);
    }

    [Server]
    public virtual void AddCards(IEnumerable<Card> cards)
    {
        foreach (Card card in cards)
        {
            AddCardAtEnd(card);
        }
    }

    [Server]
    public virtual bool RemoveCard(Card card)
    {
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
        return RemoveCard(0);
    }

    [Server]
    public virtual Card RemoveCardAtEnd()
    {
        return RemoveCard(cards.Count - 1);
    }

    [Server]
    public virtual void FlipCard(int index)
    {
        try
        {
            cards[index] = cards[index].Flipped();
        }
        catch (ArgumentOutOfRangeException)
        {
            Debug.LogWarning($"Index {index} is out of bounds in Cards");
        }
    }

    public virtual void FlipFirstCard()
    {
        FlipCard(0);
    }

    public virtual void FlipFirstCardFaceUp()
    {
        if (Cards.Count > 0 && FirstCard.IsFaceUp == false)
        {
            FlipFirstCard();
        }
    }

    public virtual void FlipFirstCardFaceDown()
    {
        if (Cards.Count > 0 && FirstCard.IsFaceUp == true)
        {
            FlipFirstCard();
        }
    }
}