using System;
using System.Collections.Generic;
using UnityEngine;

public class DeckSelectionModalWindow : SelectionModalWindowBase
{
    [SerializeField] private DeckSelectionGrid deckSelectionGrid;
    
    public void Setup(
        string titleText,
        IEnumerable<DeckData> decksData,
        Action<DeckData> onSelectDeck,
        Action onAddButtonSelect,
        Action onBackButtonSelect)
    {
        SetupBackButton(onBackButtonSelect);
        SetupTitleText(titleText);
        deckSelectionGrid.Show(decksData, onSelectDeck, onAddButtonSelect);
    }
    
    protected override void OnDestroy()
    {
        deckSelectionGrid.Hide();
        base.OnDestroy();
    }
}
