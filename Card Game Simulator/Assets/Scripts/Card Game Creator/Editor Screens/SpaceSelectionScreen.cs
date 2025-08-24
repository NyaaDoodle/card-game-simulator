using System;
using UnityEngine;

public class SpaceSelectionScreen : GameTemplateEditorScreenBase
{
    public void Show(WorkingGameTemplate workingGameTemplate, Action onBackButtonSelect)
    {
        SetupBaseButtons(onBackButtonSelect);
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        UnsetBaseButtons();
        gameObject.SetActive(false);
    }
}
