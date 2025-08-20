using Mirror;

public static class TableDataSerialization
{
    public static void WriteTableData(this NetworkWriter writer, TableData tableData)
    {
        writer.WriteFloat(tableData.Width);
        writer.WriteFloat(tableData.Height);
        writer.WriteImageAssetReference(tableData.SurfaceImageReference);
    }

    public static TableData ReadTableData(this NetworkReader reader)
    {
        float width = reader.ReadFloat();
        float height = reader.ReadFloat();
        ImageAssetReference surfaceImagePath = reader.ReadImageAssetReference();
        return new TableData(width, height, surfaceImagePath);
    }
}