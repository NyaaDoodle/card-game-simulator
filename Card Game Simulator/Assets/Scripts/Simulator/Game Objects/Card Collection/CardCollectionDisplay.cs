using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class CardCollectionDisplay : MonoBehaviour
{
    [SerializeField] private RectTransform cardDisplaysContainer;
    [SerializeField] private GameObject cardDisplayPrefab;
    public CardCollection CardCollection { get; private set; }
    protected List<CardDisplay> CardDisplays { get; } = new List<CardDisplay>();
    public event Action<CardCollection, Card> CardSelected;

    public virtual void Setup(CardCollection cardCollection)
    {
        LoggingManager.Instance.CardCollectionDisplayLogger.LogMethod();
        CardCollection = cardCollection;
        SubscribeToCardCollectionEvents();
        RefreshCardDisplays();
    }

    protected virtual void OnDestroy()
    {
        LoggingManager.Instance.CardCollectionDisplayLogger.LogMethod();
        ClearCardDisplays();
        UnsubscribeFromStateEvents();
    }

    protected virtual void SubscribeToCardCollectionEvents()
    {
        LoggingManager.Instance.CardCollectionDisplayLogger.LogMethod();
        try
        {
            CardCollection.CardAdded += OnCardAdded;
            CardCollection.CardRemoved += OnCardRemoved;
            CardCollection.CardsCleared += OnCardsCleared;
        }
        catch (NullReferenceException)
        {
            Debug.LogWarning("CardCollection is null");
        }
    }

    protected virtual void UnsubscribeFromStateEvents()
    {
        LoggingManager.Instance.CardCollectionDisplayLogger.LogMethod();
        if (CardCollection == null) return;
        CardCollection.CardAdded -= OnCardAdded;
        CardCollection.CardRemoved -= OnCardRemoved;
        CardCollection.CardsCleared -= OnCardsCleared;
    }

    protected virtual void RefreshCardDisplays()
    {
        LoggingManager.Instance.CardCollectionDisplayLogger.LogMethod();
        try
        {
            ClearCardDisplays();
            foreach (Card card in CardCollection.Cards)
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
        LoggingManager.Instance.CardCollectionDisplayLogger.LogMethod();
        AddCardDisplay(cardState, index);
    }

    protected virtual void OnCardRemoved(CardCollection _, Card card, int index)
    {
        LoggingManager.Instance.CardCollectionDisplayLogger.LogMethod();
        RemoveCardDisplay(index);
    }

    protected virtual void OnCardsCleared(CardCollection _)
    {
        LoggingManager.Instance.CardCollectionDisplayLogger.LogMethod();
        ClearCardDisplays();
    }

    protected virtual void AddCardDisplay(Card card, int insertionIndex)
    {
        LoggingManager.Instance.CardCollectionDisplayLogger.LogMethod();
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
        LoggingManager.Instance.CardCollectionDisplayLogger.LogMethod();
        AddCardDisplay(card, CardDisplays.Count);
    }

    protected virtual void RemoveCardDisplay(int removalIndex)
    {
        LoggingManager.Instance.CardCollectionDisplayLogger.LogMethod();
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
        LoggingManager.Instance.CardCollectionDisplayLogger.LogMethod();
        while (CardDisplays.Count > 0)
        {
            RemoveCardDisplay(0);
        }
    }

    protected virtual void SetCardDisplayHierarchyReverseIndex(CardDisplay cardDisplay, int index)
    {
        LoggingManager.Instance.CardCollectionDisplayLogger.LogMethod();
        Transform cardDisplaysContainerTransform = cardDisplaysContainer.transform;
        int reverseIndex = cardDisplaysContainerTransform.childCount - 1 - index;
        Transform cardDisplayTransform = cardDisplay.gameObject.transform;
        cardDisplayTransform.SetSiblingIndex(reverseIndex);
    }

    private void subscribeToCardSelected(CardDisplay cardDisplay)
    {
        LoggingManager.Instance.CardCollectionDisplayLogger.LogMethod();
        cardDisplay.Selected += OnCardSelected;
    }

    private void unsubscribeFromCardSelected(CardDisplay cardDisplay)
    {
        LoggingManager.Instance.CardCollectionDisplayLogger.LogMethod();
        cardDisplay.Selected -= OnCardSelected;
    }

    protected virtual void OnCardSelected(Card card)
    {
        LoggingManager.Instance.CardCollectionDisplayLogger.LogMethod();
        CardSelected?.Invoke(CardCollection, card);
    }
}
