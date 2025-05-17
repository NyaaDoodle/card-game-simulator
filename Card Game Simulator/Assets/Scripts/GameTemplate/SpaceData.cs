using System;
using System.Collections.Generic;
using UnityEngine;

public class SpaceData
{
    public int Id { get; set; } = 0;
    public Tuple<float, float> LocationOnTable { get; set; } = new Tuple<float, float>(0, 0);
}