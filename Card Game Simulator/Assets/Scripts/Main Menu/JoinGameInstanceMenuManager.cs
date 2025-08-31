using System.Collections.Generic;
using Mirror.Discovery;
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
        SimulatorNetworkManager.singleton.DeactivateGameDiscovery();
        SimulatorNetworkManager.singleton.SimulatorNetworkDiscovery.OnServerFound.RemoveListener(onServerFound);
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
                    StopGameInstanceSearch();
                    SimulatorNetworkManager.singleton.JoinGame(info);
                });
        }
    }

    private void clearJoinGameInstanceButtons()
    {
        foreach (Button button in joinGameInstanceButtonInstances)
        {
            Destroy(button);
        }
    }
}
