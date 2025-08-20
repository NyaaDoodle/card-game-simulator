using System;
using System.Text;
using Newtonsoft.Json;

public readonly struct TableData : IEquatable<TableData>
{
    public float Width { get; }
    public float Height { get; }
    public ImageAssetReference SurfaceImageReference { get; }

    [JsonConstructor]
    public TableData(float width, float height, ImageAssetReference surfaceImageReference)
    {
        Width = width;
        Height = height;
        SurfaceImageReference = surfaceImageReference;
    }

    public bool Equals(TableData other) =>
        other.Width == this.Width && other.Height == this.Height && other.SurfaceImageReference == this.SurfaceImageReference;

    public override bool Equals(object obj) => obj is TableData tableData && Equals(tableData);

    public override int GetHashCode() => HashCode.Combine(Width, Height, SurfaceImageReference);

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
        stringBuilder.AppendLine($"SurfaceImageReference: {SurfaceImageReference}");
        return stringBuilder.ToString();
    }
}
