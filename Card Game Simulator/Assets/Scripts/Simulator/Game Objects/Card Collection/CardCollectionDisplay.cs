using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

[RequireComponent(typeof(CardCollection))]
public class CardCollectionDisplay : MonoBehaviour
{
    [SerializeField] private RectTransform cardDisplaysContainer;
    [SerializeField] private GameObject cardDisplayPrefab;
    public CardCollection CardCollection { get; protected set; }
    protected List<CardDisplay> CardDisplays { get; } = new List<CardDisplay>();
    public event Action<CardCollection, Card> CardSelected;

    protected virtual void Awake()
    {
        LoggerReferences.Instance.CardCollectionDisplayLogger.LogMethod();
        SetCardCollection();
        SubscribeToCardCollectionEvents();
    }

    protected virtual void OnReady(CardCollection _)
    {
        LoggerReferences.Instance.CardCollectionDisplayLogger.LogMethod();
        RefreshCardDisplays();
    }

    protected virtual void OnDestroy()
    {
        LoggerReferences.Instance.CardCollectionDisplayLogger.LogMethod();
        ClearCardDisplays();
        UnsubscribeFromStateEvents();
    }
    
    protected virtual void SetCardCollection()
    {
        LoggerReferences.Instance.CardCollectionDisplayLogger.LogMethod();;
        CardCollection = GetComponent<CardCollection>();
    }

    protected virtual void SubscribeToCardCollectionEvents()
    {
        LoggerReferences.Instance.CardCollectionDisplayLogger.LogMethod();
        try
        {
            CardCollection.Ready += OnReady;
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
        LoggerReferences.Instance.CardCollectionDisplayLogger.LogMethod();
        if (CardCollection == null) return;
        CardCollection.Ready -= OnReady;
        CardCollection.CardAdded -= OnCardAdded;
        CardCollection.CardRemoved -= OnCardRemoved;
        CardCollection.CardsCleared -= OnCardsCleared;
    }

    protected virtual void RefreshCardDisplays()
    {
        LoggerReferences.Instance.CardCollectionDisplayLogger.LogMethod();
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
        LoggerReferences.Instance.CardCollectionDisplayLogger.LogMethod();
        AddCardDisplay(cardState, index);
    }

    protected virtual void OnCardRemoved(CardCollection _, Card card, int index)
    {
        LoggerReferences.Instance.CardCollectionDisplayLogger.LogMethod();
        RemoveCardDisplay(index);
    }

    protected virtual void OnCardsCleared(CardCollection _)
    {
        LoggerReferences.Instance.CardCollectionDisplayLogger.LogMethod();
        ClearCardDisplays();
    }

    protected virtual void AddCardDisplay(Card card, int insertionIndex)
    {
        LoggerReferences.Instance.CardCollectionDisplayLogger.LogMethod();
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
        LoggerReferences.Instance.CardCollectionDisplayLogger.LogMethod();
        AddCardDisplay(card, CardDisplays.Count);
    }

    protected virtual void RemoveCardDisplay(int removalIndex)
    {
        LoggerReferences.Instance.CardCollectionDisplayLogger.LogMethod();
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
        LoggerReferences.Instance.CardCollectionDisplayLogger.LogMethod();
        while (CardDisplays.Count > 0)
        {
            RemoveCardDisplay(0);
        }
    }

    protected virtual void SetCardDisplayHierarchyReverseIndex(CardDisplay cardDisplay, int index)
    {
        LoggerReferences.Instance.CardCollectionDisplayLogger.LogMethod();
        Transform cardDisplaysContainerTransform = cardDisplaysContainer.transform;
        int reverseIndex = cardDisplaysContainerTransform.childCount - 1 - index;
        Transform cardDisplayTransform = cardDisplay.gameObject.transform;
        cardDisplayTransform.SetSiblingIndex(reverseIndex);
    }

    private void subscribeToCardSelected(CardDisplay cardDisplay)
    {
        LoggerReferences.Instance.CardCollectionDisplayLogger.LogMethod();
        cardDisplay.Selected += OnCardSelected;
    }

    private void unsubscribeFromCardSelected(CardDisplay cardDisplay)
    {
        LoggerReferences.Instance.CardCollectionDisplayLogger.LogMethod();
        cardDisplay.Selected -= OnCardSelected;
    }

    protected virtual void OnCardSelected(Card card)
    {
        LoggerReferences.Instance.CardCollectionDisplayLogger.LogMethod();
        CardSelected?.Invoke(CardCollection, card);
    }
}
