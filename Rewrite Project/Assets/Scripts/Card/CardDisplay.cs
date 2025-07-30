using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour, IPointerClickHandler
{
    [Header("Card Side Images")]
    [SerializeField] private Image backSideImage;
    [SerializeField] private Image frontSideImage;

    public CardState CardState { get; private set; }

    public void Setup(CardState cardState)
    {
        CardState = cardState;
        subscribeToCardStateEvents();
        loadCardSprites();
        updateVisibleSide();
    }

    void OnDestroy()
    {
        unsubscribeFromCardStateEvents();
    }

    private void subscribeToCardStateEvents()
    {
        if (isCardNotDefined()) return;
        CardState.Flipped += onCardFlipped;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (isCardNotDefined()) return;
        Debug.Log($"Card {CardState.CardData.Id} clicked");
    }

    private void unsubscribeFromCardStateEvents()
    {
        if (CardState == null) return;
        CardState.Flipped -= onCardFlipped;
    }

    private void onCardFlipped(CardState _)
    {
        updateVisibleSide();
    }

    private void loadBackSideSprite()
    {
        // TODO allow loading outside of Resources folder
        if (isCardNotDefined() || isBackSideImageNotDefined()) return;
        string backSideSpritePath = CardState.CardData.BackSideSpritePath;
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
        string frontSideSpritePath = CardState.CardData.FrontSideSpritePath;
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
        frontSideImage.gameObject.SetActive(CardState.IsFaceUp);
        backSideImage.gameObject.SetActive(!CardState.IsFaceUp);
    }

    private bool isCardNotDefined()
    {
        if (CardState == null)
        {
            Debug.LogWarning("CardState is null");
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
