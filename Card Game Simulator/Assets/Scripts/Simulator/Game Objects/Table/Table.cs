using System;

public readonly struct Table : IEquatable<Table>
{ 
    public TableData TableData { get; }

    public Table(TableData tableData)
    {
        TableData = tableData;
    }

    public bool Equals(Table other) => other.TableData == this.TableData;

    public override bool Equals(object obj) => obj is Table table && Equals(table);

    public override int GetHashCode() => TableData.GetHashCode();

    public static bool operator ==(Table table1, Table table2)
    {
        return table1.Equals(table2);
    }

    public static bool operator !=(Table table1, Table table2)
    {
        return !table1.Equals(table2);
    }
}
