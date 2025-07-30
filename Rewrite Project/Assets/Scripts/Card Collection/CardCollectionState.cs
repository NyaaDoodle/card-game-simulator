using System;
using System.Collections.Generic;
using UnityEngine;

public class CardCollectionState
{
    public List<CardState> Cards { get; } = new List<CardState>();

    // Events
    public event Action<CardCollectionState, CardState, int> CardAdded;
    public event Action<CardCollectionState, CardState, int> CardRemoved;

    public bool IsEmpty => Cards.Count <= 0;
    public CardState FirstCard => Cards[0];

    public virtual void AddCard(CardState card, int index)
    {
        if (card == null)
        {
            Debug.LogWarning("Card to be added is null");
            return;
        }
        try
        {
            Cards.Insert(index, card);
        }
        catch (ArgumentOutOfRangeException)
        {
            Debug.LogWarning($"Insertion index {index} is out of bounds in Cards");
        }
        OnCardAdded(card, index);
    }

    public virtual void AddCardAtStart(CardState card)
    {
        AddCard(card, 0);
    }

    public virtual void AddCardAtEnd(CardState card)
    {
        AddCard(card, Cards.Count);
    }

    public virtual void AddCards(ICollection<CardState> cards)
    {
        foreach (CardState card in cards)
        {
            AddCardAtEnd(card);
        }
    }

    public virtual bool RemoveCard(CardState card)
    {
        int cardIndex = Cards.IndexOf(card);
        if (cardIndex < 0)
        {
            Debug.LogWarning("Specified card for deletion was not found");
            return false;
        }
        Cards.RemoveAt(cardIndex);
        OnCardRemoved(card, cardIndex);
        return true;
    }

    public virtual CardState RemoveCard(int index)
    {
        try
        {
            CardState card = Cards[index];
            Cards.RemoveAt(index);
            OnCardRemoved(card, index);
            return card;
        }
        catch (ArgumentOutOfRangeException)
        {
            Debug.LogWarning($"Deletion index {index} is out of bounds in Cards");
        }
        return null;
    }

    public virtual CardState RemoveCardAtStart()
    {
        return RemoveCard(0);
    }

    public virtual CardState RemoveCardAtEnd()
    {
        return RemoveCard(Cards.Count - 1);
    }

    protected virtual void OnCardAdded(CardState cardAdded, int index)
    {
        CardAdded?.Invoke(this, cardAdded, index);
    }

    protected virtual void OnCardRemoved(CardState cardRemoved, int index)
    {
        CardRemoved?.Invoke(this, cardRemoved, index);
    }
}