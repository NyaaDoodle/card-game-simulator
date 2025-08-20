using System.Collections.Generic;

public class TestGameTemplateInitialization
{
    public GameTemplate TestGameTemplate { get; private set; }
    private GameTemplateDetails gameTemplateDetails;
    private TableData tableData;
    private CardData[] cardPool;
    private DeckData[] decksData;
    private SpaceData[] spacesData;

    public TestGameTemplateInitialization()
    {
        makeGameTemplateDetails();
        makeTableData();
        makeCardPool();
        makeDecksData();
        makeSpacesData();
        makeTestTemplate();
    }

    private void makeTestTemplate()
    {
        TestGameTemplate = new GameTemplate(gameTemplateDetails, tableData, cardPool, decksData, spacesData);
    }

    private void makeGameTemplateDetails()
    {
        gameTemplateDetails = new GameTemplateDetails(
            "Test Game Template",
            "Melanie Sverdlov",
            "Test Game Template",
            new ImageAssetReference("","",""));
    }

    private void makeTableData()
    {
        tableData = new TableData(20, 15, new ImageAssetReference("TestGameTemplate/SimpleGreen"));
    }

    private void makeCardPool()
    {
        List<CardData> cardDataList = new List<CardData>();
        cardDataList.Add(new CardData("2C", "2C", new ImageAssetReference("Standard52/Gray_back"), new ImageAssetReference("Standard52/2C")));
        cardDataList.Add(new CardData("3D", "3D", new ImageAssetReference("Standard52/Green_back"), new ImageAssetReference("Standard52/3D")));
        cardDataList.Add(new CardData("4H", "4H", new ImageAssetReference("Standard52/Red_back"), new ImageAssetReference("Standard52/4H")));
        cardDataList.Add(new CardData("5S", "5S", new ImageAssetReference("Standard52/Yellow_back"), new ImageAssetReference("Standard52/5S")));
        cardDataList.Add(new CardData("10S", "10S", new ImageAssetReference("Standard52/blue_back"), new ImageAssetReference("Standard52/10S")));
        cardDataList.Add(new CardData("9D", "9D", new ImageAssetReference("Standard52/purple_back"), new ImageAssetReference("Standard52/9D")));
        cardDataList.Add(new CardData("8C", "8C", new ImageAssetReference("Standard52/Green_back"), new ImageAssetReference("Standard52/8C")));
        cardDataList.Add(new CardData("7H", "7H", new ImageAssetReference("Standard52/Red_back"), new ImageAssetReference("Standard52/7H")));
        cardPool = cardDataList.ToArray();
    }

    private void makeDecksData()
    {
        List<DeckData> deckDataList = new List<DeckData>();
        deckDataList.Add(new DeckData("Deck 1", 5, -3.25f, 0, cardPool));
        deckDataList.Add(new DeckData("Deck 2", -5, 3.25f, 0, cardPool));
        decksData = deckDataList.ToArray();
    }

    private void makeSpacesData()
    {
        List<SpaceData> spaceDataList = new List<SpaceData>();
        spaceDataList.Add(new SpaceData("Space 1", 0, 0, 0));
        spaceDataList.Add(new SpaceData("Space 2", 2.5f, 0, 0));
        spaceDataList.Add(new SpaceData("Space 3", 5, 0, 0));
        spaceDataList.Add(new SpaceData("Space 4", -2.5f, 0, 0));
        spaceDataList.Add(new SpaceData("Space 5", -5, 0, 0));
        spacesData = spaceDataList.ToArray();
    }
}