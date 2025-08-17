using Mirror;
using UnityEngine;

public class PlayerManager : NetworkBehaviour
{
    public Player LocalPlayer { get; private set; }
    public PlayerHandDisplay LocalPlayerHandDisplay { get; private set; }

    public override void OnStopClient()
    {
        LoggingManager.Instance.PlayerManagerLogger.LogMethod();
        despawnPlayerHandDisplay();
        LocalPlayer = null;
        base.OnStopClient();
    }

    [Server]
    public void AddPlayer(NetworkConnectionToClient clientConnection)
    {
        LoggingManager.Instance.PlayerManagerLogger.LogMethod();
        spawnPlayer(clientConnection);
    }

    [Server]
    private void spawnPlayer(NetworkConnectionToClient clientConnection)
    {
        LoggingManager.Instance.PlayerManagerLogger.LogMethod();
        Player player = PrefabExtensions.InstantiatePlayer(clientConnection);
        TargetPostPlayerSpawn(clientConnection, player.gameObject);
    }

    [TargetRpc]
    private void TargetPostPlayerSpawn(NetworkConnectionToClient targetConnection, GameObject playerGameObject)
    {
        LoggingManager.Instance.PlayerManagerLogger.LogMethod();
        LocalPlayer = playerGameObject.GetComponent<Player>();
        spawnPlayerHandDisplay();
        setupSelectionManager();
    }

    [Client]
    private void spawnPlayerHandDisplay()
    {
        LoggingManager.Instance.PlayerManagerLogger.LogMethod();
        if (LocalPlayer != null)
        {
            PlayerHand playerHand = LocalPlayer.GetComponent<PlayerHand>();
            LocalPlayerHandDisplay = PrefabExtensions.InstantiatePlayerHandDisplay(playerHand);
        }
    }
    
    private void despawnPlayerHandDisplay()
    {
        LoggingManager.Instance.PlayerManagerLogger.LogMethod();
        if (LocalPlayerHandDisplay != null)
        {
            Destroy(LocalPlayerHandDisplay.gameObject);
        }
    }
    
    private void setupSelectionManager()
    {
        LoggingManager.Instance.PlayerManagerLogger.LogMethod();
        ManagerReferences.Instance.SelectionManager.SetupPlayerHandEvents();
    }
}
