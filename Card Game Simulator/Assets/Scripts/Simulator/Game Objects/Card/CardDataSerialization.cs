using Mirror;

public static class CardDataSerialization
{
    public static void WriteCardData(this NetworkWriter writer, CardData cardData)
    {
        writer.WriteInt(cardData.Id);
        writer.WriteString(cardData.Name);
        writer.WriteString(cardData.Description);
        writer.WriteFloat(cardData.Width);
        writer.WriteFloat(cardData.Height);
        writer.WriteString(cardData.BackSideSpritePath);
        writer.WriteString(cardData.FrontSideSpritePath);
    }

    public static CardData ReadCardData(this NetworkReader reader)
    {
        int id = reader.ReadInt();
        string name = reader.ReadString();
        string description = reader.ReadString();
        float width = reader.ReadFloat();
        float height = reader.ReadFloat();
        string backSideSpritePath = reader.ReadString();
        string frontSideSpritePath = reader.ReadString();
        return new CardData(id, name, description, width, height, backSideSpritePath, frontSideSpritePath);
    }
}
