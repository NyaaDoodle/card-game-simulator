using System;
using UnityEngine;
using UnityEngine.UI;

public class GameTemplateEditorScreenBase : MonoBehaviour
{
    [SerializeField] protected Button BackButton;
    [SerializeField] protected Button SaveButton;

    protected virtual void SetupBaseButtons(WorkingGameTemplate workingGameTemplate, Action onBackButtonSelect)
    {
        // Use this on every Show() method
        BackButton.onClick.AddListener(() => onBackButtonSelect());
        SaveButton.onClick.AddListener(() => SaveGameTemplate(workingGameTemplate));
    }

    protected virtual void UnsetBaseButtons()
    {
        // Use this on every Hide() method
        BackButton.onClick.RemoveAllListeners();
        SaveButton.onClick.RemoveAllListeners();
    }

    protected virtual void SaveGameTemplate(WorkingGameTemplate workingGameTemplate)
    {
        Debug.Log("SaveGameTemplate");
        Debug.Log(workingGameTemplate.ConvertToGameTemplate().ToString());
        GameTemplateSaver.SaveGameTemplate(workingGameTemplate);
    }
}
