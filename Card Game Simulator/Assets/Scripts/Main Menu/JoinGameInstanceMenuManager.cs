using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinGameInstanceMenuManager : MonoBehaviour
{
    [SerializeField] private RectTransform buttonContentContainer;
    [SerializeField] private Button joinGameInstanceButtonPrefab;
    private List<long> serverIdList = new List<long>();
    private List<Button> joinGameInstanceButtonInstances = new List<Button>();
    
    private void OnEnable()
    {
        StartGameInstanceSearch();
    }

    private void OnDisable()
    {
        StopGameInstanceSearch();
    }

    public void StartGameInstanceSearch()
    {
        SimulatorNetworkManager.singleton.ActivateGameDiscovery();
        SimulatorNetworkManager.singleton.SimulatorNetworkDiscovery.OnServerFound.AddListener(onServerFound);
    }

    public void StopGameInstanceSearch()
    {
        if (SimulatorNetworkManager.singleton != null)
        {
            SimulatorNetworkManager.singleton.DeactivateGameDiscovery();
            SimulatorNetworkManager.singleton.SimulatorNetworkDiscovery.OnServerFound.RemoveListener(onServerFound);
        }
        serverIdList.Clear();
        clearJoinGameInstanceButtons();
    }

    private void onServerFound(DiscoveryResponse info)
    {
        Debug.Log($"Found server {info.serverId} at {info.uri}");
        spawnJoinGameInstanceButton(info);
    }

    private void spawnJoinGameInstanceButton(DiscoveryResponse info)
    {
        if (!serverIdList.Contains(info.serverId))
        {
            serverIdList.Add(info.serverId);
            Button joinGameInstanceButton = Instantiate(joinGameInstanceButtonPrefab, buttonContentContainer).GetComponent<Button>();
            joinGameInstanceButtonInstances.Add(joinGameInstanceButton);
            TMP_Text joinGameInstanceButtonText = joinGameInstanceButton.GetComponentInChildren<TMP_Text>();
            joinGameInstanceButtonText.text = info.gameTemplateName;
            joinGameInstanceButton.onClick.AddListener(() =>
                {
                    string templateId = info.gameTemplateId;
                    if (GameTemplateLoader.IsGameTemplateDataFileStored(templateId))
                    {
                        StopGameInstanceSearch();
                        SimulatorNetworkManager.singleton.JoinGame(info);
                    }
                    else
                    {
                        DownloadSession localContentServerDownloadSession = new DownloadSession(
                            info.EndPoint.Address.ToString(),
                            info.localContentServerPort,
                            () =>
                                {
                                    Debug.Log("Successfully downloaded game template from host's local content server");
                                    StopGameInstanceSearch();
                                    SimulatorNetworkManager.singleton.JoinGame(info);
                                },
                            (error, _) =>
                                {
                                    Debug.LogError($"Failed to download game template from local content server: {error}");
                                    GameTemplateLoader.DeleteGameTemplate(templateId);
                                });
                        ContentDownloader.GetGameTemplate(templateId, localContentServerDownloadSession);
                    }
                });
        }
    }

    private void clearJoinGameInstanceButtons()
    {
        foreach (Button button in joinGameInstanceButtonInstances)
        {
            Destroy(button.gameObject);
        }
        joinGameInstanceButtonInstances.Clear();
    }
}
