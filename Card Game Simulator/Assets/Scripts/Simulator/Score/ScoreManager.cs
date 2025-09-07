using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    [SerializeField] private RectTransform scoreManagerScreen;
    [SerializeField] private RectTransform playerScoreEntityContainer;
    private readonly Dictionary<string, PlayerScoreEntity> playerScoreEntities =
        new Dictionary<string, PlayerScoreEntity>();

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

    private void OnDestroy()
    {
        Hide();
    }

    public void Show()
    {
        scoreManagerScreen.gameObject.SetActive(true);
        PlayerManager.Instance.ConnectedPlayers.OnAdd += onConnectedPlayersAdded;
        PlayerManager.Instance.ConnectedPlayers.OnRemove += onConnectedPlayersRemoved;
        spawnPlayerScoreEntities();
        if (CameraControlsManager.Instance.IsCameraControlsEnabled == true)
        {
            CameraControlsManager.Instance.IsCameraControlsEnabled = false;
        }
    }

    public void Hide()
    {
        PlayerManager.Instance.ConnectedPlayers.OnAdd -= onConnectedPlayersAdded;
        PlayerManager.Instance.ConnectedPlayers.OnRemove -= onConnectedPlayersRemoved;
        despawnPlayerScoreEntities();
        if (scoreManagerScreen != null)
        {
            scoreManagerScreen.gameObject.SetActive(false);
        }
        if (CameraControlsManager.Instance.IsCameraControlsEnabled == false)
        {
            CameraControlsManager.Instance.IsCameraControlsEnabled = true;
        }
    }

    private void spawnPlayerScoreEntities()
    {
        IList<GameObject> playerObjects = PlayerManager.Instance.ConnectedPlayers;
        foreach (GameObject playerObject in playerObjects)
        {
            Player player = playerObject.GetComponent<Player>();
            spawnPlayerScoreEntity(player);
        }
    }

    private void onConnectedPlayersAdded(int index)
    {
        spawnPlayerScoreEntity(PlayerManager.Instance.ConnectedPlayers[index].GetComponent<Player>());
    }

    private void onConnectedPlayersRemoved(int _, GameObject removedPlayer)
    {
        despawnPlayerScoreEntity(removedPlayer.GetComponent<Player>());
    }

    private void spawnPlayerScoreEntity(Player player)
    {
        if (player != null && !playerScoreEntities.ContainsKey(player.Id))
        {
            PlayerScoreEntity playerScoreEntity =
                PrefabExtensions.InstantiatePlayerScoreEntity(player, playerScoreEntityContainer);
            playerScoreEntities.Add(player.Id, playerScoreEntity);
        }
    }

    private void despawnPlayerScoreEntity(Player player)
    {
        if (player != null && playerScoreEntities.TryGetValue(player.Id, out PlayerScoreEntity playerScoreEntity))
        {
            Destroy(playerScoreEntity.gameObject);
            playerScoreEntities.Remove(player.Id);
        }
    }

    private void despawnPlayerScoreEntities()
    {
        foreach (PlayerScoreEntity playerScoreEntity in playerScoreEntities.Values)
        {
            if (playerScoreEntity != null && playerScoreEntity.gameObject != null)
            {
                Destroy(playerScoreEntity.gameObject);
            }
        }
        playerScoreEntities.Clear();
    }
}
