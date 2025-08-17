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
        LoggingManager.Instance.CardDisplayLogger.LogMethod();
        Card = card;
        loadCardSprites();
        updateVisibleSide();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        LoggingManager.Instance.CardDisplayLogger.LogMethod();
        OnSelected();
    }

    protected virtual void OnSelected()
    {
        LoggingManager.Instance.CardDisplayLogger.LogMethod();
        Selected?.Invoke(Card);
    }

    private void loadBackSideSprite()
    {
        LoggingManager.Instance.CardDisplayLogger.LogMethod();
        // TODO allow loading outside of Resources folder
        if (isBackSideImageNotDefined()) return;
        string backSideSpritePath = Card.CardData.BackSideSpritePath;
        Sprite backSideSprite = Resources.Load<Sprite>(backSideSpritePath);
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
        LoggingManager.Instance.CardDisplayLogger.LogMethod();
        // TODO allow loading outside of Resources folder
        if (isFrontSideImageNotDefined()) return;
        string frontSideSpritePath = Card.CardData.FrontSideSpritePath;
        Sprite frontSideSprite = Resources.Load<Sprite>(frontSideSpritePath);
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
        LoggingManager.Instance.CardDisplayLogger.LogMethod();
        loadBackSideSprite();
        loadFrontSideSprite();
    }

    private void updateVisibleSide()
    {
        LoggingManager.Instance.CardDisplayLogger.LogMethod();
        if (isFrontSideImageNotDefined() || isBackSideImageNotDefined()) return;
        frontSideImage.gameObject.SetActive(Card.IsFaceUp);
        backSideImage.gameObject.SetActive(!Card.IsFaceUp);
    }

    private bool isBackSideImageNotDefined()
    {
        LoggingManager.Instance.CardDisplayLogger.LogMethod();
        if (backSideImage == null)
        {
            Debug.LogWarning("BackSideImage is null");
            return true;
        }
        return false;
    }

    private bool isFrontSideImageNotDefined()
    {
        LoggingManager.Instance.CardDisplayLogger.LogMethod();
        if (frontSideImage == null)
        {
            Debug.LogWarning("FrontSideImage is null");
            return true;
        }
        return false;
    }
}
