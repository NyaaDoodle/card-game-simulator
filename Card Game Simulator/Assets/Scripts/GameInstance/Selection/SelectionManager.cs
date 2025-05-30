using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private ViewManager viewManager;

    [Header("Selection Highlight")]
    [SerializeField] private GameObject selectionHighlightPrefab;
    [SerializeField] private float highlightScaleFactor = 1.05f;

    public GameObject CurrentSelectedObject { get; private set; }
    public SelectionType CurrentSelectionType { get; private set; } = SelectionType.None;
    private GameObject currentHighlightInstance;

    // Events
    public event Action<GameObject, SelectionType> SelectionChanged;
    public event Action SelectionCleared;

    public bool HasSelection => CurrentSelectedObject != null;
    public bool IsSelected(GameObject obj) => CurrentSelectedObject == obj;

    void Start()
    {
        if (viewManager != null)
        {
            viewManager.ViewChanged += onViewChanged;
        }
    }

    void OnDestroy()
    {
        if (viewManager != null)
        {
            viewManager.ViewChanged -= onViewChanged;
        }
    }

    private void onViewChanged(SimulatorView oldView, SimulatorView newView)
    {
        ClearSelection();
    }

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
        createHighlight();
        OnSelectionChanged();

        Debug.Log($"Selected {objectType}: {objectToSelect.name}");
    }

    public void ClearSelection()
    {
        if (CurrentSelectedObject == null) return;

        CurrentSelectedObject = null;
        CurrentSelectionType = SelectionType.None;
        removeHighlight();
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

    private void createHighlight()
    {
        if (CurrentSelectedObject == null) return;
        removeHighlight();
        if (selectionHighlightPrefab != null)
        {
            currentHighlightInstance = Instantiate(selectionHighlightPrefab, CurrentSelectedObject.transform);
            positionHighlight();
            setupHighlightSize();
        }
    }

    private void removeHighlight()
    {
        if (currentHighlightInstance == null) { return; }
        Destroy(currentHighlightInstance);
        currentHighlightInstance = null;
    }

    private void positionHighlight()
    {
        if (currentHighlightInstance != null)
        {
            RectTransform rectTransform = currentHighlightInstance.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchorMin = Vector2.zero;
                rectTransform.anchorMax = Vector2.one;
                rectTransform.offsetMin = Vector2.zero;
                rectTransform.offsetMax = Vector2.zero;
                rectTransform.localPosition = Vector3.zero;
                rectTransform.SetAsLastSibling();
            }
        }
    }

    private void setupHighlightSize()
    {
        if (currentHighlightInstance != null && CurrentSelectedObject != null)
        {
            RectTransform highlightRectTransform = currentHighlightInstance.GetComponent<RectTransform>();
            RectTransform selectedObjectRectTransform = CurrentSelectedObject.GetComponent<RectTransform>();
            if (highlightRectTransform != null && selectedObjectRectTransform != null)
            {
                Vector2 selectedObjectSize = selectedObjectRectTransform.sizeDelta;
                highlightRectTransform.sizeDelta = selectedObjectSize * highlightScaleFactor;
            }
        }
    }
}
