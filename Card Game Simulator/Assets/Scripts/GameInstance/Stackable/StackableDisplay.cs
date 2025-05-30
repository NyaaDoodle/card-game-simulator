using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(StackableState))]
public class StackableDisplay : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private TMP_Text cardCountText;

    [Header("Display Settings")]
    [SerializeField] private float highlightAreaFactor = 1.05f;

    private StackableState stackableState;
    private RectTransform stackableRectTransform;
    private Vector2 originalSize;
    private GameObject currentTopCardObject;

    void Awake()
    {
        stackableState = GetComponent<StackableState>();
        stackableRectTransform = GetComponent<RectTransform>();
        originalSize = stackableRectTransform.sizeDelta;
    }

    void Start()
    {
        stackableState.CardAdded += onCardAdded;
        stackableState.CardRemoved += onCardRemoved;
        stackableState.CardsShuffled += onCardsShuffled;
        stackableState.ChangedIsInteractable += onChangedIsInteractable;
        updateDisplay();
    }

    private void onCardAdded(StackableState _, GameObject cardAdded)
    {
        updateDisplay();
    }

    private void onCardRemoved(StackableState _, GameObject cardRemoved)
    {
        cardRemoved.GetComponent<CardState>().HideCard();
        updateDisplay();
    }

    private void onCardsShuffled(StackableState _)
    {
        updateDisplay();
    }

    private void updateDisplay()
    {
        updateTopCardDisplay();
        updateStackCount();
    }

    private void updateTopCardDisplay()
    {
        currentTopCardObject = stackableState.TopCard;
        if (currentTopCardObject != null)
        {
            CardState topCardState = currentTopCardObject.GetComponent<CardState>();
            topCardState.ShowCard();
        }
        updateRectTransformSize();
    }

    private void updateRectTransformSize()
    {
        Vector2 sizeVectorToSet = currentTopCardObject != null
                                      ? currentTopCardObject.GetComponent<RectTransform>().sizeDelta
                                      : originalSize;
        stackableRectTransform.sizeDelta = sizeVectorToSet * highlightAreaFactor;
    }

    private void updateStackCount()
    {
        if (cardCountText != null)
        {
            int count = stackableState.CardCount;
            cardCountText.text = count > 1 ? count.ToString() : "";
            cardCountText.gameObject.SetActive(count > 1);
        }
    }

    public void onChangedIsInteractable(StackableState _)
    {
        //CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        //canvasGroup.alpha = stackableState.IsInteractable ? 1f : 0.6f;
        //canvasGroup.interactable = stackableState.IsInteractable;
    }

    void OnDestroy()
    {
        if (stackableState != null)
        {
            stackableState.CardAdded -= onCardAdded;
            stackableState.CardRemoved -= onCardRemoved;
            stackableState.CardsShuffled -= onCardsShuffled;
            stackableState.ChangedIsInteractable -= onChangedIsInteractable;
        }
    }
}
