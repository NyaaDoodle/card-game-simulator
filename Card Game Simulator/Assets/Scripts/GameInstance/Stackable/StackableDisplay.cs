using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(StackableState))]
public class StackableDisplay : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private RectTransform cardDisplayArea;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Text stackCountText;

    [Header("Visual Settings")]
    [SerializeField] private Vector2 defaultContainerSize = new Vector2(200, 300);
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color highlightColor = Color.yellow;

    [Header("Stack Display")]
    [SerializeField] private bool showStackCount = false;

    private StackableState stackableState;
    private RectTransform stackableRectTransform;
    private CardDisplay currentTopCardDisplay;
    private bool isHighlighted = false;

    // Events for interaction
    public event Action<StackableDisplay> OnStackClicked;

    void Awake()
    {
        stackableState = GetComponent<StackableState>();
        stackableRectTransform = GetComponent<RectTransform>();
        stackableRectTransform.sizeDelta = defaultContainerSize;
    }

    void Start()
    {
        setupStackableDisplay();
        updateDisplay();
    }

    private void setupStackableDisplay()
    {
        stackableState.CardsChanged += onCardsChanged;

        if (backgroundImage != null)
        {
            backgroundImage.color = normalColor;
        }

        if (stackCountText != null)
        {
            stackCountText.gameObject.SetActive(showStackCount);
        }
    }

    private void onCardsChanged(StackableState _)
    {
        updateDisplay();
    }

    private void updateDisplay()
    {
        updateTopCardDisplay();
        updateStackCount();
        updateBackgroundColor();
    }

    private void updateTopCardDisplay()
    {
        // Clear current top card display
        if (currentTopCardDisplay != null)
        {
            Destroy(currentTopCardDisplay.gameObject);
            currentTopCardDisplay = null;
        }

        // Create display for top card if stack has cards
        if (stackableState.HasCards)
        {
            GameObject topCardObj = stackableState.TopCard;
            if (topCardObj != null)
            {
                //CreateTopCardDisplay(topCardObj);
            }
        }
    }

    private void updateStackCount()
    {
        if (stackCountText != null && showStackCount)
        {
            int count = stackableState.CardCount;
            stackCountText.text = count > 1 ? count.ToString() : "";
            stackCountText.gameObject.SetActive(count > 1);
        }
    }

    private void updateBackgroundColor()
    {
        if (backgroundImage != null)
        {
            Color targetColor = isHighlighted ? highlightColor : normalColor;
            backgroundImage.color = targetColor;
        }
    }

    public void SetHighlighted(bool highlighted)
    {
        isHighlighted = highlighted;
        updateBackgroundColor();
    }

    public void SetInteractable(bool interactable)
    {
        // Visual feedback for non-interactable state
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        canvasGroup.alpha = interactable ? 1f : 0.6f;
        canvasGroup.interactable = interactable;
    }

    // UI Event System handlers
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Stack clicked: {name} ({stackableState.CardCount} cards)");
        OnStackClicked?.Invoke(this);
    }

    void OnDestroy()
    {
        if (stackableState != null)
        {
            stackableState.CardsChanged -= onCardsChanged;
        }
    }
}
