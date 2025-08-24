using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTemplateEditor : MonoBehaviour
{
    private void Start()
    {
        hideAllScreens();
        showInitialScreen();
    }

    private void hideAllScreens()
    {
        GameTemplateEditorScreenReferences.Instance.GameTemplateSelectionScreen.gameObject.SetActive(false);
        GameTemplateEditorScreenReferences.Instance.GameTemplateSectionsScreen.gameObject.SetActive(false);
        GameTemplateEditorScreenReferences.Instance.EditGameTemplateDetailsScreen.gameObject.SetActive(false);
        GameTemplateEditorScreenReferences.Instance.EditTableSettingsScreen.gameObject.SetActive(false);
        GameTemplateEditorScreenReferences.Instance.CardPoolScreen.gameObject.SetActive(false);
        GameTemplateEditorScreenReferences.Instance.EditCardScreen.gameObject.SetActive(false);
        GameTemplateEditorScreenReferences.Instance.DeckSelectionScreen.gameObject.SetActive(false);
        GameTemplateEditorScreenReferences.Instance.EditDeckScreen.gameObject.SetActive(false);
        GameTemplateEditorScreenReferences.Instance.CardSelectionScreen.gameObject.SetActive(false);
        GameTemplateEditorScreenReferences.Instance.SpaceSelectionScreen.gameObject.SetActive(false);
        GameTemplateEditorScreenReferences.Instance.EditSpaceScreen.gameObject.SetActive(false);
    }

    private void showInitialScreen()
    {
        GameTemplateEditorScreenReferences.Instance.GameTemplateSelectionScreen.Show();
    }
}
