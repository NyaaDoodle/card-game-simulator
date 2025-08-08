using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour, IPointerClickHandler
{
    [Header("Card Side Images")]
    [SerializeField] private Image backSideImage;
    [SerializeField] private Image frontSideImage;

    public Card Card { get; private set; }
    public event Action<Card> Selected;
    private bool isBackSideSpriteLoaded = false;
    private bool isFrontSideSpriteLoaded = false;

    public void UpdateCard(Card card)
    {
        Card = card;
        loadCardSprites();
        updateVisibleSide();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        OnSelected();
    }

    protected virtual void OnSelected()
    {
        Selected?.Invoke(Card);
    }

    private void loadBackSideSprite()
    {
        // TODO allow loading outside of Resources folder
        if (isBackSideSpriteLoaded || isBackSideImageNotDefined()) return;
        string backSideSpritePath = Card.CardData.BackSideSpritePath;
        Sprite backSideSprite = Resources.Load<Sprite>(backSideSpritePath);
        if (backSideSprite != null)
        {
            backSideImage.sprite = backSideSprite;
            isBackSideSpriteLoaded = true;
        }
        else
        {
            Debug.LogError($"Failed to load sprite at {backSideSpritePath}");
        }
    }

    private void loadFrontSideSprite()
    {
        // TODO allow loading outside of Resources folder
        if (isFrontSideSpriteLoaded || isFrontSideImageNotDefined()) return;
        string frontSideSpritePath = Card.CardData.FrontSideSpritePath;
        Sprite frontSideSprite = Resources.Load<Sprite>(frontSideSpritePath);
        if (frontSideSprite != null)
        {
            frontSideImage.sprite = frontSideSprite;
            isFrontSideSpriteLoaded = true;
        }
        else
        {
            Debug.LogError($"Failed to load sprite at {frontSideSpritePath}");
        }
    }

    private void loadCardSprites()
    {
        loadBackSideSprite();
        loadFrontSideSprite();
    }

    private void updateVisibleSide()
    {
        if (isFrontSideImageNotDefined() || isBackSideImageNotDefined()) return;
        frontSideImage.gameObject.SetActive(Card.IsFaceUp);
        backSideImage.gameObject.SetActive(!Card.IsFaceUp);
    }

    private bool isBackSideImageNotDefined()
    {
        if (backSideImage == null)
        {
            Debug.LogWarning("BackSideImage is null");
            return true;
        }
        return false;
    }

    private bool isFrontSideImageNotDefined()
    {
        if (frontSideImage == null)
        {
            Debug.LogWarning("FrontSideImage is null");
            return true;
        }
        return false;
    }
}
