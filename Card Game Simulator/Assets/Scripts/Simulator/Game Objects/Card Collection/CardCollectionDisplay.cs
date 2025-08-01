﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class CardCollectionDisplay : MonoBehaviour
{
    [SerializeField] private GameObject cardDisplaysContainer;
    [SerializeField] private GameObject cardDisplayPrefab;

    public CardCollection CardCollectionState { get; private set; }
    public List<CardDisplay> CardDisplays { get; } = new List<CardDisplay>();

    public virtual void Setup(CardCollection cardCollectionState)
    {
        CardCollectionState = cardCollectionState;
        SubscribeToStateEvents();
        ClearAndSpawnCardDisplays();
    }

    void OnDestroy()
    {
        UnsubscribeFromStateEvents();
    }

    protected virtual void SubscribeToStateEvents()
    {
        try
        {
            CardCollectionState.CardAdded += OnCardAdded;
            CardCollectionState.CardRemoved += OnCardRemoved;
        }
        catch (NullReferenceException)
        {
            Debug.LogWarning("CardCollectionState is null");
        }
    }

    protected virtual void UnsubscribeFromStateEvents()
    {
        if (CardCollectionState == null) return;
        CardCollectionState.CardAdded -= OnCardAdded;
        CardCollectionState.CardRemoved -= OnCardRemoved;
    }

    protected virtual void ClearAndSpawnCardDisplays()
    {
        try
        {
            DestroyAndRemoveAllCardDisplays();
            foreach (Card cardState in CardCollectionState.Cards)
            {
                CreateAndAddCardDisplayToEnd(cardState);
            }
        }
        catch (NullReferenceException)
        {
            Debug.LogWarning("CardCollectionState is null");
        }
    }

    protected virtual void OnCardAdded(CardCollection _, Card cardState, int index)
    {
        CreateAndAddCardDisplay(cardState, index);
    }

    protected virtual void OnCardRemoved(CardCollection _, Card card, int index)
    {
        DestroyAndRemoveCardDisplay(index);
    }

    protected virtual void CreateAndAddCardDisplay(Card cardState, int insertionIndex)
    {
        try
        {
            CardDisplay cardDisplay =
                cardDisplayPrefab.InstantiateCardDisplay(cardState, cardDisplaysContainer.transform);
            CardDisplays.Insert(insertionIndex, cardDisplay);
            SetCardDisplayHierarchyReverseIndex(cardDisplay, insertionIndex);
        }
        catch (ArgumentOutOfRangeException)
        {
            Debug.LogWarning($"Insertion index {insertionIndex} is out of bounds in CardDisplays");
        }
    }

    protected virtual void CreateAndAddCardDisplayToEnd(Card cardState)
    {
        CreateAndAddCardDisplay(cardState, CardDisplays.Count);
    }

    protected virtual void DestroyAndRemoveCardDisplay(int deletionIndex)
    {
        try
        {
            CardDisplay cardDisplay = CardDisplays[deletionIndex];
            CardDisplays.RemoveAt(deletionIndex);
            Destroy(cardDisplay.gameObject);
        }
        catch (ArgumentOutOfRangeException)
        {
            Debug.LogWarning($"Deletion index {deletionIndex} is out of bounds in CardDisplays");
        }
    }

    protected virtual void DestroyAndRemoveAllCardDisplays()
    {
        while (CardDisplays.Count > 0)
        {
            DestroyAndRemoveCardDisplay(0);
        }
    }

    protected virtual void SetCardDisplayHierarchyReverseIndex(CardDisplay cardDisplay, int index)
    {
        Transform cardDisplaysContainerTransform = cardDisplaysContainer.transform;
        int reverseIndex = cardDisplaysContainerTransform.childCount - 1 - index;
        Transform cardDisplayTransform = cardDisplay.gameObject.transform;
        cardDisplayTransform.SetSiblingIndex(reverseIndex);
    }
}
