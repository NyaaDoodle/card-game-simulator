using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTemplateSelectionManager : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu Scene");
    }
}
