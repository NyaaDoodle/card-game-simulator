using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private RectTransform startMenu;
    [SerializeField] private CardGameCreatorMenuManager cardGameCreatorMenu;
    [SerializeField] private NewGameInstanceMenuManager newGameInstanceMenu;
    [SerializeField] private JoinGameInstanceMenuManager joinGameInstanceMenu;

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

    public void GoToCardGameCreator()
    {
        closeAllMenus();
        openCardGameCreatorMenu();
    }

    public void GoToOptionsMenu()
    {
        Debug.Log("Options menu not implemented");
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
        closeCardGameCreatorMenu();
        closeNewGameInstanceMenu();
        closeJoinGameInstanceMenu();
    }

    private void closeAllMenus()
    {
        closeOtherMenus();
        closeStartMenu();
    }

    private void closeCardGameCreatorMenu()
    {
        cardGameCreatorMenu.gameObject.SetActive(false);
    }

    private void closeNewGameInstanceMenu()
    {
        newGameInstanceMenu.gameObject.SetActive(false);
    }

    private void closeJoinGameInstanceMenu()
    {
        joinGameInstanceMenu.StopGameInstanceSearch();
        joinGameInstanceMenu.gameObject.SetActive(false);
    }

    private void openCardGameCreatorMenu()
    {
        cardGameCreatorMenu.gameObject.SetActive(true);
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

    private void closeStartMenu()
    {
        startMenu.gameObject.SetActive(false);
    }
}
