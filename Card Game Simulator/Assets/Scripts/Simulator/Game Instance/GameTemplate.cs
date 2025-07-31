using System.Collections.Generic;

public class GameTemplate
{
    public int Id { get; set; } = 0;
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public TableData TableData { get; set; } = null;
    public Dictionary<int, CardData> CardPool { get; set; } = new Dictionary<int, CardData>();
    public Dictionary<int, DeckData> DecksData { get; set; } = new Dictionary<int, DeckData>();
    public Dictionary<int, SpaceData> SpacesData { get; set; } = new Dictionary<int, SpaceData>();
}
