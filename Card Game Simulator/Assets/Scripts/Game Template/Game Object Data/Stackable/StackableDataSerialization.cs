using Mirror;
using UnityEngine;

public static class StackableDataSerialization
{
    private const byte DECK_DATA = 1;
    private const byte SPACE_DATA = 2;
    public static void WriteStackableData(this NetworkWriter writer, IStackableData StackableData)
    {
        if (StackableData is DeckData deckData)
        {
            writer.WriteByte(DECK_DATA);
            writer.WriteString(deckData.Id);
            writer.WriteString(deckData.Name);
            writer.WriteFloat(deckData.TableXCoordinate);
            writer.WriteFloat(deckData.TableYCoordinate);
            writer.WriteFloat(deckData.Rotation);
            writer.WriteArray(deckData.StartingCardIds);
        }
        else if (StackableData is SpaceData spaceData)
        {
            writer.WriteByte(SPACE_DATA);
            writer.WriteString(spaceData.Id);
            writer.WriteString(spaceData.Name);
            writer.WriteFloat(spaceData.TableXCoordinate);
            writer.WriteFloat(spaceData.TableYCoordinate);
            writer.WriteFloat(spaceData.Rotation);
        }
    }

    public static IStackableData ReadStackableData(this NetworkReader reader)
    {
        byte type = reader.ReadByte();
        string id = reader.ReadString();
        string name = reader.ReadString();
        float x = reader.ReadFloat();
        float y = reader.ReadFloat();
        float rotation = reader.ReadFloat();
        switch (type)
        {
            case DECK_DATA:
                string[] startingCardIds = reader.ReadArray<string>();
                return new DeckData(id, name, x, y, rotation, startingCardIds);
            case SPACE_DATA:
                return new SpaceData(id, name, x, y, rotation);
            default:
                Debug.LogWarning("Invalid StackableData, unable to deserialize, returning null");
                return null;
        }
    }
}
