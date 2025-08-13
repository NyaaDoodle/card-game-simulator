using Mirror;

public static class TableDataSerialization
{
    public static void WriteTableData(this NetworkWriter writer, TableData tableData)
    {
        writer.WriteFloat(tableData.Width);
        writer.WriteFloat(tableData.Height);
        writer.WriteString(tableData.SurfaceImagePath);
    }

    public static TableData ReadTableData(this NetworkReader reader)
    {
        float width = reader.ReadFloat();
        float height = reader.ReadFloat();
        string surfaceImagePath = reader.ReadString();
        return new TableData(width, height, surfaceImagePath);
    }
}