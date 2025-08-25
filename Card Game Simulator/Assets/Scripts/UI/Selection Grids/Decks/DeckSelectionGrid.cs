using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckSelectionGrid : MonoBehaviour
{
    [SerializeField] private RectTransform contentContainer;
    [SerializeField] private Button newDeckButton;

    private readonly List<DeckSelectionEntity> selectionEntities = new List<DeckSelectionEntity>();

    public void Show(IEnumerable<DeckData> decksData, Action<DeckData> onSelectDeck, Action onSelectAddButton)
    {
        gameObject.SetActive(true);
        setupAddButton(onSelectAddButton);
        spawnDeckSelectionEntities(decksData, onSelectDeck);
    }

    public void Hide()
    {
        despawnDeckSelectionEntities();
        unsetAddButton();
        gameObject.SetActive(false);
    }

    private void setupAddButton(Action onSelectAddButton)
    {
        bool showAddButton = onSelectAddButton == null;
        newDeckButton.gameObject.SetActive(showAddButton);
        newDeckButton.onClick.AddListener(() => onSelectAddButton?.Invoke());
    }

    private void unsetAddButton()
    {
        newDeckButton.onClick.RemoveAllListeners();
    }

    private void spawnDeckSelectionEntities(IEnumerable<DeckData> decksData, Action<DeckData> onSelectDeck)
    {
        foreach (DeckData deckData in decksData)
        {
            spawnDeckSelectionEntity(deckData, onSelectDeck);    
        }
        setAddButtonAsLastContentSibling();
    }

    private void despawnDeckSelectionEntities()
    {
        foreach (DeckSelectionEntity selectionEntity in selectionEntities)
        {
            Destroy(selectionEntity.gameObject);
        }
        selectionEntities.Clear();
    }

    private void spawnDeckSelectionEntity(DeckData deckData, Action<DeckData> onSelectDeck)
    {
        DeckSelectionEntity deckSelectionEntity =
            PrefabExtensions.InstantiateDeckSelectionEntity(deckData, onSelectDeck, contentContainer);
        selectionEntities.Add(deckSelectionEntity);
    }

    private void setAddButtonAsLastContentSibling()
    {
        newDeckButton.transform.SetAsLastSibling();
    }
}
