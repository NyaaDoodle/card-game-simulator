using System;
using UnityEngine;
using UnityEngine.UI;

public class GameTemplateEditorScreenBase : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button saveButton;

    protected virtual void SetupBaseButtons(Action onBackButtonSelect)
    {
        // Use this on every Show() method
        backButton.onClick.AddListener(() => onBackButtonSelect());
        saveButton.onClick.AddListener(SaveGameTemplate);
    }

    protected virtual void UnsetBaseButtons()
    {
        // Use this on every Hide() method
        backButton.onClick.RemoveAllListeners();
        saveButton.onClick.RemoveAllListeners();
    }

    protected virtual void SaveGameTemplate()
    {
           
    }
}
