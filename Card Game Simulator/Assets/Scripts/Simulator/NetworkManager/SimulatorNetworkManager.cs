using Mirror;

public class SimulatorNetworkManager : NetworkManager
{
    public override void OnServerReady(NetworkConnectionToClient conn)
    {
        TraceLogger.LogMethod();
        base.OnServerReady(conn);
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        TraceLogger.LogMethod();
        base.OnServerDisconnect(conn);
    }
}