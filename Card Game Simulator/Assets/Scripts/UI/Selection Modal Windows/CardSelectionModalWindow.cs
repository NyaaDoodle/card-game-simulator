using System;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectionModalWindow : SelectionModalWindowBase
{
    [SerializeField] private CardSelectionGrid cardSelectionGrid;

    public void Setup(
        string titleText,
        IEnumerable<CardData> cardsData,
        Action<CardData> onSelectCard,
        Action onAddButtonSelect,
        Action onBackButtonSelect)
    {
        SetupBackButton(onBackButtonSelect);
        SetupTitleText(titleText);
        cardSelectionGrid.Show(cardsData, onSelectCard, onAddButtonSelect);
    }

    public void Setup(
        string titleText,
        IEnumerable<Card> cards,
        Action<Card> onSelectCard,
        Action onAddButtonSelect,
        Action onBackButtonSelect)
    {
        SetupBackButton(onBackButtonSelect);
        SetupTitleText(titleText);
        cardSelectionGrid.Show(cards, onSelectCard, onAddButtonSelect);
    }

    protected override void OnDestroy()
    {
        cardSelectionGrid.Hide();
        base.OnDestroy();
    }
}
