using System;

public readonly struct CardData : IEquatable<CardData>
{
    public int Id { get; }
    public string Name { get; }
    public string Description { get; }
    public float Width { get; }
    public float Height { get; }
    public string BackSideSpritePath { get; }
    public string FrontSideSpritePath { get; }

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
}
