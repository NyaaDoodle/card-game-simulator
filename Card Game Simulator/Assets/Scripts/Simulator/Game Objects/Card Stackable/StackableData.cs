using System;

public readonly struct StackableData : IEquatable<StackableData>
{
    public int Id { get; }
    public string Name { get; }
    public float TableXCoordinate { get; }
    public float TableYCoordinate { get; }
    public float Rotation { get; }

    public StackableData(int id, string name, float tableXCoordinate, float tableYCoordinate, float rotation)
    {
        Id = id;
        Name = name;
        TableXCoordinate = tableXCoordinate;
        TableYCoordinate = tableYCoordinate;
        Rotation = rotation;
    }

    public bool Equals(StackableData other) => other.Id == this.Id;

    public override bool Equals(object obj) => obj is StackableData other && Equals(other);

    public override int GetHashCode() => Id.GetHashCode();

    public static bool operator ==(StackableData stackableData1, StackableData stackableData2)
    {
        return stackableData1.Equals(stackableData2);
    }

    public static bool operator !=(StackableData stackableData1, StackableData stackableData2)
    {
        return !stackableData1.Equals(stackableData2);
    }
}
