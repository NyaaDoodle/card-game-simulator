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
    
    public void Show()
    {
        gameObject.SetActive(true);
        SetupBaseButtons(GameTemplateEditor.Instance.GoToInitialScreen);
        setupSectionButtons();
    }

    public override void Hide()
    {
        unsetSectionButtons();
        base.Hide();
    }

    private void setupSectionButtons()
    {
        editGameTemplateDetailsButton.onClick.AddListener(goToEditGameTemplateDetails);
        editTableSettingsButton.onClick.AddListener(goToEditTableSettings);
        cardPoolButton.onClick.AddListener(goToCardPool);
        deckSelectionButton.onClick.AddListener(goToDeckSelection);
        spaceSelectionButton.onClick.AddListener(goToSpaceSelection);
    }

    private void unsetSectionButtons()
    {
        editGameTemplateDetailsButton.onClick.RemoveAllListeners();
        editTableSettingsButton.onClick.RemoveAllListeners();
        cardPoolButton.onClick.RemoveAllListeners();
        deckSelectionButton.onClick.RemoveAllListeners();
        spaceSelectionButton.onClick.RemoveAllListeners();
    }

    private void goToEditGameTemplateDetails()
    {
        GameTemplateEditor.Instance.GoToEditGameTemplateDetailsScreen();
    }

    private void goToEditTableSettings()
    {
        GameTemplateEditor.Instance.GoToEditTableSettingsScreen();
    }

    private void goToCardPool()
    {
        SelectionModalWindowManager.OpenCardSelectionModalWindow(
            "Select Card to Edit",
            WorkingGameTemplate.CardPool.Values,
            (cardData) =>
                {
                    GameTemplateEditor.Instance.GoToEditCardScreen(cardData, returnFromCardEditScreen);
                },
            () =>
                {
                    CardData newCard = WorkingGameTemplate.CreateNewDefaultCardData();
                    GameTemplateEditor.Instance.GoToEditCardScreen(newCard, returnFromCardEditScreen);
                },
            SelectionModalWindowManager.CloseCurrentWindow
            );
    }

    private void goToDeckSelection()
    {
        SelectionModalWindowManager.OpenDeckSelectionModalWindow(
            "Select Deck to Edit",
            WorkingGameTemplate.DecksData.Values,
            (deckData) =>
                {
                    GameTemplateEditor.Instance.GoToEditDeckScreen(deckData, returnFromDeckEditScreen);
                },
            () =>
                {
                    DeckData newDeck = WorkingGameTemplate.CreateNewDefaultDeckData();
                    GameTemplateEditor.Instance.GoToEditDeckScreen(newDeck, returnFromDeckEditScreen);
                },
            SelectionModalWindowManager.CloseCurrentWindow);
    }

    private void goToSpaceSelection()
    {
        SelectionModalWindowManager.OpenSpaceSelectionModalWindow(
            "Select Space to Edit",
            WorkingGameTemplate.SpacesData.Values,
            (spaceData) =>
                {
                    GameTemplateEditor.Instance.GoToEditSpaceScreen(spaceData, returnFromSpaceEditScreen);
                },
            () =>
                {
                    SpaceData newSpace = WorkingGameTemplate.CreateNewDefaultSpaceData();
                    GameTemplateEditor.Instance.GoToEditSpaceScreen(newSpace, returnFromSpaceEditScreen);
                },
            SelectionModalWindowManager.CloseCurrentWindow);
    }

    private void returnFromCardEditScreen()
    {
        GameTemplateEditor.Instance.GoToGameTemplateSectionsScreen();
        goToCardPool();
    }

    private void returnFromDeckEditScreen()
    {
        GameTemplateEditor.Instance.GoToGameTemplateSectionsScreen();
        goToDeckSelection();
    }

    private void returnFromSpaceEditScreen()
    {
        GameTemplateEditor.Instance.GoToGameTemplateSectionsScreen();
        goToSpaceSelection();
    }
}
