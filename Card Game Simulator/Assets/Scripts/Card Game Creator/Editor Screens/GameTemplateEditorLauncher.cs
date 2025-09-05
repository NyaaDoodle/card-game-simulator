using UnityEngine;

public class GameTemplateEditorLauncher : MonoBehaviour
{
    private void Start()
    {
        if (GameTemplateEditor.Instance.CurrentWorkingGameTemplate != null)
        {
            GameTemplateEditor.Instance.ContinueFromWorkingGameTemplate();
        }
        else
        {
            GameTemplateEditor.Instance.GoToInitialScreen();
        }
    }
}
