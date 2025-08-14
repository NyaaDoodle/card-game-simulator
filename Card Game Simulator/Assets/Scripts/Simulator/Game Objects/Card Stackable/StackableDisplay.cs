using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Stackable))]
public class StackableDisplay : CardCollectionDisplay, IPointerClickHandler
{
    public Stackable Stackable => (Stackable)CardCollection;
    public event Action<Stackable> StackableSelected;
    protected override void OnReady(CardCollection _)
    {
        LoggerReferences.Instance.StackableDisplayLogger.LogMethod();
        base.OnReady(_);
        relocateOnTable();
        rotateOnTable();
    }

    protected override void SetCardCollection()
    {
        LoggerReferences.Instance.StackableDisplayLogger.LogMethod();
        CardCollection = GetComponent<Stackable>();
    }

    private void relocateOnTable()
    {
        LoggerReferences.Instance.StackableDisplayLogger.LogMethod();
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
        LoggerReferences.Instance.StackableDisplayLogger.LogMethod();
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

    public virtual void OnPointerClick(PointerEventData pointerEventData)
    {
        LoggerReferences.Instance.StackableDisplayLogger.LogMethod();
        OnStackableSelected();
    }
    
    protected virtual void OnStackableSelected()
    {
        LoggerReferences.Instance.StackableDisplayLogger.LogMethod();
        StackableSelected?.Invoke(Stackable);
    }
}