using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class StackableDisplay : CardCollectionDisplay, IPointerClickHandler
{
    public Stackable Stackable => (Stackable)CardCollection;
    public event Action<Stackable> StackableSelected;

    public virtual void Setup(Stackable stackable)
    {
        LoggerReferences.Instance.StackableDisplayLogger.LogMethod();
        base.Setup(stackable);
        relocateOnTable();
        rotateOnTable();
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