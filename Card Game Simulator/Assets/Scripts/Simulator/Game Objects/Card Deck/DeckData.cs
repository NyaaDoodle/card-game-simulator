using System;
using System.Linq;
using Newtonsoft.Json;

public readonly struct DeckData : IStackableData, IEquatable<DeckData>
{
    public int Id { get; }
    public string Name { get; }
    public float TableXCoordinate { get; }
    public float TableYCoordinate { get; }
    public float Rotation { get; }
    public CardData[] StartingCards { get; }

    [JsonConstructor]
    public DeckData(
        int id,
        string name,
        float tableXCoordinate,
        float tableYCoordinate,
        float rotation,
        CardData[] startingCards)
    {
        Id = id;
        Name = name;
        TableXCoordinate = tableXCoordinate;
        TableYCoordinate = tableYCoordinate;
        Rotation = rotation;
        StartingCards = startingCards;
    }

    public DeckData(
        int id,
        float tableXCoordinate,
        float tableYCoordinate,
        CardData[] startingCards)
    {
        Id = id;
        Name = "";
        TableXCoordinate = tableXCoordinate;
        TableYCoordinate = tableYCoordinate;
        Rotation = 0;
        StartingCards = startingCards;
    }

    public DeckData(
        int id,
        float tableXCoordinate,
        float tableYCoordinate)
    {
        Id = id;
        Name = "";
        TableXCoordinate = tableXCoordinate;
        TableYCoordinate = tableYCoordinate;
        Rotation = 0;
        StartingCards = Array.Empty<CardData>();
    }

    public bool Equals(DeckData other)
    {
        return other.Id == this.Id && other.Name == this.Name && other.TableXCoordinate == this.TableXCoordinate
               && other.TableYCoordinate == this.TableYCoordinate && other.Rotation == this.Rotation
               && other.StartingCards != null && this.StartingCards != null
               && other.StartingCards.SequenceEqual(this.StartingCards);
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
        
        if (StartingCards != null)
        {
            foreach (CardData cardData in StartingCards)
            {
                hashCode.Add(cardData);
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
}
