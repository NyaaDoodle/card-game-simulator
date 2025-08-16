using Mirror;
using UnityEngine;

public class PlayerManager : NetworkBehaviour
{
    public Player LocalPlayer { get; private set; }
    public PlayerHandDisplay LocalPlayerHandDisplay { get; private set; }

    public override void OnStopClient()
    {
        LoggerReferences.Instance.PlayerManagerLogger.LogMethod();
        despawnPlayerHandDisplay();
        LocalPlayer = null;
        base.OnStopClient();
    }

    [Server]
    public void AddPlayer(NetworkConnectionToClient clientConnection)
    {
        LoggerReferences.Instance.PlayerManagerLogger.LogMethod();
        spawnPlayer(clientConnection);
    }

    [Server]
    private void spawnPlayer(NetworkConnectionToClient clientConnection)
    {
        LoggerReferences.Instance.PlayerManagerLogger.LogMethod();
        Player player = PrefabExtensions.InstantiatePlayer(clientConnection);
        TargetPostPlayerSpawn(clientConnection, player.gameObject);
    }

    [TargetRpc]
    private void TargetPostPlayerSpawn(NetworkConnectionToClient targetConnection, GameObject playerGameObject)
    {
        LoggerReferences.Instance.PlayerManagerLogger.LogMethod();
        LocalPlayer = playerGameObject.GetComponent<Player>();
        spawnPlayerHandDisplay();
        setupSelectionManager();
    }

    [Client]
    private void spawnPlayerHandDisplay()
    {
        LoggerReferences.Instance.PlayerManagerLogger.LogMethod();
        if (LocalPlayer != null)
        {
            PlayerHand playerHand = LocalPlayer.GetComponent<PlayerHand>();
            LocalPlayerHandDisplay = PrefabExtensions.InstantiatePlayerHandDisplay(playerHand);
        }
    }
    
    private void despawnPlayerHandDisplay()
    {
        LoggerReferences.Instance.PlayerManagerLogger.LogMethod();
        if (LocalPlayerHandDisplay != null)
        {
            Destroy(LocalPlayerHandDisplay.gameObject);
        }
    }
    
    private void setupSelectionManager()
    {
        LoggerReferences.Instance.PlayerManagerLogger.LogMethod();
        ManagerReferences.Instance.SelectionManager.SetupPlayerHandEvents();
    }
}
