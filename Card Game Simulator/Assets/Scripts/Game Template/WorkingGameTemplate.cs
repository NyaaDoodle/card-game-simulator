using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorkingGameTemplate
{
    public string Id { get; private set; }
    public GameTemplateDetails GameTemplateDetails { get; private set; }
    public TableData TableData { get; private set; }
    public readonly Dictionary<string, CardData> CardPool = new Dictionary<string, CardData>();
    public readonly Dictionary<string, DeckData> DecksData = new Dictionary<string, DeckData>();
    public readonly Dictionary<string, SpaceData> SpacesData = new Dictionary<string, SpaceData>();

    public WorkingGameTemplate()
    {
        // Creating a new game template on Card Game Creator
        Id = System.Guid.NewGuid().ToString();
        createDefaultGameTemplateDetails();
        createDefaultTableData();
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
        CardData[] cardPoolArray = this.CardPool.Values.ToArray();
        DeckData[] decksDataArray = this.DecksData.Values.ToArray();
        SpaceData[] spacesDataArray = this.SpacesData.Values.ToArray();
        return new GameTemplate(Id, GameTemplateDetails, TableData, cardPoolArray, decksDataArray, spacesDataArray);
    }

    private void createDefaultGameTemplateDetails()
    {
        const string defaultTemplateName = "";
        const string defaultCreatorName = "";
        const string defaultDescription = "";
        const string defaultTemplateImagePath = "";
        GameTemplateDetails = new GameTemplateDetails(
            defaultTemplateName,
            defaultCreatorName,
            defaultDescription,
            defaultTemplateImagePath);
    }

    private void createDefaultTableData()
    {
        const float defaultWidth = 25f;
        const float defaultHeight = 10f;
        const string defaultSurfaceImagePath = "";
        TableData = new TableData(defaultWidth, defaultHeight, defaultSurfaceImagePath);
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

    public CardData CreateNewDefaultCardData()
    {
        const string defaultName = "";
        const string defaultDescription = "";
        const float defaultWidth = 2f;
        const float defaultHeight = 3f;
        const string defaultBackSideImagePath = "";
        const string defaultFrontSideImagePath = "";
        string cardId = Guid.NewGuid().ToString();
        CardData cardData = new CardData(
            cardId,
            defaultName,
            defaultDescription,
            defaultWidth,
            defaultHeight,
            defaultBackSideImagePath,
            defaultFrontSideImagePath);
        CardPool.Add(cardData.Id, cardData);
        return cardData;
    }

    public DeckData CreateNewDefaultDeckData()
    {
        const string defaultName = "";
        const float defaultTableXCoordinate = 0;
        const float defaultTableYCoordinate = 0;
        const float defaultRotation = 0;
        string deckId = Guid.NewGuid().ToString();
        CardData[] startingCards = Array.Empty<CardData>();
        DeckData deckData = new DeckData(
            deckId,
            defaultName,
            defaultTableXCoordinate,
            defaultTableYCoordinate,
            defaultRotation,
            startingCards);
        DecksData.Add(deckData.Id, deckData);
        return deckData;
    }

    public SpaceData CreateNewDefaultSpaceData()
    {
        const string defaultName = "";
        const float defaultTableXCoordinate = 0;
        const float defaultTableYCoordinate = 0;
        const float defaultRotation = 0;
        string spaceId = Guid.NewGuid().ToString();
        SpaceData spaceData = new SpaceData(
            spaceId,
            defaultName,
            defaultTableXCoordinate,
            defaultTableYCoordinate,
            defaultRotation);
        SpacesData.Add(spaceData.Id, spaceData);
        return spaceData;
    }
}
