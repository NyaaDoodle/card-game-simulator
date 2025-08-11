using System.Collections.Generic;
using Mirror;

public class PlayerManager : NetworkBehaviour
{
    private readonly SyncDictionary<int, Player> players = new SyncDictionary<int, Player>();
    public IDictionary<int, Player> Players => players;
    public Player LocalPlayer { get; private set; }

    [Server]
    public void AddPlayer(NetworkConnectionToClient conn)
    {
        Player clientPlayer = PrefabReferences.Instance.PlayerPrefab.InstantiatePlayer(conn, "Player");
        Players.Add(clientPlayer.Id, clientPlayer);

        onPlayerAdded(conn, clientPlayer.Id);
    }

    [Server]
    public void RemovePlayer(NetworkConnectionToClient conn)
    {
        if (Players.TryGetValue(conn.connectionId, out Player player))
        {
            Destroy(player.gameObject);
            Players.Remove(conn.connectionId);
            onPlayerRemoved(conn);
        }
    }

    [TargetRpc]
    private void onPlayerAdded(NetworkConnectionToClient target, int playerId)
    {
        LocalPlayer = Players[playerId];
        ManagerReferences.Instance.SelectionManager.Setup(LocalPlayer);
    }

    [TargetRpc]
    private void onPlayerRemoved(NetworkConnectionToClient target)
    {
        LocalPlayer = null;
    }

    public override void OnStopServer()
    {
        foreach (Player player in Players.Values)
        {
            Destroy(player.gameObject);
        }
        base.OnStopServer();
    }
}
