using System;
using UnityEngine;
using UnityEngine.UI;

public class GameTemplateEditorScreenBase : MonoBehaviour
{
    [SerializeField] protected Button BackButton;
    [SerializeField] protected Button SaveButton;

    protected WorkingGameTemplate WorkingGameTemplate => GameTemplateEditor.Instance.CurrentWorkingGameTemplate;

    protected virtual void SetupBaseButtons(Action onBackButtonSelect)
    {
        // Use this on every Show() method
        BackButton.onClick.AddListener(() => onBackButtonSelect());
        SaveButton.onClick.AddListener(SaveGameTemplate);
    }

    protected virtual void UnsetBaseButtons()
    {
        // Use this on every Hide() method
        BackButton.onClick.RemoveAllListeners();
        SaveButton.onClick.RemoveAllListeners();
    }

    protected virtual void SaveGameTemplate()
    {
        GameTemplateSaver.SaveGameTemplate(WorkingGameTemplate);
    }

    public virtual void Hide()
    {
        UnsetBaseButtons();
        gameObject.SetActive(false);
    }

    protected virtual void OnDestroy()
    {
        Hide();
    }
}
