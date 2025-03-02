using UnityEngine;

public class TableData
{
    public float Width { get; set; }
    public float Height { get; set; }
    public Sprite SurfaceImage { get; set; }

    public TableData()
    {
        const float defaultWidth = 0;
        const float defaultHeight = 0;
        Width = defaultWidth;
        Height = defaultHeight;
    }

    public TableData(float width, float height) : this()
    {
        Width = width;
        Height = height;
    }
}
