using System;
using System.Collections.Generic;

public class TestGameTemplateInitialization
{
    public GameTemplate TestGameTemplate { get; private set; }
    private const int id = 0;
    private const string name = "Test Game Template";
    private const string description = "Testing";
    private readonly TableData tableData = new TableData();
    private readonly List<CardData> cardDataListA = new List<CardData>();
    private readonly List<CardData> cardDataListB = new List<CardData>();
    private readonly Dictionary<int, CardData> cardPool = new Dictionary<int, CardData>();
    private readonly Dictionary<int, DeckData> decksData = new Dictionary<int, DeckData>();
    private readonly Dictionary<int, SpaceData> spacesData = new Dictionary<int, SpaceData>();

    public TestGameTemplateInitialization()
    {
        makeTableData();
        makeCardDataListA();
        makeCardDataListB();
        makeCardPool();
        makeDecksData();
        makeSpacesData();
        makeTestTemplate();
    }

    private void makeTestTemplate()
    {
        TestGameTemplate = new GameTemplate()
                               {
                                   Id = id,
                                   Name = name,
                                   Description = description,
                                   TableData = tableData,
                                   CardPool = cardPool,
                                   DecksData = decksData,
                                   SpacesData = spacesData
                               };
    }

    private void makeTableData()
    {
        tableData.Width = 20;
        tableData.Height = 15;
        tableData.SurfaceImagePath = "TestGameTemplate/SimpleGreen";
    }

    private void makeCardDataListA()
    {
        cardDataListA.Add(
            new CardData()
                {
                    Id = 0,
                    BackSideSpritePath = "Standard52/Gray_back",
                    FrontSideSpritePath = "Standard52/2C"
                });
        cardDataListA.Add(
            new CardData()
                {
                    Id = 1,
                    BackSideSpritePath = "Standard52/Green_back",
                    FrontSideSpritePath = "Standard52/3D"
                });
        cardDataListA.Add(
            new CardData()
                {
                    Id = 2,
                    BackSideSpritePath = "Standard52/Red_back",
                    FrontSideSpritePath = "Standard52/4H"
                });
        cardDataListA.Add(
            new CardData()
                {
                    Id = 3,
                    BackSideSpritePath = "Standard52/Yellow_back",
                    FrontSideSpritePath = "Standard52/5S"
                });
    }

    private void makeCardDataListB()
    {
        cardDataListB.Add(
            new CardData()
                {
                    Id = 4,
                    BackSideSpritePath = "Standard52/blue_back",
                    FrontSideSpritePath = "Standard52/10S"
                });
        cardDataListB.Add(
            new CardData()
                {
                    Id = 5,
                    BackSideSpritePath = "Standard52/purple_back",
                    FrontSideSpritePath = "Standard52/9D"
                });
        cardDataListB.Add(
            new CardData()
                {
                    Id = 6,
                    BackSideSpritePath = "Standard52/Green_back",
                    FrontSideSpritePath = "Standard52/8C"
                });
        cardDataListB.Add(
            new CardData()
                {
                    Id = 7,
                    BackSideSpritePath = "Standard52/Red_back",
                    FrontSideSpritePath = "Standard52/7H"
                });
    }

    private void makeCardPool()
    {
        foreach (CardData cardData in cardDataListA)
        {
            cardPool.Add(cardData.Id, cardData);
        }
        foreach (CardData cardData in cardDataListB)
        {
            cardPool.Add(cardData.Id, cardData);
        }
    }

    private void makeDecksData()
    {
        decksData.Add(
            0,
            new DeckData() { Id = 0, LocationOnTable = new Tuple<float, float>(5, -3.25f), Cards = cardDataListA });
        decksData.Add(
            1,
            new DeckData() { Id = 0, LocationOnTable = new Tuple<float, float>(-5, 3.25f), Cards = cardDataListA });
    }

    private void makeSpacesData()
    {
        spacesData.Add(0, new SpaceData() { Id = 0, LocationOnTable = new Tuple<float, float>(0, 0) });
        spacesData.Add(1, new SpaceData() { Id = 1, LocationOnTable = new Tuple<float, float>(2.5f, 0) });
        spacesData.Add(2, new SpaceData() { Id = 2, LocationOnTable = new Tuple<float, float>(5, 0) });
        spacesData.Add(3, new SpaceData() { Id = 3, LocationOnTable = new Tuple<float, float>(-2.5f, 0) });
        spacesData.Add(4, new SpaceData() { Id = 4, LocationOnTable = new Tuple<float, float>(-5, 0) });
    }
}