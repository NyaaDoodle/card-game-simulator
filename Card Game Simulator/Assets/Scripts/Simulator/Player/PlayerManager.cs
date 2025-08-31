using Mirror;
using UnityEngine;

public class PlayerManager : NetworkBehaviour
{
    public static PlayerManager Instance { get; private set; }
    public Player LocalPlayer { get; private set; }
    public PlayerHandDisplay LocalPlayerHandDisplay { get; private set; }

    private void Awake()
    {
        initializeInstance();
    }

    private void initializeInstance()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

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
