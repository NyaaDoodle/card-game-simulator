using Mirror;

public class SimulatorNetworkManager : NetworkManager
{
    public override void OnStartServer()
    {
        base.OnStartServer();
        NetworkServer.RegisterHandler<PlayerCreationRequestMessage>(onPlayerCreationRequestMessage);
        NetworkServer.RegisterHandler<PlayerDeletionRequestMessage>(onPlayerDeletionRequestMessage);
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();
        NetworkClient.Send(new PlayerCreationRequestMessage());
    }

    public override void OnClientDisconnect()
    {
        NetworkClient.Send(new PlayerDeletionRequestMessage());
        base.OnClientDisconnect();
    }

    [Server]
    private void onPlayerCreationRequestMessage(NetworkConnectionToClient conn, PlayerCreationRequestMessage _)
    {
        ManagerReferences.Instance.GameInstanceManager.AddPlayer(conn);
    }

    [Server]
    private void onPlayerDeletionRequestMessage(NetworkConnectionToClient conn, PlayerDeletionRequestMessage _)
    {
        ManagerReferences.Instance.GameInstanceManager.RemovePlayer(conn);
    }
}