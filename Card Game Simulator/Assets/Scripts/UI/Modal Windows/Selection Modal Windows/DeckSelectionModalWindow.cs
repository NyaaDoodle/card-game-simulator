using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckSelectionModalWindow : SelectionModalWindowBase
{
    [SerializeField] private DeckSelectionGrid deckSelectionGrid;
    [SerializeField] private Button continueButton;
    
    public void Setup(
        string titleText,
        IEnumerable<DeckData> decksData,
        Action<DeckData> onSelectDeck,
        Action onAddButtonSelect,
        Action onBackButtonSelect,
        Action onContinueButtonSelect)
    {
        SetupBackButton(onBackButtonSelect);
        SetupTitleText(titleText);
        setContinueButton(onContinueButtonSelect);
        deckSelectionGrid.Show(decksData, onSelectDeck, onAddButtonSelect);
    }
    
    private void setContinueButton(Action onContinueButtonSelect)
    {
        bool activateContinueButton = onContinueButtonSelect != null;
        if (activateContinueButton)
        {
            continueButton.onClick.AddListener(() => onContinueButtonSelect?.Invoke());
        }
        continueButton.gameObject.SetActive(activateContinueButton);
    }
    
    protected override void OnDestroy()
    {
        deckSelectionGrid.Hide();
        continueButton.onClick.RemoveAllListeners();
        base.OnDestroy();
    }
}
