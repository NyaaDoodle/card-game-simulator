using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSelectionGrid : MonoBehaviour
{
    [SerializeField] private RectTransform contentContainer;
    [SerializeField] private Button newCardButton;

    private readonly List<CardSelectionEntity> selectionEntities = new List<CardSelectionEntity>();
    private Action<CardData> onSelectCardDataAction;
    private Action<Card> onSelectCardAction;

    public void Show(
        IEnumerable<CardData> cardsData,
        Action<CardData> onSelectCard,
        Action onSelectAddButton = null)
    {
        gameObject.SetActive(true);
        setupAddButton(onSelectAddButton);
        onSelectCardDataAction = onSelectCard;
        spawnCardSelectionEntities(cardsData);
    }

    public void Show(IEnumerable<Card> cards, Action<Card> onSelectCard, Action onSelectAddButton = null)
    {
        gameObject.SetActive(true);
        setupAddButton(onSelectAddButton);
        onSelectCardAction = onSelectCard;
        spawnCardSelectionEntities(cards);
    }

    public void Show(
        IEnumerable<string> cardIds,
        WorkingGameTemplate workingGameTemplate,
        Action<CardData> onSelectCard,
        Action onSelectAddButton = null)
    {
        IEnumerable<CardData> cardsData = workingGameTemplate.GetCardDataListFromCardIds(cardIds);
        this.Show(cardsData, onSelectCard, onSelectAddButton);
    }

    public void UpdateCards(IEnumerable<CardData> cardsData)
    {
        despawnCardSelectionEntities();
        spawnCardSelectionEntities(cardsData);
    }

    public void UpdateCards(IEnumerable<Card> cards)
    {
        despawnCardSelectionEntities();
        spawnCardSelectionEntities(cards);
    }

    public void UpdateCards(IEnumerable<string> cardIds, WorkingGameTemplate workingGameTemplate)
    {
        IEnumerable<CardData> cardsData = workingGameTemplate.GetCardDataListFromCardIds(cardIds);
        UpdateCards(cardsData);
    }

    public void Hide()
    {
        onSelectCardDataAction = null;
        onSelectCardAction = null;
        despawnCardSelectionEntities();
        unsetAddButton();
        gameObject.SetActive(false);
    }

    private void setupAddButton(Action onSelectAddButton)
    {
        bool showAddButton = onSelectAddButton != null;
        newCardButton.gameObject.SetActive(showAddButton);
        newCardButton.onClick.AddListener(() => onSelectAddButton?.Invoke());
    }

    private void unsetAddButton()
    {
        newCardButton.onClick.RemoveAllListeners();
    }

    private void spawnCardSelectionEntities(IEnumerable<CardData> cardsData)
    {
        foreach (CardData cardData in cardsData)
        {
            spawnCardSelectionEntity(cardData, onSelectCardDataAction);    
        }
        setAddButtonAsLastContentSibling();
    }
    
    private void spawnCardSelectionEntities(IEnumerable<Card> cards)
    {
        foreach (Card card in cards)
        {
            spawnCardSelectionEntity(card, onSelectCardAction);    
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
