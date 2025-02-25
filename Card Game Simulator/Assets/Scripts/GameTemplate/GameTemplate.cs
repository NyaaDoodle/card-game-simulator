using System;
using System.Collections.Generic;

public class GameTemplate
{
    public string Name { get; set; }
    public TableData TableData { get; set; }
    public CardPool CardPool { get; set; }
    // TODO thinking about and implementing these...
    //private readonly Dictionary<int, DeckData> deckDataDictionary;
    //private readonly Dictionary<int, PlacementLocationData> placementLocationDictionary;
}
