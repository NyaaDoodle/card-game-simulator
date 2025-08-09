using System;
using Newtonsoft.Json;

public readonly struct SpaceData : IStackableData, IEquatable<SpaceData>
{
    public int Id { get; }
    public string Name { get; }
    public float TableXCoordinate { get; }
    public float TableYCoordinate { get; }
    public float Rotation { get; }

    [JsonConstructor]
    public SpaceData(
        int id,
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
        int id,
        float tableXCoordinate,
        float tableYCoordinate)
    {
        Id = id;
        Name = "";
        TableXCoordinate = tableXCoordinate;
        TableYCoordinate = tableYCoordinate;
        Rotation = 0;
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
}
