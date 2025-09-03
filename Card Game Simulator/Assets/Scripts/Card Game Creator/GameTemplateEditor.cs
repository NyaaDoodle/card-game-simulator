using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTemplateEditor : MonoBehaviour
{
    public static GameTemplateEditor Instance { get; private set; }
    public WorkingGameTemplate CurrentWorkingGameTemplate { get; private set; } = null;
    public bool IsReady { get; private set; }
    
    private void Awake()
    {
        initializeInstance();
        onReady();
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
        ModalWindowManager.CloseCurrentWindow();
        GameTemplateEditorScreenReferences.Instance.GameTemplateSectionsScreen.Hide();
        GameTemplateEditorScreenReferences.Instance.EditGameTemplateDetailsScreen.Hide();
        GameTemplateEditorScreenReferences.Instance.EditTableSettingsScreen.Hide();
        GameTemplateEditorScreenReferences.Instance.EditCardScreen.Hide();
        GameTemplateEditorScreenReferences.Instance.EditDeckScreen.Hide();
        GameTemplateEditorScreenReferences.Instance.EditSpaceScreen.Hide();
    }

    private void showGameTemplateSelectionWindow()
    {
        ModalWindowManager.OpenGameTemplateSelectionModalWindow(
            "Select Game Template to Edit:",
            (gameTemplate) =>
                {
                    setCurrentWorkingGameTemplate(gameTemplate);
                    ModalWindowManager.CloseCurrentWindow();
                    GameTemplateEditorScreenReferences.Instance.GameTemplateSectionsScreen.Show();
                },
            () =>
                {
                    createNewGameTemplate();
                    ModalWindowManager.CloseCurrentWindow();
                    GameTemplateEditorScreenReferences.Instance.EditGameTemplateDetailsScreen.Show();
                },
            () =>
                {
                    ModalWindowManager.CloseCurrentWindow();
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

    public void GoToCardPoolScreen()
    {
        hideAllScreens();
        GameTemplateEditorScreenReferences.Instance.GameTemplateSectionsScreen.Show();
        GameTemplateEditorScreenReferences.Instance.GameTemplateSectionsScreen.GoToCardPool();
    }

    public void GoToDeckSelection()
    {
        hideAllScreens();
        GameTemplateEditorScreenReferences.Instance.GameTemplateSectionsScreen.Show();
        GameTemplateEditorScreenReferences.Instance.GameTemplateSectionsScreen.GoToDeckSelection();
    }

    public void GoToSpaceSelection()
    {
        hideAllScreens();
        GameTemplateEditorScreenReferences.Instance.GameTemplateSectionsScreen.Show();
        GameTemplateEditorScreenReferences.Instance.GameTemplateSectionsScreen.GoToSpaceSelection();
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

    private void onReady()
    {
        IsReady = true;
    }
}
