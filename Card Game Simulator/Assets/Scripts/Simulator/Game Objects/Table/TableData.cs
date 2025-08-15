using System;
using System.Text;
using Newtonsoft.Json;

public readonly struct TableData : IEquatable<TableData>
{
    public float Width { get; }
    public float Height { get; }
    public string SurfaceImagePath { get; }

    [JsonConstructor]
    public TableData(float width, float height, string surfaceImagePath)
    {
        Width = width;
        Height = height;
        SurfaceImagePath = surfaceImagePath;
    }

    public bool Equals(TableData other) =>
        other.Width == this.Width && other.Height == this.Height && other.SurfaceImagePath == this.SurfaceImagePath;

    public override bool Equals(object obj) => obj is TableData tableData && Equals(tableData);

    public override int GetHashCode() => HashCode.Combine(Width, Height, SurfaceImagePath);

    public static bool operator ==(TableData tableData1, TableData tableData2)
    {
        return tableData1.Equals(tableData2);
    }

    public static bool operator !=(TableData tableData1, TableData tableData2)
    {
        return !tableData1.Equals(tableData2);
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("TableData");
        stringBuilder.AppendLine($"Width {Width}");
        stringBuilder.AppendLine($"Height: {Height}");
        stringBuilder.AppendLine($"SurfaceImagePath: {SurfaceImagePath}");
        return stringBuilder.ToString();
    }
}
