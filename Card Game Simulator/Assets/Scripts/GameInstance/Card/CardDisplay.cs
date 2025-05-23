using UnityEngine;

[RequireComponent(typeof(CardState))]
public class CardDisplay : MonoBehaviour
{
    private CardState cardState;
    public bool IsDisplayEnabled { get; private set; } = false;

    void Awake()
    {
        cardState = GetComponent<CardState>();
    }

    void Start()
    {
        cardState.Defined += cardState_OnDefined;
        if (cardState.IsDefined)
        {
            initializeCardDisplay();
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
        if (!cardState.IsDefined) { return; }
        SpriteRenderer spriteRenderer = cardState.BackSideGameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = cardState.BackSideSprite;
        ResizeSprite.RecalculateSpriteScale(
            cardState.BackSideGameObject,
            cardState.Width ?? 0,
            cardState.Height ?? 0);
    }

    private void initializeFrontSideObject()
    {
        if (!cardState.IsDefined) { return; }
        SpriteRenderer spriteRenderer = cardState.FrontSideGameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = cardState.FrontSideSprite;
        ResizeSprite.RecalculateSpriteScale(
            cardState.FrontSideGameObject,
            cardState.Width ?? 0,
            cardState.Height ?? 0);
    }

    private void updateViewableCardSide()
    {
        cardState.BackSideGameObject.SetActive(!cardState.IsFaceUp);
        cardState.FrontSideGameObject.SetActive(cardState.IsFaceUp);
    }

    private void enableDisplay()
    {
        updateViewableCardSide();
        IsDisplayEnabled = true;
    }

    private void disableDisplay()
    {
        cardState.BackSideGameObject.SetActive(false);
        cardState.FrontSideGameObject.SetActive(false);
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
