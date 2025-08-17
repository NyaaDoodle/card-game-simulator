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
        LoggingManager.Instance.StackableDisplayLogger.LogMethod();
        base.Setup(stackable);
        relocateOnTable();
        rotateOnTable();
    } 

    private void relocateOnTable()
    {
        LoggingManager.Instance.StackableDisplayLogger.LogMethod();
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
        LoggingManager.Instance.StackableDisplayLogger.LogMethod();
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
        LoggingManager.Instance.StackableDisplayLogger.LogMethod();
        OnStackableSelected();
    }
    
    protected virtual void OnStackableSelected()
    {
        LoggingManager.Instance.StackableDisplayLogger.LogMethod();
        StackableSelected?.Invoke(Stackable);
    }
}