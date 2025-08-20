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
    public ImageAssetReference BackSideImageReference { get; }
    public ImageAssetReference FrontSideImageReference { get; }

    [JsonConstructor]
    public CardData(
        string id,
        string name,
        string description,
        float width,
        float height,
        ImageAssetReference backSideImageReference,
        ImageAssetReference frontSideImageReference)
    {
        Id = id;
        Name = name;
        Description = description;
        Width = width;
        Height = height;
        BackSideImageReference = backSideImageReference;
        FrontSideImageReference = frontSideImageReference;
    }

    public CardData(
        string id,
        string name,
        string description,
        ImageAssetReference backSideImageReference,
        ImageAssetReference frontSideImageReference)
    {
        Id = id;
        Name = name;
        Description = description;
        Width = defaultWidth;
        Height = defaultHeight;
        BackSideImageReference = backSideImageReference;
        FrontSideImageReference = frontSideImageReference;
    }

    public CardData(
        string name,
        string description,
        ImageAssetReference backSideImageReference,
        ImageAssetReference frontSideImageReference)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        Description = description;
        Width = defaultWidth;
        Height = defaultHeight;
        BackSideImageReference = backSideImageReference;
        FrontSideImageReference = frontSideImageReference;
    }

    public bool Equals(CardData other)
    {
        return other.Id == this.Id && other.Name == this.Name && other.Description == this.Description
               && other.Width == this.Width && other.Height == this.Height
               && other.BackSideImageReference == this.BackSideImageReference
               && other.FrontSideImageReference == this.FrontSideImageReference;
    } 

    public override bool Equals(object obj) => obj is CardData cardData && Equals(cardData);

    public override int GetHashCode() =>
        HashCode.Combine(Id, Name, Description, Width, Height, BackSideImageReference, FrontSideImageReference);

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
        stringBuilder.AppendLine($"BackSideImageReference: {BackSideImageReference}");
        stringBuilder.AppendLine($"FrontSideImageReference: {FrontSideImageReference}");
        return stringBuilder.ToString();
    }
}
