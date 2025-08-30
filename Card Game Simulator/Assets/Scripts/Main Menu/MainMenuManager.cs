using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private RectTransform startMenu;
    [SerializeField] private NewGameInstanceMenuManager newGameInstanceMenu;
    [SerializeField] private JoinGameInstanceMenuManager joinGameInstanceMenu;
    [SerializeField] private OptionsMenuManager optionsMenu;

    void Start()
    {
        GoToStartMenu();
    }

    public void GoToNewGameInstanceMenu()
    {
        closeAllMenus();
        openNewGameInstanceMenu();
    }

    public void GoToJoinGameInstanceMenu()
    {
        closeAllMenus();
        openJoinGameInstanceMenu();
    }

    public void GoToOptionsMenu()
    {
        closeAllMenus();
        openOptionsMenu();
    }

    public void GoToCardGameCreator()
    {
        closeAllMenus();
        SceneManager.LoadScene("Card Game Creator Scene");
    }

    public void GoToStartMenu()
    {
        closeAllMenus();
        openStartMenu();
    }

    public void ExitApplication()
    {
        // Credit to KillerFirefly at https://stackoverflow.com/questions/70437401/cannot-finish-the-game-in-unity-using-application-quit
        #if UNITY_STANDALONE
            Application.Quit(0);
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    private void closeOtherMenus()
    {
        closeNewGameInstanceMenu();
        closeJoinGameInstanceMenu();
        closeOptionsMenu();
    }

    private void closeAllMenus()
    {
        closeOtherMenus();
        closeStartMenu();
    }

    private void closeNewGameInstanceMenu()
    {
        newGameInstanceMenu.gameObject.SetActive(false);
    }

    private void closeJoinGameInstanceMenu()
    {
        joinGameInstanceMenu.gameObject.SetActive(false);
    }

    private void closeOptionsMenu()
    {
        optionsMenu.gameObject.SetActive(false);
    }

    private void openStartMenu()
    {
        startMenu.gameObject.SetActive(true);
    }

    private void openNewGameInstanceMenu()
    {
        newGameInstanceMenu.gameObject.SetActive(true);
    }

    private void openJoinGameInstanceMenu()
    {
        joinGameInstanceMenu.gameObject.SetActive(true);
    }

    private void openOptionsMenu()
    {
        optionsMenu.gameObject.SetActive(true);
    }

    private void closeStartMenu()
    {
        startMenu.gameObject.SetActive(false);
    }
}
