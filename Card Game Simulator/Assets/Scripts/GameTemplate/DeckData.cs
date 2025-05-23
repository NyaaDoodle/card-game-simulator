﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class DeckData
{
    public int Id { get; set; } = 0;
    public Tuple<float, float> LocationOnTable { get; set; } = new Tuple<float, float>(0, 0);

    // In-order card list
    public LinkedList<int> CardIds { get; set; } = new LinkedList<int>();
}