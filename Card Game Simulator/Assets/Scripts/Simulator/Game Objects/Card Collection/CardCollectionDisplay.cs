using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

[RequireComponent(typeof(CardCollection))]
public class CardCollectionDisplay : NetworkBehaviour
{
    [SerializeField] private RectTransform cardDisplaysContainer;
    [SerializeField] private GameObject cardDisplayPrefab;
    protected CardCollection cardCollection;
    protected List<CardDisplay> CardDisplays { get; } = new List<CardDisplay>();
    public event Action<CardCollection, Card> CardSelected;

    public override void OnStartClient()
    {
        base.OnStartClient();
        SetCardCollection();
        SubscribeToStateEvents();
        RefreshCardDisplays();
    }

    protected virtual void SetCardCollection()
    {
        cardCollection = GetComponent<CardCollection>();
    }

    void OnDestroy()
    {
        UnsubscribeFromStateEvents();
    }

    protected virtual void SubscribeToStateEvents()
    {
        try
        {
            cardCollection.CardAdded += OnCardAdded;
            cardCollection.CardRemoved += OnCardRemoved;
            cardCollection.CardsCleared += OnCardsCleared;
        }
        catch (NullReferenceException)
        {
            Debug.LogWarning("CardCollection is null");
        }
    }

    protected virtual void UnsubscribeFromStateEvents()
    {
        if (cardCollection == null) return;
        cardCollection.CardAdded -= OnCardAdded;
        cardCollection.CardRemoved -= OnCardRemoved;
        cardCollection.CardsCleared -= OnCardsCleared;
    }

    protected virtual void RefreshCardDisplays()
    {
        try
        {
            ClearCardDisplays();
            foreach (Card card in cardCollection.Cards)
            {
                AddCardDisplayToEnd(card);
            }
        }
        catch (NullReferenceException)
        {
            Debug.LogWarning("CardCollection is null");
        }
    }

    protected virtual void OnCardAdded(CardCollection _, Card cardState, int index)
    {
        AddCardDisplay(cardState, index);
    }

    protected virtual void OnCardRemoved(CardCollection _, Card card, int index)
    {
        RemoveCardDisplay(index);
    }

    protected virtual void OnCardsCleared(CardCollection _)
    {
        ClearCardDisplays();
    }

    protected virtual void AddCardDisplay(Card card, int insertionIndex)
    {
        try
        {
            CardDisplay cardDisplay = cardDisplayPrefab.InstantiateCardDisplay(card, cardDisplaysContainer.transform);
            CardDisplays.Insert(insertionIndex, cardDisplay);
            SetCardDisplayHierarchyReverseIndex(cardDisplay, insertionIndex);
            subscribeToCardSelected(cardDisplay);
        }
        catch (ArgumentOutOfRangeException)
        {
            Debug.LogWarning($"Insertion index {insertionIndex} is out of bounds in CardDisplays");
        }
    }

    protected virtual void AddCardDisplayToEnd(Card card)
    {
        AddCardDisplay(card, CardDisplays.Count);
    }

    protected virtual void RemoveCardDisplay(int removalIndex)
    {
        try
        {
            CardDisplay cardDisplay = CardDisplays[removalIndex];
            CardDisplays.RemoveAt(removalIndex);
            unsubscribeFromCardSelected(cardDisplay);
            Destroy(cardDisplay.gameObject);
        }
        catch (ArgumentOutOfRangeException)
        {
            Debug.LogWarning($"Removal index {removalIndex} is out of bounds in CardDisplays");
        }
    }

    protected virtual void ClearCardDisplays()
    {
        while (CardDisplays.Count > 0)
        {
            RemoveCardDisplay(0);
        }
    }

    protected virtual void SetCardDisplayHierarchyReverseIndex(CardDisplay cardDisplay, int index)
    {
        Transform cardDisplaysContainerTransform = cardDisplaysContainer.transform;
        int reverseIndex = cardDisplaysContainerTransform.childCount - 1 - index;
        Transform cardDisplayTransform = cardDisplay.gameObject.transform;
        cardDisplayTransform.SetSiblingIndex(reverseIndex);
    }

    private void subscribeToCardSelected(CardDisplay cardDisplay)
    {
        cardDisplay.Selected += OnCardSelected;
    }

    private void unsubscribeFromCardSelected(CardDisplay cardDisplay)
    {
        cardDisplay.Selected -= OnCardSelected;
    }

    protected virtual void OnCardSelected(Card card)
    {
        CardSelected?.Invoke(cardCollection, card);
    }
}
