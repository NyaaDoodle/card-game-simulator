using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    private Image cardBackSideImage;
    private Image cardFrontSideImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CardState cardState = GetComponent<CardState>();
        cardState.Flipped += cardState_OnFlipped;
        createSidesImageChildren(cardState);
        changeCardSideDisplay(cardState);
    }

    private void createSidesImageChildren(CardState cardState)
    {
        // TODO generalize it, for use with text-only cards and such.
        const float cardDimensionFactor = 100;
        float cardWidth = cardState.Width * cardDimensionFactor;
        float cardHeight = cardState.Height * cardDimensionFactor;
        Vector2 cardSizeVector = new Vector2(cardWidth, cardHeight);

        GameObject cardBackSide = new GameObject();
        cardBackSide.AddComponent<Image>();
        cardBackSide.GetComponent<Image>().sprite = cardState.BackSideSprite;
        cardBackSide.transform.parent = this.transform;
        cardBackSide.GetComponent<RectTransform>().sizeDelta = cardSizeVector;

        GameObject cardFrontSide = new GameObject();
        cardFrontSide.AddComponent<Image>();
        cardFrontSide.GetComponent<Image>().sprite = cardState.FrontSideSprite;
        cardFrontSide.GetComponent<RectTransform>().sizeDelta = cardSizeVector;
        cardFrontSide.transform.parent = this.transform;
    }

    private void changeCardSideDisplay(CardState cardState)
    {
        cardBackSideImage.enabled = !cardState.IsFaceUp;
        cardFrontSideImage.enabled = cardState.IsFaceUp;
    }

    private void cardState_OnFlipped(CardState cardState)
    {
        changeCardSideDisplay(cardState);
    }
}
