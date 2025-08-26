using UnityEngine;

public class GameTemplateEditorLauncher : MonoBehaviour
{
    private void Start()
    {
        GameTemplateEditor.Instance.GoToInitialScreen();
    }
}
