using UnityEngine;

[RequireComponent(typeof(CardState))]
public class CardDisplay : MonoBehaviour
{
    [SerializeField] private GameObject cardBackSideObject;
    [SerializeField] private GameObject cardFrontSideObject;
    private CardState cardState;

    public bool IsDisplayEnabled { get; private set; }

    private const int sortingOrder = 1;

    void Start()
    {
        cardState = GetComponent<CardState>();

        if (cardState.IsDefined)
        {
            initializeCardDisplay();
        }
        else
        {
            cardState.Defined += cardState_OnDefined;
            IsDisplayEnabled = false;
        }
    }

    private void initializeCardDisplay()
    {
        cardState.Flipped += cardState_OnFlipped;
        cardState.Hidden += cardState_OnHidden;
        cardState.Shown += cardState_OnShown;

        initializeCardSideObjects();
        updateViewableCardSide();
        IsDisplayEnabled = true;
    }

    private void initializeCardSideObjects()
    {
        initializeBackSideObject();
        initializeFrontSideObject();
    }

    private void initializeBackSideObject()
    {
        SpriteRenderer spriteRenderer = cardBackSideObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = cardState.BackSideSprite;
        spriteRenderer.sortingOrder = sortingOrder;

        ResizeSpriteRenderScript resizeSpriteRender = cardBackSideObject.GetComponent<ResizeSpriteRenderScript>();
        resizeSpriteRender.enabled = true;
        resizeSpriteRender.Width = cardState.Width ?? 0;
        resizeSpriteRender.Height = cardState.Height ?? 0;
    }

    private void initializeFrontSideObject()
    {
        SpriteRenderer spriteRenderer = cardFrontSideObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = cardState.FrontSideSprite;
        spriteRenderer.sortingOrder = sortingOrder;

        ResizeSpriteRenderScript resizeSpriteRender = cardFrontSideObject.GetComponent<ResizeSpriteRenderScript>();
        resizeSpriteRender.enabled = true;
        resizeSpriteRender.Width = cardState.Width ?? 0;
        resizeSpriteRender.Height = cardState.Height ?? 0;
    }

    private void updateViewableCardSide()
    {
        cardBackSideObject.SetActive(!cardState.IsFaceUp);
        cardFrontSideObject.SetActive(cardState.IsFaceUp);
    }

    private void enableDisplay()
    {
        updateViewableCardSide();
        IsDisplayEnabled = true;
    }

    private void disableDisplay()
    {
        cardBackSideObject.SetActive(false);
        cardFrontSideObject.SetActive(false);
        IsDisplayEnabled = false;
    }

    private void cardState_OnDefined(CardState calledCardState)
    {
        initializeCardDisplay();
    }

    private void cardState_OnFlipped(CardState calledCardState)
    {
        updateViewableCardSide();
    }

    private void cardState_OnShown(CardState calledCardState)
    {
        enableDisplay();
    }

    private void cardState_OnHidden(CardState calledCardState)
    {
        disableDisplay();
    }
}
