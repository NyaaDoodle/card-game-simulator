using Mirror;

public class SimulatorNetworkManager : NetworkManager
{
    public override void OnServerReady(NetworkConnectionToClient conn)
    {
        LoggerReferences.Instance.SimulatorNetworkManagerLogger.LogMethod();
        base.OnServerReady(conn);
        ManagerReferences.Instance.PlayerManager.AddPlayer(conn);
    }
}