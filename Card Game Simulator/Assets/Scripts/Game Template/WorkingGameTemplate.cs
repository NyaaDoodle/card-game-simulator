using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorkingGameTemplate
{
    public string Id { get; set; }
    public GameTemplateDetails GameTemplateDetails { get; set; }
    public TableData TableData { get; set; }
    public readonly Dictionary<string, CardData> CardPool = new Dictionary<string, CardData>();
    public readonly Dictionary<string, DeckData> DecksData = new Dictionary<string, DeckData>();
    public readonly Dictionary<string, SpaceData> SpacesData = new Dictionary<string, SpaceData>();
    private readonly Dictionary<string, Texture> imagesToSave = new Dictionary<string, Texture>();
    private readonly List<string> imagesToDelete = new List<string>();

    public WorkingGameTemplate()
    {
        // Creating a new game template on Card Game Creator
        Id = System.Guid.NewGuid().ToString();
        GameTemplateDetails = getDefaultGameTemplateDetails();
        TableData = getDefaultTableData();
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

    public GameTemplate ConvertToGameTemplate()
    {
        string id = Id;
        GameTemplateDetails gameTemplateDetails = GameTemplateDetails;
        TableData tableData = TableData;
        CardData[] cardPool = CardPool.Values.ToArray();
        DeckData[] decksData = DecksData.Values.ToArray();
        SpaceData[] spacesData = SpacesData.Values.ToArray();
        return new GameTemplate(id, gameTemplateDetails, tableData, cardPool, decksData, spacesData);
    }

    private GameTemplateDetails getDefaultGameTemplateDetails()
    {
        const string defaultTemplateName = "";
        const string defaultCreatorName = "";
        const string defaultDescription = "";
        const string defaultTemplateImagePath = "";
        return new GameTemplateDetails(
            defaultTemplateName,
            defaultCreatorName,
            defaultDescription,
            defaultTemplateImagePath);
    }

    private TableData getDefaultTableData()
    {
        const float defaultWidth = 15f;
        const float defaultHeight = 10f;
        const string defaultSurfaceImagePath = "";
        return new TableData(defaultWidth, defaultHeight, defaultSurfaceImagePath);
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

    public void SetTemplateName(string templateName)
    {
        GameTemplateDetails = new GameTemplateDetails(
            templateName,
            GameTemplateDetails.CreatorName,
            GameTemplateDetails.Description,
            GameTemplateDetails.TemplateImagePath);
    }

    public void SetTemplateCreatorName(string creatorName)
    {
        GameTemplateDetails = new GameTemplateDetails(
            GameTemplateDetails.TemplateName,
            creatorName,
            GameTemplateDetails.Description,
            GameTemplateDetails.TemplateImagePath);
    }

    public void SetTemplateDescription(string description)
    {
        GameTemplateDetails = new GameTemplateDetails(
            GameTemplateDetails.TemplateName,
            GameTemplateDetails.CreatorName,
            description,
            GameTemplateDetails.TemplateImagePath);
    }

    public void SetTemplateThumbnail(string thumbnailLocalPath)
    {
        GameTemplateDetails = new GameTemplateDetails(
            GameTemplateDetails.TemplateName,
            GameTemplateDetails.CreatorName,
            GameTemplateDetails.Description,
            thumbnailLocalPath);
    }

    public void SetTableWidth(float width)
    {
        TableData = new TableData(width, TableData.Height, TableData.SurfaceImagePath);
    }

    public void SetTableHeight(float height)
    {
        TableData = new TableData(TableData.Width, height, TableData.SurfaceImagePath);
    }

    public void SetTableSurfaceImage(string imageLocalPath)
    {
        TableData = new TableData(TableData.Width, TableData.Height, imageLocalPath);
    }
}
