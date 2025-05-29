using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CardState))]
[RequireComponent(typeof(CanvasGroup))]
public class CardDisplay : MonoBehaviour
{
    [Header("Card UI Components")]
    [SerializeField] private GameObject frontSideObject;
    [SerializeField] private GameObject backSideObject;
    [SerializeField] private GameObject cardButtonObject;

    [Header("Card Display Settings")]
    [SerializeField] private float nonInteractableCardAlpha = 0.6f;

    private CardState cardState;
    private RectTransform rectTransform;

    public bool IsDisplayEnabled { get; private set; } = false;

    void Awake()
    {
        cardState = GetComponent<CardState>();
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        setupCardDisplay();
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
        cardState.Defined += onCardDefined;
        cardState.Flipped += onCardFlipped;
        cardState.Hidden += onCardHidden;
        cardState.Shown += onCardShown;
        cardState.ChangedIsInteractable += onChangedIsInteractable;
    }

    void OnDestroy()
    {
        if (cardState != null)
        {
            cardState.Defined -= onCardDefined;
            cardState.Flipped -= onCardFlipped;
            cardState.Hidden -= onCardHidden;
            cardState.Shown -= onCardShown;
            cardState.ChangedIsInteractable -= onChangedIsInteractable;
        }
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

    private void onCardDefined(CardState _)
    {
        initializeCardDisplay();
    }

    private void onCardFlipped(CardState _)
    {
        updateVisibleSide();
    }

    private void onCardHidden(CardState _)
    {
        disableDisplay();
    }

    private void onCardShown(CardState _)
    {
        enableDisplay();
    }

    private void onChangedIsInteractable(CardState _)
    {
        Button cardButton = cardButtonObject.GetComponent<Button>();
        if (cardButton != null)
        {
            cardButton.interactable = cardState.IsInteractable;
        }

        updateInteractDisplay();
    }

    private void updateInteractDisplay()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = cardState.IsInteractable ? 1f : nonInteractableCardAlpha;
            canvasGroup.interactable = cardState.IsInteractable;
        }
    }
}
