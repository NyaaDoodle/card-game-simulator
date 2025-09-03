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
        editGameTemplateDetailsButton.onClick.AddListener(GoToEditGameTemplateDetails);
        editTableSettingsButton.onClick.AddListener(GoToEditTableSettings);
        cardPoolButton.onClick.AddListener(GoToCardPool);
        deckSelectionButton.onClick.AddListener(GoToDeckSelection);
        spaceSelectionButton.onClick.AddListener(GoToSpaceSelection);
    }

    private void unsetSectionButtons()
    {
        editGameTemplateDetailsButton.onClick.RemoveAllListeners();
        editTableSettingsButton.onClick.RemoveAllListeners();
        cardPoolButton.onClick.RemoveAllListeners();
        deckSelectionButton.onClick.RemoveAllListeners();
        spaceSelectionButton.onClick.RemoveAllListeners();
    }

    public void GoToEditGameTemplateDetails()
    {
        GameTemplateEditor.Instance.GoToEditGameTemplateDetailsScreen();
    }

    public void GoToEditTableSettings()
    {
        GameTemplateEditor.Instance.GoToEditTableSettingsScreen();
    }

    public void GoToCardPool()
    {
        ModalWindowManager.OpenCardSelectionModalWindow(
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
            ModalWindowManager.CloseCurrentWindow,
            () =>
                {
                    ModalWindowManager.CloseCurrentWindow();
                    GoToDeckSelection();
                }
            );
    }

    public void GoToDeckSelection()
    {
        ModalWindowManager.OpenDeckSelectionModalWindow(
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
            ModalWindowManager.CloseCurrentWindow,
            () =>
                {
                    ModalWindowManager.CloseCurrentWindow();
                    GoToSpaceSelection();
                });
    }

    public void GoToSpaceSelection()
    {
        ModalWindowManager.OpenSpaceSelectionModalWindow(
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
            ModalWindowManager.CloseCurrentWindow);
    }

    private void returnFromCardEditScreen()
    {
        GameTemplateEditor.Instance.GoToGameTemplateSectionsScreen();
        GoToCardPool();
    }

    private void returnFromDeckEditScreen()
    {
        GameTemplateEditor.Instance.GoToGameTemplateSectionsScreen();
        GoToDeckSelection();
    }

    private void returnFromSpaceEditScreen()
    {
        GameTemplateEditor.Instance.GoToGameTemplateSectionsScreen();
        GoToSpaceSelection();
    }
}
