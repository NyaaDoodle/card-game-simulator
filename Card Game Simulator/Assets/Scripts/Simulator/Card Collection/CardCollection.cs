using System;
using System.Collections.Generic;
using UnityEngine;

public class CardCollection
{
    public List<Card> Cards { get; } = new List<Card>();

    // Events
    public event Action<CardCollection, Card, int> CardAdded;
    public event Action<CardCollection, Card, int> CardRemoved;
    public event Action<CardCollection, Card> CardSelected;

    public bool IsEmpty => Cards.Count <= 0;
    public Card FirstCard => Cards[0];

    public virtual void AddCard(Card card, int index)
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

    public virtual void AddCardAtStart(Card card)
    {
        AddCard(card, 0);
    }

    public virtual void AddCardAtEnd(Card card)
    {
        AddCard(card, Cards.Count);
    }

    public virtual void AddCards(ICollection<Card> cards)
    {
        foreach (Card card in cards)
        {
            AddCardAtEnd(card);
        }
    }

    public virtual bool RemoveCard(Card card)
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

    public virtual Card RemoveCard(int index)
    {
        try
        {
            Card card = Cards[index];
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

    public virtual Card RemoveCardAtStart()
    {
        return RemoveCard(0);
    }

    public virtual Card RemoveCardAtEnd()
    {
        return RemoveCard(Cards.Count - 1);
    }

    protected virtual void OnCardAdded(Card cardAdded, int index)
    {
        cardAdded.Selected += OnCardSelected;
        CardAdded?.Invoke(this, cardAdded, index);
    }

    protected virtual void OnCardRemoved(Card cardRemoved, int index)
    {
        cardRemoved.Selected -= OnCardSelected;
        CardRemoved?.Invoke(this, cardRemoved, index);
    }

    protected virtual void OnCardSelected(Card cardSelected)
    {
        CardSelected?.Invoke(this, cardSelected);
    }
}