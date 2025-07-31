using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class StackableDisplay : CardCollectionDisplay, IPointerClickHandler
{
    public Stackable StackableState { get; private set; }

    public void Setup(Stackable stackableState)
    {
        StackableState = stackableState;
        relocateOnTable();
        base.Setup(stackableState);
    }

    void OnDestroy()
    {
        UnsubscribeFromStateEvents();
    }

    private void relocateOnTable()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        try
        {
            float x = StackableState.StackableData.LocationOnTable.Item1;
            float y = StackableState.StackableData.LocationOnTable.Item2;
            rectTransform.anchoredPosition = new Vector2(x, y);
        }
        catch (NullReferenceException)
        {
            Debug.LogWarning("StackableState is null");
        }
    }
    
    protected override void SubscribeToStateEvents()
    {
        base.SubscribeToStateEvents();
        StackableState.CardsShuffled += onCardsShuffled;
    }

    protected override void UnsubscribeFromStateEvents()
    {
        base.UnsubscribeFromStateEvents();
        StackableState.CardsShuffled -= onCardsShuffled;
    }

    public virtual void OnPointerClick(PointerEventData pointerEventData)
    {
        try
        {
            Debug.Log($"Stackable {StackableState.StackableData.Id} clicked");
        }
        catch (NullReferenceException)
        {
            Debug.LogWarning("StackableState is null");
        }
    }

    private void onCardsShuffled(Stackable _)
    {
        ClearAndSpawnCardDisplays();
    }
}