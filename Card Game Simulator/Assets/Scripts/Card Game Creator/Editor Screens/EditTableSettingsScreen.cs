using System;
using UnityEngine;

public class EditTableSettingsScreen : GameTemplateEditorScreenBase
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