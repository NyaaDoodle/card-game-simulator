using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Stackable))]
public class StackableDisplay : CardCollectionDisplay, IPointerClickHandler
{
    protected Stackable stackable;
    public event Action<Stackable> StackableSelected;

    public override void OnStartClient()
    {
        base.OnStartClient();
        relocateOnTable();
        rotateOnTable();
    }

    protected override void SetCardCollection()
    {
        stackable = GetComponent<Stackable>();
        cardCollection = stackable;
    }

    private void relocateOnTable()
    {
        Debug.Log(stackable.ToString() ?? "null");
        RectTransform rectTransform = GetComponent<RectTransform>();
        try
        {
            float x = stackable.StackableData.TableXCoordinate;
            float y = stackable.StackableData.TableYCoordinate;
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
            rectTransform.Rotate(0, 0, stackable.StackableData.Rotation);
        }
        catch (NullReferenceException)
        {
            Debug.LogWarning("Stackable is null");
        }
    }

    protected override void SubscribeToStateEvents()
    {
        base.SubscribeToStateEvents();
        stackable.CardsShuffled += onCardsShuffled;
    }

    protected override void UnsubscribeFromStateEvents()
    {
        base.UnsubscribeFromStateEvents();
        stackable.CardsShuffled -= onCardsShuffled;
    }

    public virtual void OnPointerClick(PointerEventData pointerEventData)
    {
        OnStackableSelected();
    }

    private void onCardsShuffled(Stackable _)
    {
        RefreshCardDisplays();
    }
    
    protected virtual void OnStackableSelected()
    {
        StackableSelected?.Invoke(stackable);
    }
}