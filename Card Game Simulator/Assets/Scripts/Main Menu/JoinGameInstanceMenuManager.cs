using Mirror.Discovery;
using UnityEngine;

public class JoinGameInstanceMenuManager : MonoBehaviour
{
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
        SimulatorNetworkManager.singleton.NetworkDiscovery.OnServerFound.AddListener(onServerFound);
    }

    public void StopGameInstanceSearch()
    {
        SimulatorNetworkManager.singleton.DeactivateGameDiscovery();
        SimulatorNetworkManager.singleton.NetworkDiscovery.OnServerFound.RemoveListener(onServerFound);
    }

    private void onServerFound(ServerResponse info)
    {
        SimulatorNetworkManager.singleton.JoinGame(info);
    }
}
