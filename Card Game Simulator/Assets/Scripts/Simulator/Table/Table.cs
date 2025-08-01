public class Table
{ 
    public TableData TableData { get; private set; }

    public Table(TableData tableData)
    {
        TableData = tableData;
    }
}
