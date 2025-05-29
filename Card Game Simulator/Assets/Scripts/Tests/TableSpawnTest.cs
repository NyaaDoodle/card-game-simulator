using UnityEngine;

public class TableSpawnTest : MonoBehaviour
{
    [Header("Table Testing")]
    [SerializeField] private GameObject tablePrefab;
    [SerializeField] private Transform tableContainer;

    [Header("Test Table Settings")]
    [SerializeField] private float testTableWidth = 8f;
    [SerializeField] private float testTableHeight = 6f;
    [SerializeField] private string testSurfaceImagePath = "";

    private GameObject currentTableInstance;
    private TableBehaviour currentTableBehaviour;

    void Start()
    {
        SpawnTestTable();
    }

    [ContextMenu("Spawn Test Table")]
    public void SpawnTestTable()
    {
        // Clear existing table first
        ClearTable();

        // Create test table data
        TableData testTableData = CreateTestTableData();

        // Spawn the table
        SpawnTable(testTableData);

        // Log for debugging
        Debug.Log($"Spawned test table: {testTableWidth} x {testTableHeight} units");

        // Show table info
        if (currentTableBehaviour != null)
        {
            Vector2 tableSize = currentTableBehaviour.GetTableSize();
            Debug.Log($"Table UI size: {tableSize.x} x {tableSize.y} pixels");
        }
    }

    [ContextMenu("Clear Table")]
    public void ClearTable()
    {
        if (currentTableInstance != null)
        {
            Destroy(currentTableInstance);
            currentTableInstance = null;
            currentTableBehaviour = null;
            Debug.Log("Cleared table");
        }
    }

    private TableData CreateTestTableData()
    {
        return new TableData
                   {
                       Width = testTableWidth,
                       Height = testTableHeight,
                       SurfaceImagePath = testSurfaceImagePath
                   };
    }

    private void SpawnTable(TableData tableData)
    {
        if (tablePrefab == null)
        {
            Debug.LogError("Table prefab is not assigned!");
            return;
        }

        if (tableContainer == null)
        {
            Debug.LogError("Table container (TableViewPanel) is not assigned!");
            return;
        }

        // Instantiate the table prefab
        currentTableInstance = Instantiate(tablePrefab, tableContainer);

        // Get the TableBehaviour component
        currentTableBehaviour = currentTableInstance.GetComponent<TableBehaviour>();

        if (currentTableBehaviour == null)
        {
            Debug.LogError("Table prefab doesn't have TableBehaviour component!");
            return;
        }

        // Initialize the table with data
        currentTableBehaviour.Initialize(tableData);
    }

    void OnGUI()
    {
        // Simple on-screen instructions
        GUI.Label(new Rect(10, 10, 300, 20), "Press T to spawn table, C to clear table");

        if (currentTableBehaviour != null)
        {
            Vector2 tableSize = currentTableBehaviour.GetTableSize();
            GUI.Label(new Rect(10, 30, 400, 20), $"Table Size: {tableSize.x} x {tableSize.y} pixels");
            GUI.Label(new Rect(10, 50, 400, 20), $"Table Data: {testTableWidth} x {testTableHeight} units");
        }
    }
}
