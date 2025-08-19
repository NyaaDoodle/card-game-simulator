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
            "");
    }

    private void makeTableData()
    {
        tableData = new TableData(20, 15, "TestGameTemplate/SimpleGreen");
    }

    private void makeCardPool()
    {
        List<CardData> cardDataList = new List<CardData>();
        cardDataList.Add(new CardData("2C", "2C", "Standard52/Gray_back", "Standard52/2C"));
        cardDataList.Add(new CardData("3D", "3D", "Standard52/Green_back", "Standard52/3D"));
        cardDataList.Add(new CardData("4H", "4H", "Standard52/Red_back", "Standard52/4H"));
        cardDataList.Add(new CardData("5S", "5S", "Standard52/Yellow_back", "Standard52/5S"));
        cardDataList.Add(new CardData("10S", "10S", "Standard52/blue_back", "Standard52/10S"));
        cardDataList.Add(new CardData("9D", "9D", "Standard52/purple_back", "Standard52/9D"));
        cardDataList.Add(new CardData("8C", "8C", "Standard52/Green_back", "Standard52/8C"));
        cardDataList.Add(new CardData("7H", "7H", "Standard52/Red_back", "Standard52/7H"));
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