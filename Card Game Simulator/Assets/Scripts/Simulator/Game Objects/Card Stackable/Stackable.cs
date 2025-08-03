using System;
using System.Collections.Generic;
using UnityEngine;

public class Stackable : CardCollection
{
    public StackableData StackableData { get; private set; }
    public Card TopCard => FirstCard;

    // Events
    public event Action<Stackable> CardsShuffled;
    public event Action<Stackable> StackableSelected;

    public Stackable(StackableData stackableData)
    {
        StackableData = stackableData;
    }

    public void AddCardToTop(Card card)
    {
        card.FlipFaceDown();
        AddCardAtStart(card);
    }

    public void AddCardToBottom(Card card)
    {
        card.FlipFaceDown();
        AddCardAtEnd(card);
    }

    public override void AddCards(ICollection<Card> cards)
    {
        foreach (Card card in cards)
        {
            AddCardToBottom(card);
        }
    }

    public Card DrawCard()
    {
        if (IsEmpty)
        {
            Debug.LogWarning("Attempting to draw from an empty deck, returning null!");
            return null;
        }

        Card cardDrawn = RemoveCardAtStart();
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

    protected virtual void OnStackableSelected()
    {
        StackableSelected?.Invoke(this);
    }

    public void NotifySelection()
    {
        OnStackableSelected();
    }
}
