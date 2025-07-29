using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class StackableDisplay : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject cardDisplaysContainer;
    [SerializeField] private GameObject cardDisplayPrefab;
    public StackableState StackableState { get; private set; }
    public List<CardTableDisplay> CardDisplays { get; } = new List<CardTableDisplay>();

    public void Setup(StackableState stackableState)
    {
        StackableState = stackableState;
        subscribeToStackableStateEvents();
        relocateOnTable();
        spawnCardDisplays();
    }

    void OnDestroy()
    {
        unsubscribeFromStackableStateEvents();
    }

    private void relocateOnTable()
    {
        if (IsStackableNotDefined()) return;

        RectTransform rectTransform = GetComponent<RectTransform>();
        float x = StackableState.StackableData.LocationOnTable.Item1;
        float y = StackableState.StackableData.LocationOnTable.Item2;
        rectTransform.anchoredPosition = new Vector2(x, y);
    }
    
    private void subscribeToStackableStateEvents()
    {
        if (IsStackableNotDefined()) return;
        StackableState.CardAdded += onCardAdded;
        StackableState.CardRemoved += onCardRemoved;
        StackableState.CardsShuffled += onCardsShuffled;
    }

    private void unsubscribeFromStackableStateEvents()
    {
        if (StackableState == null) return;
        StackableState.CardAdded -= onCardAdded;
        StackableState.CardRemoved -= onCardRemoved;
        StackableState.CardsShuffled -= onCardsShuffled;
    }

    public virtual void OnPointerClick(PointerEventData pointerEventData)
    {
        if (IsStackableNotDefined()) return;
        Debug.Log($"Stackable {StackableState.StackableData.Id} clicked");
    }

    private void spawnCardDisplays()
    {
        if (IsStackableNotDefined()) return;
        CardDisplays.Clear();
        foreach (CardState cardState in StackableState.Cards)
        {
            createAndAddCardDisplayToEnd(cardState);
        }
    }

    private void onCardAdded(StackableState _, CardState cardState, int index)
    {
        createAndAddCardDisplay(cardState, index);
    }

    private void onCardRemoved(StackableState _, CardState card, int index)
    {
        destroyAndRemoveCardDisplay(index);
    }

    private void onCardsShuffled(StackableState _)
    {
        destroyAndRemoveAllCardDisplays();
        spawnCardDisplays();
    }

    private void createAndAddCardDisplay(CardState cardState, int insertionIndex)
    {
        if (insertionIndex < 0 || insertionIndex > CardDisplays.Count)
        {
            Debug.LogWarning($"Insertion index {insertionIndex} is out of bounds in CardDisplays");
            return;
        }
        cardState.FlipFaceDown();
        CardTableDisplay cardDisplay =
            cardDisplayPrefab.InstantiateCardTableDisplay(cardState, cardDisplaysContainer.transform);
        CardDisplays.Insert(insertionIndex, cardDisplay);
        setCardDisplayHierarchyIndex(cardDisplay, insertionIndex);
    }

    private void createAndAddCardDisplayToEnd(CardState cardState)
    {
        createAndAddCardDisplay(cardState, CardDisplays.Count);
    }

    private void destroyAndRemoveCardDisplay(int deletionIndex)
    {
        if (deletionIndex < 0 || deletionIndex >= CardDisplays.Count)
        {
            Debug.LogWarning($"Deletion index {deletionIndex} is out of bounds in CardDisplays");
            return;
        }
        CardTableDisplay cardDisplay = CardDisplays[deletionIndex];
        CardDisplays.RemoveAt(deletionIndex);
        Destroy(cardDisplay.gameObject);
    }

    private void destroyAndRemoveAllCardDisplays()
    {
        for (int i = 0; i < CardDisplays.Count; i++)
        {
            destroyAndRemoveCardDisplay(i);
        }
    }

    private void setCardDisplayHierarchyIndex(CardTableDisplay cardDisplay, int index)
    {
        Transform cardDisplayTransform = cardDisplay.gameObject.transform;
        cardDisplayTransform.SetSiblingIndex(index);
    }

    protected bool IsStackableNotDefined()
    {
        if (StackableState == null)
        {
            Debug.LogWarning("StackableState is null");
            return true;
        }
        return false;
    }
}