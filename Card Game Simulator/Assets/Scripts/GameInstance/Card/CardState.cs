using System;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class CardState : MonoBehaviour
{
    // CardData related properties
    public CardData CardData { get; private set; } = null;
    public bool IsDefined => CardData != null;
    public int? Id => CardData.Id;
    public string Name => CardData.Name;
    public string Description => CardData.Description;
    public float? Width => CardData?.Width;
    public float? Height => CardData?.Height;

    // Card Sprites
    public Sprite BackSideSprite { get; private set; } = null;
    public Sprite FrontSideSprite { get; private set; } = null;

    // Card State Properties
    public bool IsFaceUp { get; private set; } = false;
    public bool IsShown { get; private set; } = false;
    public bool IsInteractable { get; private set; } = false;

    // Card State Events
    public event Action<CardState> Flipped;
    public event Action<CardState> Hidden;
    public event Action<CardState> Shown;
    public event Action<CardState> Defined;
    public event Action<CardState> ChangedIsInteractable;

    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Initialize(CardData cardData)
    {
        if (!IsDefined)
        {
            CardData = cardData;
            setupRectTransformSize();
            setupRectTransformAnchor();
            loadCardSprites();
            setDefaultCardProperties();
            OnDefined();
        }
        else
        {
            Debug.Log($"SetCardData(): Card with id {CardData.Id} has already been set");
        }
    }

    private void setupRectTransformSize()
    {
        rectTransform.sizeDelta = getCardSizeVector();
    }

    private Vector2 getCardSizeVector()
    {
        float cardWidth = Width ?? 0;
        float cardHeight = Height ?? 0;
        return new Vector2(cardWidth * UIConstants.CanvasScaleFactor, cardHeight * UIConstants.CanvasScaleFactor);
    }

    private void setupRectTransformAnchor()
    {
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
    }

    private void setDefaultCardProperties()
    {
        IsFaceUp = false;
        IsShown = false;
        IsInteractable = true;
    }

    public void Flip()
    {
        IsFaceUp = !IsFaceUp;
        OnFlipped();
    }

    public void FlipFaceUp()
    {
        if (IsFaceUp != true)
        {
            Flip();
        }
    }

    public void FlipFaceDown()
    {
        if (IsFaceUp == true)
        {
            Flip();
        }
    }

    public void HideCard()
    {
        IsShown = false;
        OnHidden();
    }

    public void ShowCard()
    {
        IsShown = true;
        OnShown();
    }

    protected virtual void OnFlipped()
    {
        Flipped?.Invoke(this);
    }

    protected virtual void OnHidden()
    {
        Hidden?.Invoke(this);
    }

    protected virtual void OnShown()
    {
        Shown?.Invoke(this);
    }

    protected virtual void OnDefined()
    {
        Defined?.Invoke(this);
    }

    protected virtual void OnChangedIsInteractable()
    {
        ChangedIsInteractable?.Invoke(this);
    }

    private void loadCardSprites()
    {
        // TODO allow loading outside of Resources folder
        BackSideSprite = Resources.Load<Sprite>(CardData.BackSideSpritePath);
        if (BackSideSprite == null)
        {
            Debug.LogError($"CardState: Failed to load sprite at {CardData.BackSideSpritePath}");
        }

        FrontSideSprite = Resources.Load<Sprite>(CardData.FrontSideSpritePath);
        if (FrontSideSprite == null)
        {
            Debug.LogError($"CardState: Failed to load sprite at {CardData.FrontSideSpritePath}");
        }
    }
}
