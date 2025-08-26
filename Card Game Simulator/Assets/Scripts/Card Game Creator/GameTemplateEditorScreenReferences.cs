using UnityEngine;

public class GameTemplateEditorScreenReferences : MonoBehaviour
{
    public static GameTemplateEditorScreenReferences Instance { get; private set; }
    
    public GameTemplateSectionsScreen GameTemplateSectionsScreen;
    public EditGameTemplateDetailsScreen EditGameTemplateDetailsScreen;
    public EditTableSettingsScreen EditTableSettingsScreen;
    public EditCardScreen EditCardScreen;
    public EditDeckScreen EditDeckScreen;
    public EditSpaceScreen EditSpaceScreen;

    private void Awake()
    {
        initializeInstance();
    }

    private void initializeInstance()
    {
        if (Instance != null && Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
