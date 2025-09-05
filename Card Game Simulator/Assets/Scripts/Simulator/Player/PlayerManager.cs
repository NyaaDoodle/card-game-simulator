using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerManager : NetworkBehaviour
{
    public static PlayerManager Instance { get; private set; }
    public Player LocalPlayer { get; private set; }
    public PlayerHandDisplay LocalPlayerHandDisplay { get; private set; }

    public readonly SyncList<GameObject> ConnectedPlayers = new SyncList<GameObject>();
    
    private readonly Dictionary<string, PlayerData> disconnectedPlayerData = new Dictionary<string, PlayerData>();

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

    public override void OnStartServer()
    {
        base.OnStartServer();
        ConnectedPlayers.OnAdd += onPlayerJoined;
        ConnectedPlayers.OnRemove += onPlayerLeft;
    }

    public override void OnStopServer()
    {
        ConnectedPlayers.OnAdd -= onPlayerJoined;
        ConnectedPlayers.OnRemove -= onPlayerLeft;
    }

    public override void OnStopClient()
    {
        despawnPlayerHandDisplay();
        LocalPlayer = null;
        base.OnStopClient();
    }

    [Server]
    public void AddPlayer(NetworkConnectionToClient clientConnection, string playerID, string playerName)
    {
        if (!disconnectedPlayerData.ContainsKey(playerID))
        {
            spawnPlayer(clientConnection, playerID, playerName);
        }
        else
        {
            spawnPreviousPlayer(clientConnection, disconnectedPlayerData[playerID], playerName);
        }
    }

    [Server]
    private void spawnPlayer(NetworkConnectionToClient clientConnection, string playerID, string playerName)
    {
        Player player = PrefabExtensions.InstantiatePlayer(clientConnection, playerID, playerName);
        ConnectedPlayers.Add(player.gameObject);
        TargetPostPlayerSpawn(clientConnection, player.gameObject);
    }

    [Server]
    private void spawnPreviousPlayer(NetworkConnectionToClient clientConnection, PlayerData playerData, string playerName)
    {
        Player player = PrefabExtensions.InstantiatePreviousPlayer(clientConnection, playerData, playerName);
        ConnectedPlayers.Add(player.gameObject);
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

    [Server]
    public void AddDisconnectingPlayerData(NetworkConnectionToClient conn)
    {
        Player player = conn.identity.GetComponent<Player>();
        PlayerHand playerHand = conn.identity.GetComponent<PlayerHand>();
        PlayerData disconnectingPlayerData = new PlayerData(player, playerHand);
        disconnectedPlayerData[disconnectingPlayerData.Id] = disconnectingPlayerData;
        ConnectedPlayers.Remove(player.gameObject);
    }

    private void onPlayerJoined(int index)
    {
        string joinedPlayerName = ConnectedPlayers[index].GetComponent<Player>().Name;
        rpcAnnouncePlayerJoin(joinedPlayerName);
    }

    [ClientRpc]
    private void rpcAnnouncePlayerJoin(string playerName)
    {
        PopupMessageManager.NewPopupMessage($"{playerName} has joined", 2f);
    }

    private void onPlayerLeft(int _, GameObject removedPlayerObject)
    {
        string removedPlayerName = removedPlayerObject.GetComponent<Player>().Name;
        rpcAnnouncePlayerLeave(removedPlayerName);
    }

    [ClientRpc]
    private void rpcAnnouncePlayerLeave(string playerName)
    {
        PopupMessageManager.NewPopupMessage($"{playerName} has left", 2f);
    }
}
