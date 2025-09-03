using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSelectionModalWindow : SelectionModalWindowBase
{
    [SerializeField] private CardSelectionGrid cardSelectionGrid;
    [SerializeField] private Button continueButton;

    public void Setup(
        string titleText,
        IEnumerable<CardData> cardsData,
        Action<CardData> onSelectCard,
        Action onAddButtonSelect,
        Action onBackButtonSelect,
        Action onContinueButtonSelect)
    {
        SetupBackButton(onBackButtonSelect);
        SetupTitleText(titleText);
        setContinueButton(onContinueButtonSelect);
        cardSelectionGrid.Show(cardsData, onSelectCard, onAddButtonSelect);
    }

    public void Setup(
        string titleText,
        IEnumerable<Card> cards,
        Action<Card> onSelectCard,
        Action onAddButtonSelect,
        Action onBackButtonSelect,
        Action onContinueButtonSelect)
    {
        SetupBackButton(onBackButtonSelect);
        SetupTitleText(titleText);
        setContinueButton(onContinueButtonSelect);
        cardSelectionGrid.Show(cards, onSelectCard, onAddButtonSelect);
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
        cardSelectionGrid.Hide();
        continueButton.onClick.RemoveAllListeners();
        base.OnDestroy();
    }
}
