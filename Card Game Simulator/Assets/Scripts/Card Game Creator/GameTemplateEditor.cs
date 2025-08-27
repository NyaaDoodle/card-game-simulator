using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTemplateEditor : MonoBehaviour
{
    public static GameTemplateEditor Instance { get; private set; }
    public WorkingGameTemplate CurrentWorkingGameTemplate { get; private set; } = null;
    
    private void Awake()
    {
        initializeInstance();
    }

    private void initializeInstance()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void GoToInitialScreen()
    {
        discardCurrentWorkingGameTemplate();
        hideAllScreens();
        showGameTemplateSelectionWindow();
    }

    private void hideAllScreens()
    {
        SelectionModalWindowManager.CloseCurrentWindow();
        GameTemplateEditorScreenReferences.Instance.GameTemplateSectionsScreen.Hide();
        GameTemplateEditorScreenReferences.Instance.EditGameTemplateDetailsScreen.Hide();
        GameTemplateEditorScreenReferences.Instance.EditTableSettingsScreen.Hide();
        GameTemplateEditorScreenReferences.Instance.EditCardScreen.Hide();
        GameTemplateEditorScreenReferences.Instance.EditDeckScreen.Hide();
        GameTemplateEditorScreenReferences.Instance.EditSpaceScreen.Hide();
    }

    private void showGameTemplateSelectionWindow()
    {
        SelectionModalWindowManager.OpenGameTemplateSelectionModalWindow(
            "Select Game Template to Edit:",
            (gameTemplate) =>
                {
                    setCurrentWorkingGameTemplate(gameTemplate);
                    SelectionModalWindowManager.CloseCurrentWindow();
                    GameTemplateEditorScreenReferences.Instance.GameTemplateSectionsScreen.Show();
                },
            () =>
                {
                    createNewGameTemplate();
                    SelectionModalWindowManager.CloseCurrentWindow();
                    GameTemplateEditorScreenReferences.Instance.GameTemplateSectionsScreen.Show();
                },
            () =>
                {
                    SelectionModalWindowManager.CloseCurrentWindow();
                    SceneManager.LoadScene("Main Menu Scene");
                });
    }

    public void GoToGameTemplateSectionsScreen()
    {
        hideAllScreens();
        GameTemplateEditorScreenReferences.Instance.GameTemplateSectionsScreen.Show();
    }

    public void GoToEditGameTemplateDetailsScreen()
    {
        hideAllScreens();
        GameTemplateEditorScreenReferences.Instance.EditGameTemplateDetailsScreen.Show();
    }

    public void GoToEditTableSettingsScreen()
    {
        hideAllScreens();
        GameTemplateEditorScreenReferences.Instance.EditTableSettingsScreen.Show();
    }

    public void GoToEditCardScreen(CardData cardData, Action onBackButtonSelect)
    {
        hideAllScreens();
        GameTemplateEditorScreenReferences.Instance.EditCardScreen.Show(cardData, onBackButtonSelect);
    }

    public void GoToEditDeckScreen(DeckData deckData, Action onBackButtonSelect)
    {
        hideAllScreens();
        GameTemplateEditorScreenReferences.Instance.EditDeckScreen.Show(deckData, onBackButtonSelect);
    }

    public void GoToEditSpaceScreen(SpaceData spaceData, Action onBackButtonSelect)
    {
        hideAllScreens();
        GameTemplateEditorScreenReferences.Instance.EditSpaceScreen.Show(spaceData, onBackButtonSelect);
    }

    private void createNewGameTemplate()
    {
        CurrentWorkingGameTemplate = new WorkingGameTemplate();
        Debug.Log($"Created new game template {CurrentWorkingGameTemplate.Id}");
    }

    private void setCurrentWorkingGameTemplate(GameTemplate gameTemplate)
    {
        CurrentWorkingGameTemplate = new WorkingGameTemplate(gameTemplate);
        Debug.Log($"Selected game template {CurrentWorkingGameTemplate.Id}");
    }

    private void discardCurrentWorkingGameTemplate()
    {
        CurrentWorkingGameTemplate = null;
    }
}
