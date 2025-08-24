using System;
using UnityEngine;
using UnityEngine.UI;

public class GameTemplateSectionsScreen : GameTemplateEditorScreenBase
{
    [SerializeField] private EditGameTemplateDetailsScreen editGameTemplateDetailsScreen;
    [SerializeField] private Button editGameTemplateDetailsButton;

    [SerializeField] private EditTableSettingsScreen editTableSettingsScreen;
    [SerializeField] private Button editTableSettingsButton;

    [SerializeField] private CardPoolScreen cardPoolScreen;
    [SerializeField] private Button cardPoolButton;

    [SerializeField] private DeckSelectionScreen deckSelectionScreen;
    [SerializeField] private Button deckSelectionButton;

    [SerializeField] private SpaceSelectionScreen spaceSelectionScreen;
    [SerializeField] private Button spaceSelectionButton;
    
    public void Show(WorkingGameTemplate workingGameTemplate, Action onBackButtonSelect)
    {
        gameObject.SetActive(true);
        SetupBaseButtons(onBackButtonSelect);
        setupSectionButtons(workingGameTemplate);
    }

    public void Hide()
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
        gameObject.SetActive(false);
        editGameTemplateDetailsScreen.Show(workingGameTemplate,
            () =>
                {
                    editGameTemplateDetailsScreen.Hide();
                    gameObject.SetActive(true);
                });
    }

    private void goToEditTableSettings(WorkingGameTemplate workingGameTemplate)
    {
        gameObject.SetActive(false);
        editTableSettingsScreen.Show(workingGameTemplate,
            () =>
                {
                    editTableSettingsScreen.Hide();
                    gameObject.SetActive(true);
                });
    }

    private void goToCardPool(WorkingGameTemplate workingGameTemplate)
    {
        gameObject.SetActive(false);
        cardPoolScreen.Show(workingGameTemplate,
            () =>
                {
                    cardPoolScreen.Hide();
                    gameObject.SetActive(true);
                });
    }

    private void goToDeckSelection(WorkingGameTemplate workingGameTemplate)
    {
        gameObject.SetActive(false);
        deckSelectionScreen.Show(workingGameTemplate,
            () =>
                {
                    deckSelectionScreen.Hide();
                    gameObject.SetActive(true);
                });
    }

    private void goToSpaceSelection(WorkingGameTemplate workingGameTemplate)
    {
        gameObject.SetActive(false);
        spaceSelectionScreen.Show(workingGameTemplate,
            () =>
                {
                    spaceSelectionScreen.Hide();
                    gameObject.SetActive(true);
                });
    }
}
