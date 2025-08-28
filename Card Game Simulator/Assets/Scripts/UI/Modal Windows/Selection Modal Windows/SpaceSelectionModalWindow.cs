using System;
using System.Collections.Generic;
using UnityEngine;

public class SpaceSelectionModalWindow : SelectionModalWindowBase
{
    [SerializeField] private SpaceSelectionGrid spaceSelectionGrid;
    public void Setup(
        string titleText,
        IEnumerable<SpaceData> spacesData,
        Action<SpaceData> onSelectSpace,
        Action onAddButtonSelect,
        Action onBackButtonSelect)
    {
        SetupBackButton(onBackButtonSelect);
        SetupTitleText(titleText);
        spaceSelectionGrid.Show(spacesData, onSelectSpace, onAddButtonSelect);
    }
    
    protected override void OnDestroy()
    {
        spaceSelectionGrid.Hide();
        base.OnDestroy();
    }
}
