using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CardState))]
public class CardDisplay : MonoBehaviour
{
    [Header("Card UI Components")]
    [SerializeField] private Image frontSideImage;
    [SerializeField] private Image backSideImage;

    private CardState cardState;

    public bool IsDisplayEnabled { get; private set; } = false;

    void Awake()
    {
        cardState = GetComponent<CardState>();
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
    }

    void OnDestroy()
    {
        if (cardState != null)
        {
            cardState.Defined -= onCardDefined;
            cardState.Flipped -= onCardFlipped;
            cardState.Hidden -= onCardHidden;
            cardState.Shown -= onCardShown;
        }
    }

    private void initializeCardDisplay()
    {
        loadCardImages();
        updateVisibleSide();
        IsDisplayEnabled = true;
    }

    private void loadCardImages()
    {
        if (!cardState.IsDefined) return;

        if (frontSideImage != null)
        {
            frontSideImage.sprite = cardState.FrontSideSprite;
        }

        if (backSideImage != null)
        {
            backSideImage.sprite = cardState.BackSideSprite;
        }
    }

    private void updateVisibleSide()
    {
        if (!IsDisplayEnabled) return;

        if (frontSideImage != null)
            frontSideImage.gameObject.SetActive(cardState.IsFaceUp);

        if (backSideImage != null)
            backSideImage.gameObject.SetActive(!cardState.IsFaceUp);
    }

    private void enableDisplay()
    {
        updateVisibleSide();
        IsDisplayEnabled = true;
    }

    private void disableDisplay()
    {
        frontSideImage.gameObject.SetActive(false);
        backSideImage.gameObject.SetActive(false);
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
}
