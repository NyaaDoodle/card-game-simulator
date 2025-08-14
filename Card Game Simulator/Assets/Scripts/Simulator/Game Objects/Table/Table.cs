using Mirror;

public class Table : NetworkBehaviour
{
    [SyncVar] private TableData tableData;

    public TableData TableData => tableData;

    [Server]
    public void Setup(TableData tableData)
    {
        LoggerReferences.Instance.TableLogger.LogMethod();
        this.tableData = tableData;
    }

    public override void OnStartClient()
    {
        LoggerReferences.Instance.TableLogger.LogMethod();
        attachToTableContainer();
    }

    private void attachToTableContainer()
    {
        LoggerReferences.Instance.TableLogger.LogMethod();
        gameObject.transform.SetParent(ContainerReferences.Instance.TableContainer, false);
    }
}
