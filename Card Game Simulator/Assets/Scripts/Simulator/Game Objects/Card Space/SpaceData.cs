using System;
using System.Text;
using Newtonsoft.Json;

public readonly struct SpaceData : IStackableData, IEquatable<SpaceData>
{
    public string Id { get; }
    public string Name { get; }
    public float TableXCoordinate { get; }
    public float TableYCoordinate { get; }
    public float Rotation { get; }

    [JsonConstructor]
    public SpaceData(
        string id,
        string name,
        float tableXCoordinate,
        float tableYCoordinate,
        float rotation)
    {
        Id = id;
        Name = name;
        TableXCoordinate = tableXCoordinate;
        TableYCoordinate = tableYCoordinate;
        Rotation = rotation;
    }

    public SpaceData(
        string name,
        float tableXCoordinate,
        float tableYCoordinate,
        float rotation)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        TableXCoordinate = tableXCoordinate;
        TableYCoordinate = tableYCoordinate;
        Rotation = rotation;
    }

    public bool Equals(SpaceData other)
    {
        return other.Id == this.Id && other.Name == this.Name && other.TableXCoordinate == this.TableXCoordinate
               && other.TableYCoordinate == this.TableYCoordinate && other.Rotation == this.Rotation;
    }

    public override bool Equals(object obj)
    {
        return obj is SpaceData other && this.Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, TableXCoordinate, TableYCoordinate, Rotation);
    }

    public static bool operator ==(SpaceData spaceData1, SpaceData spaceData2)
    {
        return spaceData1.Equals(spaceData2);
    }

    public static bool operator !=(SpaceData spaceData1, SpaceData spaceData2)
    {
        return !spaceData1.Equals(spaceData2);
    }
    
    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"Id: {Id}");
        stringBuilder.AppendLine($"Name: {Name}");
        stringBuilder.AppendLine($"TableXCoordinate: {TableXCoordinate}");
        stringBuilder.AppendLine($"TableYCoordinate: {TableYCoordinate}");
        stringBuilder.AppendLine($"Rotation: {Rotation}");
        return stringBuilder.ToString();
    }
}
