using System;
using UnityEngine;

public class CardPoolScreen : GameTemplateEditorScreenBase
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