using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTemplateSelectionScreen : MonoBehaviour
{
    [SerializeField] private GameTemplateSelectionGrid gameTemplateSelectionGrid;
    [SerializeField] private Button backButton;

    public void Show()
    {
        gameObject.SetActive(true);
        backButton.onClick.AddListener(goToMainMenu);
        gameTemplateSelectionGrid.Show(onGameTemplateSelectionSelect, onNewGameTemplateSelect);
    }

    private void hide()
    {
        backButton.onClick.RemoveAllListeners();
        gameTemplateSelectionGrid.Hide();
        gameObject.SetActive(false);
    }
    
    private void goToMainMenu()
    {
        this.hide();
        SceneManager.LoadScene("Main Menu Scene");
    }

    private void onGameTemplateSelectionSelect(GameTemplate gameTemplate)
    {
        this.hide();
        WorkingGameTemplate workingGameTemplate = new WorkingGameTemplate(gameTemplate);
        GameTemplateEditorScreenReferences.Instance.GameTemplateSectionsScreen.Show(workingGameTemplate);
    }

    private void onNewGameTemplateSelect()
    {
        this.hide();
        WorkingGameTemplate workingGameTemplate = new WorkingGameTemplate();
        GameTemplateEditorScreenReferences.Instance.GameTemplateSectionsScreen.Show(workingGameTemplate);
    }
}
