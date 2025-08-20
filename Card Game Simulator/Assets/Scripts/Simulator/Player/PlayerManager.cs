using Mirror;
using UnityEngine;

public class PlayerManager : NetworkBehaviour
{
    public Player LocalPlayer { get; private set; }
    public PlayerHandDisplay LocalPlayerHandDisplay { get; private set; }

    public override void OnStopClient()
    {
        despawnPlayerHandDisplay();
        LocalPlayer = null;
        base.OnStopClient();
    }

    [Server]
    public void AddPlayer(NetworkConnectionToClient clientConnection)
    {
        spawnPlayer(clientConnection);
    }

    [Server]
    private void spawnPlayer(NetworkConnectionToClient clientConnection)
    {
        Player player = PrefabExtensions.InstantiatePlayer(clientConnection);
        TargetPostPlayerSpawn(clientConnection, player.gameObject);
    }

    [TargetRpc]
    private void TargetPostPlayerSpawn(NetworkConnectionToClient targetConnection, GameObject playerGameObject)
    {
        LocalPlayer = playerGameObject.GetComponent<Player>();
        spawnPlayerHandDisplay();
        setupSelectionManager();
    }

    [Client]
    private void spawnPlayerHandDisplay()
    {
        if (LocalPlayer != null)
        {
            PlayerHand playerHand = LocalPlayer.GetComponent<PlayerHand>();
            LocalPlayerHandDisplay = PrefabExtensions.InstantiatePlayerHandDisplay(playerHand);
        }
    }
    
    private void despawnPlayerHandDisplay()
    {
        if (LocalPlayerHandDisplay != null)
        {
            Destroy(LocalPlayerHandDisplay.gameObject);
        }
    }
    
    private void setupSelectionManager()
    {
        ManagerReferences.Instance.SelectionManager.SetupPlayerHandEvents();
    }
}
