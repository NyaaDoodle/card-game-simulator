using System;
using UnityEngine;

public class GameTemplateSelectionModalWindow : SelectionModalWindowBase
{
    [SerializeField] private GameTemplateSelectionGrid gameTemplateSelectionGrid;

    public void Setup(
        string titleText,
        Action<GameTemplate> onSelectGameTemplate,
        Action onAddButtonSelect,
        Action onBackButtonSelect)
    {
        SetupBackButton(onBackButtonSelect);
        SetupTitleText(titleText);
        gameTemplateSelectionGrid.Show(onSelectGameTemplate, onAddButtonSelect);
    }

    protected override void OnDestroy()
    {
        gameTemplateSelectionGrid.Hide();
        base.OnDestroy();
    }
}
