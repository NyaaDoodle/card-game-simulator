using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTemplateEditor : MonoBehaviour
{
    public static GameTemplateEditor Instance { get; private set; }
    public WorkingGameTemplate CurrentWorkingGameTemplate { get; private set; } = null;
    
    [SerializeField] private GameTemplateSectionsScreen gameTemplateSectionsScreen;
    [SerializeField] private EditGameTemplateDetailsScreen editGameTemplateDetailsScreen;
    [SerializeField] private EditTableSettingsScreen editTableSettingsScreen;
    [SerializeField] private EditCardScreen editCardScreen;
    [SerializeField] private EditDeckScreen editDeckScreen;
    [SerializeField] private EditSpaceScreen editSpaceScreen;
    
    private void Awake()
    {
        initializeInstance();
    }

    private void initializeInstance()
    {
        if (Instance != null && Instance == this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    private void Start()
    {
        GoToInitialScreen();
    }

    public void GoToInitialScreen()
    {
        discardCurrentWorkingGameTemplate();
        hideAllScreens();
        showGameTemplateSelectionWindow();
    }

    private void hideAllScreens()
    {
        gameTemplateSectionsScreen.Hide();
        editGameTemplateDetailsScreen.Hide();
        editTableSettingsScreen.Hide();
        editCardScreen.Hide();
        editDeckScreen.Hide();
        editSpaceScreen.Hide();
    }

    private void showGameTemplateSelectionWindow()
    {
        SelectionModalWindowManager.OpenGameTemplateSelectionModalWindow(
            "Select Game Template to Edit:",
            (gameTemplate) =>
                {
                    setCurrentWorkingGameTemplate(gameTemplate);
                    SelectionModalWindowManager.CloseCurrentWindow();
                    gameTemplateSectionsScreen.Show();
                },
            () =>
                {
                    createNewGameTemplate();
                    SelectionModalWindowManager.CloseCurrentWindow();
                    gameTemplateSectionsScreen.Show();
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
        gameTemplateSectionsScreen.Show();
    }

    public void GoToEditGameTemplateDetailsScreen()
    {
        hideAllScreens();
        editGameTemplateDetailsScreen.Show();
    }

    public void GoToEditTableSettingsScreen()
    {
        hideAllScreens();
        editTableSettingsScreen.Show();
    }

    public void GoToEditCardScreen(CardData cardData)
    {
        hideAllScreens();
        
    }

    public void GoToEditDeckScreen(DeckData deckData)
    {
        hideAllScreens();
        
    }

    public void GoToEditSpaceScreen(SpaceData spaceData)
    {
        hideAllScreens();
        
    }

    private void createNewGameTemplate()
    {
        WorkingGameTemplate workingGameTemplate = new WorkingGameTemplate();
    }

    private void setCurrentWorkingGameTemplate(GameTemplate gameTemplate)
    {
        WorkingGameTemplate workingGameTemplate = new WorkingGameTemplate(gameTemplate);
    }

    private void discardCurrentWorkingGameTemplate()
    {
        CurrentWorkingGameTemplate = null;
    }
}
