using System;
using System.Text;
using Newtonsoft.Json;

public readonly struct CardData : IEquatable<CardData>
{
    private const float defaultWidth = 2f;
    private const float defaultHeight = 3f;
    
    public string Id { get; }
    public string Name { get; }
    public string Description { get; }
    public float Width { get; }
    public float Height { get; }
    public string BackSideImagePath { get; }
    public string FrontSideImagePath { get; }

    [JsonConstructor]
    public CardData(
        string id,
        string name,
        string description,
        float width,
        float height,
        string backSideImagePath,
        string frontSideImagePath)
    {
        Id = id;
        Name = name;
        Description = description;
        Width = width;
        Height = height;
        BackSideImagePath = backSideImagePath;
        FrontSideImagePath = frontSideImagePath;
    }

    public CardData(
        string id,
        string name,
        string description,
        string backSideImagePath,
        string frontSideImagePath)
    {
        Id = id;
        Name = name;
        Description = description;
        Width = defaultWidth;
        Height = defaultHeight;
        BackSideImagePath = backSideImagePath;
        FrontSideImagePath = frontSideImagePath;
    }

    public bool Equals(CardData other)
    {
        return other.Id == this.Id && other.Name == this.Name && other.Description == this.Description
               && other.Width == this.Width && other.Height == this.Height
               && other.BackSideImagePath == this.BackSideImagePath
               && other.FrontSideImagePath == this.FrontSideImagePath;
    } 

    public override bool Equals(object obj) => obj is CardData cardData && Equals(cardData);

    public override int GetHashCode() =>
        HashCode.Combine(Id, Name, Description, Width, Height, BackSideImagePath, FrontSideImagePath);

    public static bool operator ==(CardData cardData1, CardData cardData2)
    {
        return cardData1.Equals(cardData2);
    }

    public static bool operator !=(CardData cardData1, CardData cardData2)
    {
        return !cardData1.Equals(cardData2);
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("CardData");
        stringBuilder.AppendLine($"Id: {Id}");
        stringBuilder.AppendLine($"Name: {Name}");
        stringBuilder.AppendLine($"Description: {Description}");
        stringBuilder.AppendLine($"Width: {Width}");
        stringBuilder.AppendLine($"Height: {Height}");
        stringBuilder.AppendLine($"BackSideImagePath: {BackSideImagePath}");
        stringBuilder.AppendLine($"FrontSideImagePath: {FrontSideImagePath}");
        return stringBuilder.ToString();
    }
}
