using System;
using System.Text;
using Newtonsoft.Json;

public readonly struct CardData : IEquatable<CardData>
{
    public int Id { get; }
    public string Name { get; }
    public string Description { get; }
    public float Width { get; }
    public float Height { get; }
    public string BackSideSpritePath { get; }
    public string FrontSideSpritePath { get; }

    [JsonConstructor]
    public CardData(
        int id,
        string name,
        string description,
        float width,
        float height,
        string backSideSpritePath,
        string frontSideSpritePath)
    {
        Id = id;
        Name = name;
        Description = description;
        Width = width;
        Height = height;
        BackSideSpritePath = backSideSpritePath;
        FrontSideSpritePath = frontSideSpritePath;
    }

    public CardData(int id, string backSideSpritePath, string frontSideSpritePath)
    {
        Id = id;
        Name = "";
        Description = "";
        Width = 2;
        Height = 3;
        BackSideSpritePath = backSideSpritePath;
        FrontSideSpritePath = frontSideSpritePath;
    }

    public bool Equals(CardData other)
    {
        return other.Id == this.Id && other.Name == this.Name && other.Description == this.Description
               && other.Width == this.Width && other.Height == this.Height
               && other.BackSideSpritePath == this.BackSideSpritePath
               && other.FrontSideSpritePath == this.FrontSideSpritePath;
    } 

    public override bool Equals(object obj) => obj is CardData cardData && Equals(cardData);

    public override int GetHashCode() =>
        HashCode.Combine(Id, Name, Description, Width, Height, BackSideSpritePath, FrontSideSpritePath);

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
        stringBuilder.AppendLine($"Id: {Id}");
        stringBuilder.AppendLine($"Name: {Name}");
        stringBuilder.AppendLine($"Description: {Description}");
        stringBuilder.AppendLine($"Width: {Width}");
        stringBuilder.AppendLine($"Height: {Height}");
        stringBuilder.AppendLine($"BackSideSpritePath: {BackSideSpritePath}");
        stringBuilder.AppendLine($"FrontSideSpritePath: {FrontSideSpritePath}");
        return stringBuilder.ToString();
    }
}
