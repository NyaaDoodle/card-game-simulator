using Mirror;

public static class CardSerialization
{
    public static void WriteCard(this NetworkWriter writer, Card card)
    {
        writer.WriteCardData(card.CardData);
        writer.WriteBool(card.IsFaceUp);
        writer.WriteFloat(card.Rotation);
    }

    public static Card ReadCard(this NetworkReader reader)
    {
        CardData cardData = reader.ReadCardData();
        bool isFaceUp = reader.ReadBool();
        float rotation = reader.ReadFloat();
        return new Card(cardData, isFaceUp, rotation);
    }
}
