using Mirror;

public class Table : NetworkBehaviour
{
    [SyncVar] private TableData tableData;

    public TableData TableData => tableData;

    public void Setup(TableData tableData)
    {
        this.tableData = tableData;
    }

    public override void OnStartClient()
    {
        attachToTableContainer();
    }

    private void attachToTableContainer()
    {
        gameObject.transform.SetParent(ContainerReferences.Instance.TableContainer, false);
    }
}
