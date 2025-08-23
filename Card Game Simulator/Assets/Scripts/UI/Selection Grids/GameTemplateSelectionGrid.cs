using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTemplateSelectionGrid : MonoBehaviour
{
    [SerializeField] private RectTransform contentContainer;
    [SerializeField] private Button newGameTemplateButton;

    private readonly List<GameTemplateSelectionEntity> selectionEntities = new List<GameTemplateSelectionEntity>();

    public void Show(Action<GameTemplate> onSelectGameTemplate, Action onSelectAddButton)
    {
        gameObject.SetActive(true);
        setupAddButton(onSelectAddButton);
        spawnGameTemplateSelectionEntities(onSelectGameTemplate);
    }

    public void Hide()
    {
        despawnGameTemplateSelectionEntities();
        unsetAddButton();
        gameObject.SetActive(false);
    }

    private void setupAddButton(Action onSelectAddButton)
    {
        newGameTemplateButton.onClick.AddListener(() => onSelectAddButton());
    }

    private void unsetAddButton()
    {
        newGameTemplateButton.onClick.RemoveAllListeners();
    }

    private void spawnGameTemplateSelectionEntities(Action<GameTemplate> onSelectGameTemplate)
    {
        List<GameTemplate> gameTemplates = GameTemplateLoader.LoadGameTemplates();
        foreach (GameTemplate gameTemplate in gameTemplates)
        {
            spawnGameTemplateSelectionEntity(gameTemplate, onSelectGameTemplate);    
        }
        setAddButtonAsLastContentSibling();
    }

    private void despawnGameTemplateSelectionEntities()
    {
        foreach (GameTemplateSelectionEntity selectionEntity in selectionEntities)
        {
            Destroy(selectionEntity.gameObject);
        }
        selectionEntities.Clear();
    }

    private void spawnGameTemplateSelectionEntity(GameTemplate gameTemplate, Action<GameTemplate> onSelectGameTemplate)
    {
        GameTemplateSelectionEntity gameTemplateSelectionEntity =
            PrefabExtensions.InstantiateGameTemplateSelectionEntity(
                gameTemplate,
                onSelectGameTemplate,
                contentContainer);
        selectionEntities.Add(gameTemplateSelectionEntity);
    }

    private void setAddButtonAsLastContentSibling()
    {
        newGameTemplateButton.transform.SetAsLastSibling();
    }
}
