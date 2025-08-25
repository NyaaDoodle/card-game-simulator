using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionModalWindowBase : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private TMP_Text titleText;
    
    protected virtual void SetupBackButton(Action onBackButtonSelect)
    {
        backButton.onClick.AddListener(() => onBackButtonSelect?.Invoke());
    }

    protected virtual void SetupTitleText(string text)
    {
        titleText.text = text;
    }
    
    protected virtual void OnDestroy()
    {
        unsetBackButton();
    }

    protected virtual void unsetBackButton()
    {
        backButton.onClick.RemoveAllListeners();
    }
}
