using System;
using UnityEngine;

public class SpaceSelectionScreen : GameTemplateEditorScreenBase
{
    public void Show(WorkingGameTemplate workingGameTemplate)
    {
        SetupBaseButtons(workingGameTemplate, () => goToGameTemplateSectionsScreen(workingGameTemplate));
        gameObject.SetActive(true);
    }

    private void hide()
    {
        UnsetBaseButtons();
        gameObject.SetActive(false);
    }
    
    private void goToGameTemplateSectionsScreen(WorkingGameTemplate workingGameTemplate)
    {
        this.hide();
        GameTemplateEditorScreenReferences.Instance.GameTemplateSectionsScreen.Show(workingGameTemplate);
    }
}
