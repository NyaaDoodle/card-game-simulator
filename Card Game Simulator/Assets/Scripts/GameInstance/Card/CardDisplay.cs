using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CardState))]
[RequireComponent(typeof(CanvasGroup))]
public class CardDisplay : MonoBehaviour, IPointerClickHandler
{
    [Header("Card UI Components")]
    [SerializeField] private GameObject frontSideObject;
    [SerializeField] private GameObject backSideObject;
    [SerializeField] private GameObject cardButtonObject;

    [Header("Card Display Settings")]
    [SerializeField] private float nonInteractableCardAlpha = 0.6f;

    private CardState cardState;
    private RectTransform rectTransform;
    private bool isInteractable = true;

    // Interaction Events
    public event Action<CardDisplay> OnCardClicked;

    public bool IsDisplayEnabled { get; private set; } = false;

    void Awake()
    {
        cardState = GetComponent<CardState>();
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        setupCardDisplay();
        setupInteraction();
    }

    private void setupCardDisplay()
    {
        subscribeToCardStateEvents();

        if (cardState.IsDefined)
        {
            initializeCardDisplay();
        }
    }

    private void subscribeToCardStateEvents()
    {
        cardState.Defined += OnCardDefined;
        cardState.Flipped += OnCardFlipped;
        cardState.Hidden += OnCardHidden;
        cardState.Shown += OnCardShown;
    }

    void OnDestroy()
    {
        if (cardState != null)
        {
            cardState.Defined -= OnCardDefined;
            cardState.Flipped -= OnCardFlipped;
            cardState.Hidden -= OnCardHidden;
            cardState.Shown -= OnCardShown;
        }
    }

    private void setupInteraction()
    {
        Button cardButton = cardButtonObject.GetComponent<Button>();
        if (cardButton != null)
        {
            cardButton.onClick.AddListener(HandleCardClick);
        }

        // Ensure images are raycast targets for interaction
        //if (frontSideImage != null)
        //    frontSideImage.raycastTarget = true;
        //if (backSideImage != null)
        //    backSideImage.raycastTarget = true;
    }

    private void initializeCardDisplay()
    {
        setupRectTransformSize();
        setupRectTransformAnchor();
        loadCardImages();
        updateVisibleSide();
        IsDisplayEnabled = true;
    }

    private void setupRectTransformSize()
    {
        rectTransform.sizeDelta = getCardSizeVector();
    }

    private Vector2 getCardSizeVector()
    {
        float cardWidth = cardState.Width ?? 0;
        float cardHeight = cardState.Height ?? 0;
        return new Vector2(cardWidth * UIConstants.CanvasScaleFactor, cardHeight * UIConstants.CanvasScaleFactor);
    }

    private void setupRectTransformAnchor()
    {
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
    }

    private void loadCardImages()
    {
        if (!cardState.IsDefined) return;

        Image frontSideImage = frontSideObject.GetComponent<Image>();
        if (frontSideImage != null)
        {
            frontSideImage.sprite = cardState.FrontSideSprite;
        }

        Image backSideImage = backSideObject.GetComponent<Image>();
        if (backSideImage != null)
        {
            backSideImage.sprite = cardState.BackSideSprite;
        }
    }

    private void updateVisibleSide()
    {
        if (!IsDisplayEnabled) return;

        if (frontSideObject != null)
            frontSideObject.SetActive(cardState.IsFaceUp);

        if (backSideObject != null)
            backSideObject.SetActive(!cardState.IsFaceUp);
    }

    private void enableDisplay()
    {
        updateVisibleSide();
        IsDisplayEnabled = true;
    }

    private void disableDisplay()
    {
        frontSideObject.SetActive(false);
        backSideObject.SetActive(false);
        IsDisplayEnabled = false;
    }

    public void setInteractable(bool interactable)
    {
        isInteractable = interactable;

        Button cardButton = cardButtonObject.GetComponent<Button>();
        if (cardButton != null)
        {
            cardButton.interactable = interactable;
        }

        updateInteractDisplay();
    }

    public void updateInteractDisplay()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = isInteractable ? 1f : nonInteractableCardAlpha;
            canvasGroup.interactable = isInteractable;
        }
    }

    // UI Event System handlers - These work with New Input System automatically
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isInteractable)
        {
            HandleCardClick();
        }
    }

    private void HandleCardClick()
    {
        Debug.Log($"Card clicked: {cardState.Name}");
        OnCardClicked?.Invoke(this);
    }

    private void OnCardDefined(CardState _)
    {
        initializeCardDisplay();
    }

    private void OnCardFlipped(CardState _)
    {
        updateVisibleSide();
    }

    private void OnCardHidden(CardState _)
    {
        disableDisplay();
    }

    private void OnCardShown(CardState _)
    {
        enableDisplay();
    }
}
