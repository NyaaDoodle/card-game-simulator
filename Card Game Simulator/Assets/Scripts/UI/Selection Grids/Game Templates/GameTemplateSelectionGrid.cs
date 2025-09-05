using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTemplateSelectionGrid : MonoBehaviour
{
    [SerializeField] private RectTransform contentContainer;
    [SerializeField] private Button newGameTemplateButton;
    [SerializeField] private Button downloadGameTemplatesButton;

    private readonly List<GameTemplateSelectionEntity> selectionEntities = new List<GameTemplateSelectionEntity>();
    private Action<GameTemplate> onSelectGameTemplateAction;

    public void Show(Action<GameTemplate> onSelectGameTemplate, Action onSelectAddButton = null, bool showDownloadGameTemplatesButton = true)
    {
        gameObject.SetActive(true);
        onSelectGameTemplateAction = onSelectGameTemplate;
        setupAddButton(onSelectAddButton);
        setupDownloadGameTemplatesButton(showDownloadGameTemplatesButton);
        spawnGameTemplateSelectionEntities();
    }

    public void Hide()
    {
        despawnGameTemplateSelectionEntities();
        onSelectGameTemplateAction = null;
        unsetAddButton();
        unsetDownloadGameTemplatesButton();
        gameObject.SetActive(false);
    }

    private void setupAddButton(Action onSelectAddButton)
    {
        bool showAddButton = onSelectAddButton != null;
        newGameTemplateButton.gameObject.SetActive(showAddButton);
        newGameTemplateButton.onClick.AddListener(() => onSelectAddButton?.Invoke());
    }

    private void setupDownloadGameTemplatesButton(bool showDownloadGameTemplatesButton)
    {
        downloadGameTemplatesButton.onClick.RemoveAllListeners();
        if (showDownloadGameTemplatesButton)
        {
            downloadGameTemplatesButton.gameObject.SetActive(true);
            downloadGameTemplatesButton.onClick.AddListener(() =>
                {
                    DownloadSession cloudServerDownloadSession = new DownloadSession(
                        PlayerPrefsManager.Instance.CloudBackendIP,
                        PlayerPrefsManager.Instance.CloudBackendPort,
                        () =>
                            {
                                PopupMessageManager.NewPopupMessage("Successfully downloaded game templates!", 2f);
                                updateGameTemplates();
                            },
                        (error, gameTemplateId) =>
                            {
                                PopupMessageManager.NewPopupMessage(
                                    "Failed to download game templates, check logs for details",
                                    3f,
                                    Color.red);
                                Debug.LogError($"Failed to download game templates: {error}");
                                if (gameTemplateId != null)
                                {
                                    GameTemplateLoader.DeleteGameTemplate(gameTemplateId);
                                }
                            });
                    ContentDownloader.GetCompleteAvailableGameTemplates(cloudServerDownloadSession);
                });
        }
        else
        {
            downloadGameTemplatesButton.gameObject.SetActive(false);
        }
    }

    private void unsetAddButton()
    {
        newGameTemplateButton.onClick.RemoveAllListeners();
    }

    private void unsetDownloadGameTemplatesButton()
    {
        downloadGameTemplatesButton.onClick.RemoveAllListeners();
    }

    private void updateGameTemplates()
    {
        despawnGameTemplateSelectionEntities();
        spawnGameTemplateSelectionEntities();
    }

    private void spawnGameTemplateSelectionEntities()
    {
        List<GameTemplate> gameTemplates = GameTemplateLoader.LoadGameTemplates();
        foreach (GameTemplate gameTemplate in gameTemplates)
        {
            spawnGameTemplateSelectionEntity(gameTemplate, onSelectGameTemplateAction);    
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
