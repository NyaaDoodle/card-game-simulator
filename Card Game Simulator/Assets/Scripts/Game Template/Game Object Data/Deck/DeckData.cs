using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

public readonly struct DeckData : IStackableData, IEquatable<DeckData>
{
    public string Id { get; }
    public string Name { get; }
    public float TableXCoordinate { get; }
    public float TableYCoordinate { get; }
    public float Rotation { get; }
    public string[] StartingCardIds { get; }

    [JsonConstructor]
    public DeckData(
        string id,
        string name,
        float tableXCoordinate,
        float tableYCoordinate,
        float rotation,
        string[] startingCardIds)
    {
        Id = id;
        Name = name;
        TableXCoordinate = tableXCoordinate;
        TableYCoordinate = tableYCoordinate;
        Rotation = rotation;
        StartingCardIds = startingCardIds;
    }

    public bool Equals(DeckData other)
    {
        return other.Id == this.Id && other.Name == this.Name && other.TableXCoordinate == this.TableXCoordinate
               && other.TableYCoordinate == this.TableYCoordinate && other.Rotation == this.Rotation
               && other.StartingCardIds != null && this.StartingCardIds != null
               && other.StartingCardIds.SequenceEqual(this.StartingCardIds);
    }

    public override bool Equals(object obj)
    {
        return obj is DeckData other && this.Equals(other);
    }

    public override int GetHashCode()
    {
        HashCode hashCode = new HashCode();
        hashCode.Add(Id);
        hashCode.Add(Name);
        hashCode.Add(TableXCoordinate);
        hashCode.Add(TableYCoordinate);
        hashCode.Add(Rotation);
        
        if (StartingCardIds != null)
        {
            foreach (string cardId in StartingCardIds)
            {
                hashCode.Add(cardId);
            }
        }
        
        return hashCode.ToHashCode();
    }

    public static bool operator ==(DeckData deckData1, DeckData deckData2)
    {
        return deckData1.Equals(deckData2);
    }

    public static bool operator !=(DeckData deckData1, DeckData deckData2)
    {
        return !deckData1.Equals(deckData2);
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"Id: {Id}");
        stringBuilder.AppendLine($"Name: {Name}");
        stringBuilder.AppendLine($"TableXCoordinate: {TableXCoordinate}");
        stringBuilder.AppendLine($"TableYCoordinate: {TableYCoordinate}");
        stringBuilder.AppendLine($"Rotation: {Rotation}");
        stringBuilder.AppendLine($"StartingCardIds:");
        if (StartingCardIds != null)
        {
            foreach (string cardId in StartingCardIds)
            {
                stringBuilder.AppendLine(cardId.ToString());
            }
        }
        else
        {
            stringBuilder.AppendLine("null");
        }
        return stringBuilder.ToString();
    }
}
