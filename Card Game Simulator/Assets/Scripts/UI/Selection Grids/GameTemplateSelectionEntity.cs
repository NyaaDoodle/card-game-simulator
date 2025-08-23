using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTemplateSelectionEntity : MonoBehaviour
{
    [SerializeField] private Button selectionButton;
    [SerializeField] private Image gameTemplateImage;
    [SerializeField] private TMP_Text gameTemplateTitle;
    [SerializeField] private Sprite fallbackSprite;

    public void Setup(GameTemplate gameTemplate, Action<GameTemplate> onSelectAction)
    {
        setupSelectionButton(gameTemplate, onSelectAction);
        setupGameTemplateImage(gameTemplate);
        setupGameTemplateTitle(gameTemplate);
    }

    private void OnDestroy()
    {
        removeSelectionButtonListeners();
    }

    private void setupSelectionButton(GameTemplate gameTemplate, Action<GameTemplate> onSelectAction)
    {
        selectionButton.onClick.AddListener(() => onSelectAction(gameTemplate));
    }

    private void removeSelectionButtonListeners()
    {
        selectionButton.onClick.RemoveAllListeners();
    }

    private void setupGameTemplateImage(GameTemplate gameTemplate)
    {
        string templateImagePath = gameTemplate.GameTemplateDetails.TemplateImagePath;
        SimulatorImageLoader.LoadImage(templateImagePath, gameTemplateImage, fallbackSprite);
    }

    private void setupGameTemplateTitle(GameTemplate gameTemplate)
    {
        gameTemplateTitle.text = gameTemplate.GameTemplateDetails.TemplateName;
    }
}
