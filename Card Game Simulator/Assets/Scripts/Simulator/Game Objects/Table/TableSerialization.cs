using Mirror;

public static class TableSerialization
{
    public static void WriteTable(this NetworkWriter writer, Table table)
    {
        if (table == null)
        {
            writer.WriteBool(false);
        }
        else
        {
            writer.WriteBool(true);
            writer.WriteFloat(table.TableData.Width);
            writer.WriteFloat(table.TableData.Height);
            writer.WriteString(table.TableData.SurfaceImagePath ?? "");
        }
    }

    public static Table ReadTable(this NetworkReader reader)
    {
        bool hasTable = reader.ReadBool();
        if (!hasTable)
            return null;

        float width = reader.ReadFloat();
        float height = reader.ReadFloat();
        string imagePath = reader.ReadString();

        return new Table(new TableData
                             {
                                 Width = width,
                                 Height = height,
                                 SurfaceImagePath = imagePath
                             });
    }
}