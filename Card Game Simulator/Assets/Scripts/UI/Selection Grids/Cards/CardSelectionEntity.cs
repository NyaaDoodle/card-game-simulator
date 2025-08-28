using System;
using UnityEngine;
using UnityEngine.UI;

public class CardSelectionEntity : MonoBehaviour
{
    [SerializeField] private Button selectionButton;
    [SerializeField] private Image cardFrontSideImage;
    [SerializeField] private Sprite fallbackSprite;

    public void Setup(CardData cardData, Action<CardData> onSelectAction)
    {
        setupSelectionButton(cardData, onSelectAction);
        setupCardFrontSideImage(cardData);
    }

    public void Setup(Card card, Action<Card> onSelectAction)
    {
        setupSelectionButton(card, onSelectAction);
        setupCardFrontSideImage(card);
    }

    private void OnDestroy()
    {
        removeSelectionButtonListeners();
    }

    private void setupSelectionButton(CardData cardData, Action<CardData> onSelectAction)
    {
        selectionButton.onClick.AddListener(() => onSelectAction?.Invoke(cardData));
    }

    private void setupSelectionButton(Card card, Action<Card> onSelectAction)
    {
        selectionButton.onClick.AddListener(() => onSelectAction?.Invoke(card));
    }

    private void setupCardFrontSideImage(CardData cardData)
    {
        string frontImagePath = cardData.FrontSideImagePath;
        SimulatorImageLoader.LoadSpriteLocalPath(frontImagePath, cardFrontSideImage, fallbackSprite);
    }

    private void setupCardFrontSideImage(Card card)
    {
        string frontImagePath = card.CardData.FrontSideImagePath;
        SimulatorImageLoader.LoadSpriteLocalPath(frontImagePath, cardFrontSideImage, fallbackSprite);
    }

    private void removeSelectionButtonListeners()
    {
        selectionButton.onClick.RemoveAllListeners();
    }
}
