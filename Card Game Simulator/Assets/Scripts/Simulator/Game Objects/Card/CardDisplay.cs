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

    public void Setup(Card card)
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
        if (isBackSideImageNotDefined()) return;
        string backSideSpritePath = Card.CardData.BackSideImagePath;
        Sprite backSideSprite = ImageLoader.LoadImageAsset(backSideSpritePath);
        if (backSideSprite != null)
        {
            backSideImage.sprite = backSideSprite;
        }
        else
        {
            Debug.LogError($"Failed to load sprite at {backSideSpritePath}");
        }
    }

    private void loadFrontSideSprite()
    {
        if (isFrontSideImageNotDefined()) return;
        string frontSideSpritePath = Card.CardData.FrontSideImagePath;
        Sprite frontSideSprite = ImageLoader.LoadImageAsset(frontSideSpritePath);
        if (frontSideSprite != null)
        {
            frontSideImage.sprite = frontSideSprite;
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
