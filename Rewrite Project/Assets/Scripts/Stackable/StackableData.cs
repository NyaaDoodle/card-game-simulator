using System;

public class StackableData
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Tuple<float, float> LocationOnTable { get; set; } = new Tuple<float, float>(0, 0);
}
