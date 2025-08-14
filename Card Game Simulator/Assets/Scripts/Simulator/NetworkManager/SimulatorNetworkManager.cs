using Mirror;

public class SimulatorNetworkManager : NetworkManager
{
    public override void OnServerReady(NetworkConnectionToClient conn)
    {
        LoggerReferences.Instance.SimulatorNetworkManager.LogMethod();
        base.OnServerReady(conn);
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        LoggerReferences.Instance.SimulatorNetworkManager.LogMethod();
        base.OnServerDisconnect(conn);
    }
}