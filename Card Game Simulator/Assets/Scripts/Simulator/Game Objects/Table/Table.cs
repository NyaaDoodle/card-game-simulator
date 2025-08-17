using Mirror;

public class Table : NetworkBehaviour
{
    [SyncVar] private TableData tableData;

    public TableData TableData => tableData;

    [Server]
    public void Setup(TableData tableData)
    {
        LoggingManager.Instance.TableLogger.LogMethod();
        this.tableData = tableData;
    }
}
