using UnityEngine;
using UnityEngine.SceneManagement;

public class CardGameCreatorManager : MonoBehaviour
{
    [SerializeField] private GameTemplateSelectionGrid gameTemplateSelectionGrid;
    
    private void Start()
    {
        gameTemplateSelectionGrid.Show(onGameTemplateSelectionEntitySelect, onNewGameTemplateSelect);
    }

    private void OnDestroy()
    {
        gameTemplateSelectionGrid.Hide();
    }
    
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu Scene");
    }

    private void onGameTemplateSelectionEntitySelect(GameTemplate gameTemplate)
    {
        // PLACEHOLDER
        Debug.Log(gameTemplate.ToString());
    }

    private void onNewGameTemplateSelect()
    {
        // PLACEHOLDER
        Debug.Log("Create new game template!");
    }
}
