using Mirror;

public class SimulatorNetworkManager : NetworkManager
{
    public override void OnServerReady(NetworkConnectionToClient conn)
    {
        TraceLogger.LogMethod();
        base.OnServerReady(conn);
        ManagerReferences.Instance.PlayerManager.AddPlayer(conn);
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        TraceLogger.LogMethod();
        ManagerReferences.Instance.PlayerManager.RemovePlayer(conn);
        base.OnServerDisconnect(conn);
    }
}