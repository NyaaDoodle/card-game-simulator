using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour, IPointerClickHandler
{
    [Header("Card Side Images")]
    [SerializeField] private Image backSideImage;
    [SerializeField] private Image frontSideImage;

    public Card Card { get; private set; }

    public void Setup(Card card)
    {
        Card = card;
        subscribeToCardEvents();
        loadCardSprites();
        updateVisibleSide();
    }

    void OnDestroy()
    {
        unsubscribeFromCardEvents();
    }

    private void subscribeToCardEvents()
    {
        if (isCardNotDefined()) return;
        Card.Flipped += onCardFlipped;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (isCardNotDefined()) return;
        Card.NotifySelection();
    }

    private void unsubscribeFromCardEvents()
    {
        if (Card == null) return;
        Card.Flipped -= onCardFlipped;
    }

    private void onCardFlipped(Card _)
    {
        updateVisibleSide();
    }

    private void loadBackSideSprite()
    {
        // TODO allow loading outside of Resources folder
        if (isCardNotDefined() || isBackSideImageNotDefined()) return;
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
        // TODO allow loading outside of Resources folder
        if (isCardNotDefined() || isFrontSideImageNotDefined()) return;
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
        loadBackSideSprite();
        loadFrontSideSprite();
    }

    private void updateVisibleSide()
    {
        if (isCardNotDefined() || isFrontSideImageNotDefined() || isBackSideImageNotDefined()) return;
        frontSideImage.gameObject.SetActive(Card.IsFaceUp);
        backSideImage.gameObject.SetActive(!Card.IsFaceUp);
    }

    private bool isCardNotDefined()
    {
        if (Card == null)
        {
            Debug.LogWarning("Card is null");
            return true;
        }
        return false;
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
