using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckSelectionEntity : MonoBehaviour
{
    [SerializeField] private Button selectionButton;
    [SerializeField] private TMP_Text deckName;

    public void Setup(DeckData deckData, Action<DeckData> onSelectAction)
    {
        setupSelectionButton(deckData, onSelectAction);
        setupDeckName(deckData);
    }

    private void OnDestroy()
    {
        removeSelectionButtonListeners();
    }

    private void setupSelectionButton(DeckData deckData, Action<DeckData> onSelectAction)
    {
        selectionButton.onClick.AddListener(() => onSelectAction(deckData));
    }

    private void removeSelectionButtonListeners()
    {
        selectionButton.onClick.RemoveAllListeners();
    }

    private void setupDeckName(DeckData deckData)
    {
        deckName.text = deckData.Name;
    }
}
