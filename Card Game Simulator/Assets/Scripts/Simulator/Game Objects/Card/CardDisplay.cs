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
        LoggerReferences.Instance.CardDisplayLogger.LogMethod();
        Card = card;
        loadCardSprites();
        updateVisibleSide();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        LoggerReferences.Instance.CardDisplayLogger.LogMethod();
        OnSelected();
    }

    protected virtual void OnSelected()
    {
        LoggerReferences.Instance.CardDisplayLogger.LogMethod();
        Selected?.Invoke(Card);
    }

    private void loadBackSideSprite()
    {
        LoggerReferences.Instance.CardDisplayLogger.LogMethod();
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
        LoggerReferences.Instance.CardDisplayLogger.LogMethod();
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
        LoggerReferences.Instance.CardDisplayLogger.LogMethod();
        loadBackSideSprite();
        loadFrontSideSprite();
    }

    private void updateVisibleSide()
    {
        LoggerReferences.Instance.CardDisplayLogger.LogMethod();
        if (isFrontSideImageNotDefined() || isBackSideImageNotDefined()) return;
        frontSideImage.gameObject.SetActive(Card.IsFaceUp);
        backSideImage.gameObject.SetActive(!Card.IsFaceUp);
    }

    private bool isBackSideImageNotDefined()
    {
        LoggerReferences.Instance.CardDisplayLogger.LogMethod();
        if (backSideImage == null)
        {
            Debug.LogWarning("BackSideImage is null");
            return true;
        }
        return false;
    }

    private bool isFrontSideImageNotDefined()
    {
        LoggerReferences.Instance.CardDisplayLogger.LogMethod();
        if (frontSideImage == null)
        {
            Debug.LogWarning("FrontSideImage is null");
            return true;
        }
        return false;
    }
}
