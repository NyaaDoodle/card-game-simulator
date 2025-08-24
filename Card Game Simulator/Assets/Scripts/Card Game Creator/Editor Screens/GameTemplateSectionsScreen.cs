using System;
using UnityEngine;
using UnityEngine.UI;

public class GameTemplateSectionsScreen : GameTemplateEditorScreenBase
{
    [SerializeField] private Button editGameTemplateDetailsButton;
    [SerializeField] private Button editTableSettingsButton;
    [SerializeField] private Button cardPoolButton;
    [SerializeField] private Button deckSelectionButton;
    [SerializeField] private Button spaceSelectionButton;
    
    public void Show(WorkingGameTemplate workingGameTemplate)
    {
        gameObject.SetActive(true);
        SetupBaseButtons(workingGameTemplate, goToGameTemplateSelectionScreen);
        setupSectionButtons(workingGameTemplate);
    }

    private void hide()
    {
        UnsetBaseButtons();
        unsetSectionButtons();
        gameObject.SetActive(false);
    }

    private void setupSectionButtons(WorkingGameTemplate workingGameTemplate)
    {
        editGameTemplateDetailsButton.onClick.AddListener(() => goToEditGameTemplateDetails(workingGameTemplate));
        editTableSettingsButton.onClick.AddListener(() => goToEditTableSettings(workingGameTemplate));
        cardPoolButton.onClick.AddListener(() => goToCardPool(workingGameTemplate));
        deckSelectionButton.onClick.AddListener(() => goToDeckSelection(workingGameTemplate));
        spaceSelectionButton.onClick.AddListener(() => goToSpaceSelection(workingGameTemplate));
    }

    private void unsetSectionButtons()
    {
        editGameTemplateDetailsButton.onClick.RemoveAllListeners();
        editTableSettingsButton.onClick.RemoveAllListeners();
        cardPoolButton.onClick.RemoveAllListeners();
        deckSelectionButton.onClick.RemoveAllListeners();
        spaceSelectionButton.onClick.RemoveAllListeners();
    }

    private void goToEditGameTemplateDetails(WorkingGameTemplate workingGameTemplate)
    {
        this.hide();
        GameTemplateEditorScreenReferences.Instance.EditGameTemplateDetailsScreen.Show(workingGameTemplate);
    }

    private void goToEditTableSettings(WorkingGameTemplate workingGameTemplate)
    {
        this.hide();
        GameTemplateEditorScreenReferences.Instance.EditTableSettingsScreen.Show(workingGameTemplate);
    }

    private void goToCardPool(WorkingGameTemplate workingGameTemplate)
    {
        this.hide();
        GameTemplateEditorScreenReferences.Instance.CardPoolScreen.Show(workingGameTemplate);
    }

    private void goToDeckSelection(WorkingGameTemplate workingGameTemplate)
    {
        this.hide();
        GameTemplateEditorScreenReferences.Instance.DeckSelectionScreen.Show(workingGameTemplate);
    }

    private void goToSpaceSelection(WorkingGameTemplate workingGameTemplate)
    {
        this.hide();
        GameTemplateEditorScreenReferences.Instance.SpaceSelectionScreen.Show(workingGameTemplate);
    }

    private void goToGameTemplateSelectionScreen()
    {
        this.hide();
        GameTemplateEditorScreenReferences.Instance.GameTemplateSelectionScreen.Show();
    }
}
