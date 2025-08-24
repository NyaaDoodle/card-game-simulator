using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTemplateEditor : MonoBehaviour
{
    [SerializeField] private GameTemplateSelectionScreen gameTemplateSelectionScreen;
    [SerializeField] private GameTemplateSectionsScreen gameTemplateSectionsScreen;
    
    private void Start()
    {
        showGameTemplateSelectionScreen();
    }

    private void showGameTemplateSelectionScreen()
    {
        gameTemplateSelectionScreen.Show(
            onGameTemplateSelectionSelect,
            onNewGameTemplateSelect,
            returnFromGameTemplateSelectionScreen);
    }

    private void showGameTemplateSectionsScreen(WorkingGameTemplate workingGameTemplate)
    {
        gameTemplateSectionsScreen.Show(workingGameTemplate, returnFromGameTemplateSectionsScreen);
    }

    private void onGameTemplateSelectionSelect(GameTemplate gameTemplate)
    {
        gameTemplateSelectionScreen.Hide();
        WorkingGameTemplate workingGameTemplate = new WorkingGameTemplate(gameTemplate);
        showGameTemplateSectionsScreen(workingGameTemplate);
    }

    private void onNewGameTemplateSelect()
    {
        gameTemplateSelectionScreen.Hide();
        WorkingGameTemplate workingGameTemplate = new WorkingGameTemplate();
        showGameTemplateSectionsScreen(workingGameTemplate);
    }
    
    private void returnFromGameTemplateSelectionScreen()
    {
        gameTemplateSelectionScreen.Hide();
        goToMainMenu();
    }

    private void returnFromGameTemplateSectionsScreen()
    {
        gameTemplateSectionsScreen.Hide();
        showGameTemplateSelectionScreen();
    }

    private void goToMainMenu()
    {
        SceneManager.LoadScene("Main Menu Scene");
    }
}
