using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSelectionGrid : MonoBehaviour
{
    [SerializeField] private RectTransform contentContainer;
    [SerializeField] private Button newCardButton;

    private readonly List<CardSelectionEntity> selectionEntities = new List<CardSelectionEntity>();

    public void Show(
        IEnumerable<CardData> cardsData,
        Action<CardData> onSelectCard,
        Action onSelectAddButton)
    {
        gameObject.SetActive(true);
        setupAddButton(onSelectAddButton);
        spawnCardSelectionEntities(cardsData, onSelectCard);
    }

    public void Show(IEnumerable<Card> cards, Action<Card> onSelectCard, Action onSelectAddButton)
    {
        gameObject.SetActive(true);
        setupAddButton(onSelectAddButton);
        spawnCardSelectionEntities(cards, onSelectCard);
    }

    public void Hide()
    {
        despawnCardSelectionEntities();
        unsetAddButton();
        gameObject.SetActive(false);
    }

    private void setupAddButton(Action onSelectAddButton)
    {
        bool showAddButton = onSelectAddButton == null;
        newCardButton.gameObject.SetActive(showAddButton);
        newCardButton.onClick.AddListener(() => onSelectAddButton?.Invoke());
    }

    private void unsetAddButton()
    {
        newCardButton.onClick.RemoveAllListeners();
    }

    private void spawnCardSelectionEntities(IEnumerable<CardData> cardsData, Action<CardData> onSelectCard)
    {
        foreach (CardData cardData in cardsData)
        {
            spawnCardSelectionEntity(cardData, onSelectCard);    
        }
        setAddButtonAsLastContentSibling();
    }
    
    private void spawnCardSelectionEntities(IEnumerable<Card> cards, Action<Card> onSelectCard)
    {
        foreach (Card card in cards)
        {
            spawnCardSelectionEntity(card, onSelectCard);    
        }
        setAddButtonAsLastContentSibling();
    }

    private void despawnCardSelectionEntities()
    {
        foreach (CardSelectionEntity selectionEntity in selectionEntities)
        {
            Destroy(selectionEntity.gameObject);
        }
        selectionEntities.Clear();
    }

    private void spawnCardSelectionEntity(CardData cardData, Action<CardData> onSelectCard)
    {
        CardSelectionEntity cardSelectionEntity =
            PrefabExtensions.InstantiateCardSelectionEntity(cardData, onSelectCard, contentContainer);
        selectionEntities.Add(cardSelectionEntity);
    }
    
    private void spawnCardSelectionEntity(Card card, Action<Card> onSelectCard)
    {
        CardSelectionEntity cardSelectionEntity =
            PrefabExtensions.InstantiateCardSelectionEntity(
                card,
                onSelectCard,
                contentContainer);
        selectionEntities.Add(cardSelectionEntity);
    }

    private void setAddButtonAsLastContentSibling()
    {
        newCardButton.transform.SetAsLastSibling();
    }
}
