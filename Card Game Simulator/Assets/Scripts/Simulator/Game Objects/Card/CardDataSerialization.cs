using Mirror;

public static class CardDataSerialization
{
    public static void WriteCardData(this NetworkWriter writer, CardData cardData)
    {
        writer.WriteString(cardData.Id);
        writer.WriteString(cardData.Name);
        writer.WriteString(cardData.Description);
        writer.WriteFloat(cardData.Width);
        writer.WriteFloat(cardData.Height);
        writer.WriteString(cardData.BackSideSpritePath);
        writer.WriteString(cardData.FrontSideSpritePath);
    }

    public static CardData ReadCardData(this NetworkReader reader)
    {
        string id = reader.ReadString();
        string name = reader.ReadString();
        string description = reader.ReadString();
        float width = reader.ReadFloat();
        float height = reader.ReadFloat();
        string backSideSpritePath = reader.ReadString();
        string frontSideSpritePath = reader.ReadString();
        return new CardData(id, name, description, width, height, backSideSpritePath, frontSideSpritePath);
    }
}
