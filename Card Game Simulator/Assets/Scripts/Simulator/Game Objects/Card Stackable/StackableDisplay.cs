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
        base.Setup(stackable);
        relocateOnTable();
        rotateOnTable();
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

    public virtual void OnPointerClick(PointerEventData pointerEventData)
    {
        OnStackableSelected();
    }
    
    protected virtual void OnStackableSelected()
    {
        StackableSelected?.Invoke(Stackable);
    }
}