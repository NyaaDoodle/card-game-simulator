using System;
using System.Collections.Generic;
using UnityEngine;

public class StackableState : CardCollectionState
{
    public StackableData StackableData { get; private set; }
    public CardState TopCard => FirstCard;

    // Events
    public event Action<StackableState> CardsShuffled;

    public StackableState(StackableData stackableData)
    {
        StackableData = stackableData;
    }

    public void AddCardToTop(CardState card)
    {
        card.FlipFaceDown();
        AddCardAtStart(card);
    }

    public void AddCardToBottom(CardState card)
    {
        card.FlipFaceDown();
        AddCardAtEnd(card);
    }

    public override void AddCards(ICollection<CardState> cards)
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
        CardState cardDrawn = RemoveCard(0);
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

    protected virtual void OnCardsShuffled()
    {
        CardsShuffled?.Invoke(this);
    }
}
