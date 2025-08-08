using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class StackableDisplay : CardCollectionDisplay, IPointerClickHandler
{
    public Stackable Stackable { get; private set; }

    public void Setup(Stackable stackableState)
    {
        Stackable = stackableState;
        relocateOnTable();
        rotateOnTable();
        base.Setup(stackableState);
    }

    private void relocateOnTable()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        try
        {
            float x = Stackable.StackableData.TableXCoordinate;
            float y = Stackable.StackableData.TableYCoordinate;
            rectTransform.anchoredPosition = new Vector2(x, y);
        }
        catch (NullReferenceException)
        {
            Debug.LogWarning("Stackable is null");
        }
    }

    private void rotateOnTable()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        try
        {
            rectTransform.Rotate(0, 0, Stackable.StackableData.Rotation);
        }
        catch (NullReferenceException)
        {
            Debug.LogWarning("Stackable is null");
        }
    }
    
    protected override void SubscribeToStateEvents()
    {
        base.SubscribeToStateEvents();
        Stackable.CardsShuffled += onCardsShuffled;
    }

    protected override void UnsubscribeFromStateEvents()
    {
        base.UnsubscribeFromStateEvents();
        Stackable.CardsShuffled -= onCardsShuffled;
    }

    public virtual void OnPointerClick(PointerEventData pointerEventData)
    {
        try
        {
            Stackable.NotifySelection();
        }
        catch (NullReferenceException)
        {
            Debug.LogWarning("Stackable is null");
        }
    }

    private void onCardsShuffled(Stackable _)
    {
        RefreshCardDisplays();
    }
}