using System;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private GameObject actionMenuPanel;

    public GameObject CurrentSelectedObject { get; private set; }
    public SelectionType CurrentSelectionType { get; private set; } = SelectionType.None;

    // Events
    public event Action<GameObject, SelectionType> SelectionChanged;
    public event Action SelectionCleared;

    public bool HasSelection => CurrentSelectedObject != null;
    public bool IsSelected(GameObject obj) => CurrentSelectedObject == obj;

    public void SelectObject(GameObject objectToSelect, SelectionType objectType)
    {
        // Nothing to select
        if (objectToSelect == null)
        {
            ClearSelection();
            return;
        }
        // Check if already selected
        if (CurrentSelectedObject == objectToSelect && CurrentSelectionType == objectType)
            return;

        ClearSelection();
        CurrentSelectedObject = objectToSelect;
        CurrentSelectionType = objectType;
        OnSelectionChanged();

        Debug.Log($"Selected {objectType}: {objectToSelect.name}");
    }

    public void ClearSelection()
    {
        if (CurrentSelectedObject == null) return;

        CurrentSelectedObject = null;
        CurrentSelectionType = SelectionType.None;
        OnSelectionCleared();

        Debug.Log("Selection cleared");
    }

    protected virtual void OnSelectionCleared()
    {
        SelectionCleared?.Invoke();
    }

    protected virtual void OnSelectionChanged()
    {
        SelectionChanged?.Invoke(CurrentSelectedObject, CurrentSelectionType);
    }
}
