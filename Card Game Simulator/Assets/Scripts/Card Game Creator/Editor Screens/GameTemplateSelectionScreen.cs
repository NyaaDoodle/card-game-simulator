using System;
using UnityEngine;
using UnityEngine.UI;

public class GameTemplateSelectionScreen : MonoBehaviour
{
    [SerializeField] private GameTemplateSelectionGrid gameTemplateSelectionGrid;
    [SerializeField] private Button backButton;

    public void Show(
        Action<GameTemplate> onGameTemplateSelectionSelect,
        Action onNewGameTemplateSelect,
        Action onBackButtonSelect)
    {
        gameObject.SetActive(true);
        backButton.onClick.AddListener(() => onBackButtonSelect());
        gameTemplateSelectionGrid.Show(onGameTemplateSelectionSelect, onNewGameTemplateSelect);
    }

    public void Hide()
    {
        backButton.onClick.RemoveAllListeners();
        gameTemplateSelectionGrid.Hide();
        gameObject.SetActive(false);
    }
}
