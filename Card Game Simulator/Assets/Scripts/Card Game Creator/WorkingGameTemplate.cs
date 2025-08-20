using System.Collections.Generic;

public class WorkingGameTemplate
{
    public string Id { get; set; }
    public GameTemplateDetails GameTemplateDetails { get; set; }
    public TableData TableData { get; set; }
    public readonly Dictionary<string, CardData> CardPool = new Dictionary<string, CardData>();
    public readonly Dictionary<string, DeckData> DecksData = new Dictionary<string, DeckData>();
    public readonly Dictionary<string, SpaceData> SpacesData = new Dictionary<string, SpaceData>();

    public WorkingGameTemplate()
    {
        // Creating a new game template on Card Game Creator
        Id = System.Guid.NewGuid().ToString();
    }

    public WorkingGameTemplate(GameTemplate gameTemplate)
    {
        // Working on an existing game template on Card Game Creator
        Id = gameTemplate.Id;
        GameTemplateDetails = gameTemplate.GameTemplateDetails;
        TableData = gameTemplate.TableData;
        setCardPool(gameTemplate.CardPool);
        setDecksData(gameTemplate.DecksData);
        setSpacesData(gameTemplate.SpacesData);
    }

    private void setCardPool(CardData[] cardDataArray)
    {
        if (cardDataArray == null) return;
        foreach (CardData cardData in cardDataArray)
        {
            CardPool.Add(cardData.Id, cardData);
        }
    }

    private void setDecksData(DeckData[] deckDataArray)
    {
        if (deckDataArray == null) return;
        foreach (DeckData deckData in deckDataArray)
        {
            DecksData.Add(deckData.Id, deckData);
        }
    }

    private void setSpacesData(SpaceData[] spaceDataArray)
    {
        if (spaceDataArray == null) return;
        foreach (SpaceData spaceData in spaceDataArray)
        {
            SpacesData.Add(spaceData.Id, spaceData);
        }
    }
}
