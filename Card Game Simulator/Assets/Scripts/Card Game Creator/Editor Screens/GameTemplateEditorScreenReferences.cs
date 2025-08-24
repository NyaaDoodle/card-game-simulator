using UnityEngine;

public class GameTemplateEditorScreenReferences : MonoBehaviour
{
    public static GameTemplateEditorScreenReferences Instance { get; private set; }
    public GameTemplateSelectionScreen GameTemplateSelectionScreen;
    public GameTemplateSectionsScreen GameTemplateSectionsScreen;
    public EditGameTemplateDetailsScreen EditGameTemplateDetailsScreen;
    public EditTableSettingsScreen EditTableSettingsScreen;
    public CardPoolScreen CardPoolScreen;
    public EditCardScreen EditCardScreen;
    public DeckSelectionScreen DeckSelectionScreen;
    public EditDeckScreen EditDeckScreen;
    public CardSelectionScreen CardSelectionScreen;
    public SpaceSelectionScreen SpaceSelectionScreen;
    public EditSpaceScreen EditSpaceScreen;

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
        }
    }
}
