using System;
using UnityEngine;

public class TableState : MonoBehaviour
{
    private TableData tableData;
    public event Action<TableData> TableDataChanged;
    public TableData TableData => tableData;

    public void Initialize(TableData data)
    {
        tableData = data;
        OnTableDataChanged();
    }

    protected virtual void OnTableDataChanged()
    {
        TableDataChanged?.Invoke(tableData);
    }
}
