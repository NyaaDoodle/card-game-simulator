using System;
using UnityEngine;

public class GameTemplateSectionsScreen : GameTemplateEditorScreenBase
{
    public void Show(WorkingGameTemplate workingGameTemplate, Action onBackButtonSelect)
    {
        gameObject.SetActive(true);
        SetupBaseButtons(onBackButtonSelect);
    }

    public void Hide()
    {
        UnsetBaseButtons();
        gameObject.SetActive(false);
    }
}
