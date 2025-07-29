using System;
using System.Collections.Generic;
using UnityEngine;

public class StackableState
{
    public StackableData StackableData { get; private set; }
    public List<CardState> Cards { get; } = new List<CardState>();

    // Events
    public event Action<StackableState, CardState, int> CardAdded;
    public event Action<StackableState, CardState, int> CardRemoved;
    public event Action<StackableState> CardsShuffled;

    public bool IsEmpty => Cards.Count <= 0;
    public CardState TopCard => Cards[0];

    public StackableState(StackableData stackableData)
    {
        StackableData = stackableData;
    }

    public void AddCardToTop(CardState cardState)
    {
        Cards.Insert(0, cardState);
        OnCardAdded(cardState, 0);
    }

    public void AddCardToBottom(CardState cardState)
    {
        Cards.Add(cardState);
        OnCardAdded(cardState, Cards.Count - 1);
    }

    public void AddCards(ICollection<CardState> cards)
    {
        foreach (CardState card in cards)
        {
            AddCardToBottom(card);
        }
    }

    public CardState DrawCard()
    {
        if (IsEmpty)
        {
            Debug.LogWarning("Attempting to draw from an empty deck, returning null!");
            return null;
        }
        CardState cardDrawn = TopCard;
        Cards.RemoveAt(0);
        OnCardRemoved(cardDrawn, 0);
        return cardDrawn;
    }

    public void FlipTopCard()
    {
        if (IsEmpty) return;
        TopCard.Flip();
    }

    public void Shuffle()
    {
        if (Cards.Count <= 1) return;

        for (int i = Cards.Count - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            (Cards[i], Cards[randomIndex]) = (Cards[randomIndex], Cards[i]);
        }

        OnCardsShuffled();
    }

    public bool RemoveCard(CardState cardState)
    {
        int cardStateIndex = Cards.IndexOf(cardState);
        if (cardStateIndex < 0)
        {
            Debug.LogWarning("Specified card for deletion was not found");
            return false;
        }
        Cards.RemoveAt(cardStateIndex);
        OnCardRemoved(cardState, cardStateIndex);
        return true;
    }

    protected virtual void OnCardAdded(CardState cardAdded, int index)
    {
        CardAdded?.Invoke(this, cardAdded, index);
    }

    protected virtual void OnCardRemoved(CardState cardRemoved, int index)
    {
        CardRemoved?.Invoke(this, cardRemoved, index);
    }

    protected virtual void OnCardsShuffled()
    {
        CardsShuffled?.Invoke(this);
    }
}
