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
        const float defaultWidth = 20f;
        const float defaultHeight = 20f;
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

    public void SetTemplateThumbnail(string templateThumbnailImagePath)
    {
        GameTemplateDetails = new GameTemplateDetails(
            GameTemplateDetails.TemplateName,
            GameTemplateDetails.CreatorName,
            GameTemplateDetails.Description,
            templateThumbnailImagePath);
    }

    public void SetTableWidth(float width)
    {
        TableData = new TableData(width, TableData.Height, TableData.SurfaceImagePath);
    }

    public void SetTableHeight(float height)
    {
        TableData = new TableData(TableData.Width, height, TableData.SurfaceImagePath);
    }

    public void SetTableSurfaceImage(string tableSurfaceImagePath)
    {
        TableData = new TableData(TableData.Width, TableData.Height, tableSurfaceImagePath);
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
        string[] startingCardIds = Array.Empty<string>();
        DeckData deckData = new DeckData(
            deckId,
            defaultName,
            defaultTableXCoordinate,
            defaultTableYCoordinate,
            defaultRotation,
            startingCardIds);
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

    public CardData SetCardName(CardData cardData, string name)
    {
        CardData updatedCardData = new CardData(
            cardData.Id,
            name,
            cardData.Description,
            cardData.Width,
            cardData.Height,
            cardData.BackSideImagePath,
            cardData.FrontSideImagePath);
        CardPool[cardData.Id] = updatedCardData;
        return updatedCardData;
    }

    public CardData SetCardDescription(CardData cardData, string description)
    {
        CardData updatedCardData = new CardData(
            cardData.Id,
            cardData.Name,
            description,
            cardData.Width,
            cardData.Height,
            cardData.BackSideImagePath,
            cardData.FrontSideImagePath);
        CardPool[cardData.Id] = updatedCardData;
        return updatedCardData;
    }

    public CardData SetCardBackSideImagePath(CardData cardData, string backSideImagePath)
    {
        CardData updatedCardData = new CardData(
            cardData.Id,
            cardData.Name,
            cardData.Description,
            cardData.Width,
            cardData.Height,
            backSideImagePath,
            cardData.FrontSideImagePath);
        CardPool[cardData.Id] = updatedCardData;
        return updatedCardData;
    }
    
    public CardData SetCardFrontSideImagePath(CardData cardData, string frontSideImagePath)
    {
        CardData updatedCardData = new CardData(
            cardData.Id,
            cardData.Name,
            cardData.Description,
            cardData.Width,
            cardData.Height,
            cardData.BackSideImagePath,
            frontSideImagePath);
        CardPool[cardData.Id] = updatedCardData;
        return updatedCardData;
    }

    public void DeleteCardData(CardData cardData)
    {
        CardPool.Remove(cardData.Id);
    }

    public DeckData SetDeckName(DeckData deckData, string name)
    {
        DeckData updatedDeckData = new DeckData(
            deckData.Id,
            name,
            deckData.TableXCoordinate,
            deckData.TableYCoordinate,
            deckData.Rotation,
            deckData.StartingCardIds);
        DecksData[deckData.Id] = updatedDeckData;
        return updatedDeckData;
    }

    public DeckData SetDeckXCoordinate(DeckData deckData, float x)
    {
        DeckData updatedDeckData = new DeckData(
            deckData.Id,
            deckData.Name,
            x,
            deckData.TableYCoordinate,
            deckData.Rotation,
            deckData.StartingCardIds);
        DecksData[deckData.Id] = updatedDeckData;
        return updatedDeckData;
    }

    public DeckData SetDeckYCoordinate(DeckData deckData, float y)
    {
        DeckData updatedDeckData = new DeckData(
            deckData.Id,
            deckData.Name,
            deckData.TableXCoordinate,
            y,
            deckData.Rotation,
            deckData.StartingCardIds);
        DecksData[deckData.Id] = updatedDeckData;
        return updatedDeckData;
    }

    public DeckData SetDeckRotation(DeckData deckData, float rotation)
    {
        DeckData updatedDeckData = new DeckData(
            deckData.Id,
            deckData.Name,
            deckData.TableXCoordinate,
            deckData.TableYCoordinate,
            rotation,
            deckData.StartingCardIds);
        DecksData[deckData.Id] = updatedDeckData;
        return updatedDeckData;
    }

    public DeckData AddCardDataToDeckStartingCards(DeckData deckData, CardData cardData)
    {
        List<string> cardDataIdList = new List<string>(deckData.StartingCardIds);
        cardDataIdList.Add(cardData.Id);
        DeckData updatedDeckData = new DeckData(
            deckData.Id,
            deckData.Name,
            deckData.TableXCoordinate,
            deckData.TableYCoordinate,
            deckData.Rotation,
            cardDataIdList.ToArray());
        DecksData[deckData.Id] = updatedDeckData;
        return updatedDeckData;
    }

    public DeckData RemoveCardDataFromDeckStartingCards(DeckData deckData, CardData cardData)
    {
        List<string> cardDataIdList = new List<string>(deckData.StartingCardIds);
        cardDataIdList.Remove(cardData.Id);
        DeckData updatedDeckData = new DeckData(
            deckData.Id,
            deckData.Name,
            deckData.TableXCoordinate,
            deckData.TableYCoordinate,
            deckData.Rotation,
            cardDataIdList.ToArray());
        DecksData[deckData.Id] = updatedDeckData;
        return updatedDeckData;
    }

    public void DeleteDeckData(DeckData deckData)
    {
        DecksData.Remove(deckData.Id);
    }

    public SpaceData SetSpaceName(SpaceData spaceData, string name)
    {
        SpaceData updateSpaceData = new SpaceData(
            spaceData.Id,
            name,
            spaceData.TableXCoordinate,
            spaceData.TableYCoordinate,
            spaceData.Rotation);
        SpacesData[spaceData.Id] = updateSpaceData;
        return updateSpaceData;
    }

    public SpaceData SetSpaceXCoordinate(SpaceData spaceData, float x)
    {
        SpaceData updatedSpaceData = new SpaceData(
            spaceData.Id,
            spaceData.Name,
            x,
            spaceData.TableYCoordinate,
            spaceData.Rotation);
        SpacesData[spaceData.Id] = updatedSpaceData;
        return updatedSpaceData;
    }

    public SpaceData SetSpaceYCoordinate(SpaceData spaceData, float y)
    {
        SpaceData updatedSpaceData = new SpaceData(
            spaceData.Id,
            spaceData.Name,
            spaceData.TableXCoordinate,
            y,
            spaceData.Rotation);
        SpacesData[spaceData.Id] = updatedSpaceData;
        return updatedSpaceData;
    }

    public SpaceData SetSpaceRotation(SpaceData spaceData, float rotation)
    {
        SpaceData updateSpaceData = new SpaceData(
            spaceData.Id,
            spaceData.Name,
            spaceData.TableXCoordinate,
            spaceData.TableYCoordinate,
            rotation);
        SpacesData[spaceData.Id] = updateSpaceData;
        return updateSpaceData;
    }

    public void DeleteSpaceData(SpaceData spaceData)
    {
        SpacesData.Remove(spaceData.Id);
    }

    public Dictionary<string, CardData> GetCardDataDictionaryFromCardIds(IEnumerable<string> cardIds)
    {
        // Returns a dictionary (Card ID to corresponding Card Data) for the specified card IDs.
        // If a card ID is not found on CardPool, the function proceeds to the next card ID.
        Dictionary<string, CardData> dictionary = new Dictionary<string, CardData>();
        foreach (string cardId in cardIds)
        {
            if (CardPool.TryGetValue(cardId, out CardData cardData))
            {
                dictionary.Add(cardId, cardData);
            }
        }
        return dictionary;
    }
}
