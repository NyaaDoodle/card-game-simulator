using System.Collections.Generic;
using Newtonsoft.Json;

public readonly struct GameTemplate
{
    public int Id { get; }
    public string Name { get; }
    public string Description { get; }
    public TableData TableData { get; }
    public Dictionary<int, CardData> CardPool { get; }
    public Dictionary<int, DeckData> DecksData { get; }
    public Dictionary<int, SpaceData> SpacesData { get; }

    [JsonConstructor]
    public GameTemplate(
        int id,
        string name,
        string description,
        TableData tableData,
        Dictionary<int, CardData> cardPool,
        Dictionary<int, DeckData> decksData,
        Dictionary<int, SpaceData> spacesData)
    {
        Id = id;
        Name = name;
        Description = description;
        TableData = tableData;
        CardPool = cardPool;
        DecksData = decksData;
        SpacesData = spacesData;
    }

    public GameTemplate(
        int id,
        TableData tableData,
        Dictionary<int, CardData> cardPool,
        Dictionary<int, DeckData> decksData,
        Dictionary<int, SpaceData> spacesData)
    {
        Id = id;
        Name = "";
        Description = "";
        TableData = tableData;
        CardPool = cardPool;
        DecksData = decksData;
        SpacesData = spacesData;
    }
}
