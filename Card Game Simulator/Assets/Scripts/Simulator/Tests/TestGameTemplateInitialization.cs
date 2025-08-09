using System.Collections.Generic;

public class TestGameTemplateInitialization
{
    public GameTemplate TestGameTemplate { get; private set; }
    private const int id = 0;
    private const string name = "Test Game Template";
    private const string description = "Testing";
    private TableData tableData = new TableData();
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
        TestGameTemplate = new GameTemplate(id, name, description, tableData, cardPool, decksData, spacesData);
    }

    private void makeTableData()
    {
        tableData = new TableData(20, 15, "TestGameTemplate/SimpleGreen");
    }

    private void makeCardDataListA()
    {
        cardDataListA.Add(new CardData(0, "Standard52/Gray_back", "Standard52/2C"));
        cardDataListA.Add(new CardData(1, "Standard52/Green_back", "Standard52/3D"));
        cardDataListA.Add(new CardData(2, "Standard52/Red_back", "Standard52/4H"));
        cardDataListA.Add(new CardData(3, "Standard52/Yellow_back", "Standard52/5S"));
    }

    private void makeCardDataListB()
    {
        cardDataListB.Add(new CardData(4, "Standard52/blue_back", "Standard52/10S"));
        cardDataListB.Add(new CardData(5, "Standard52/purple_back", "Standard52/9D"));
        cardDataListB.Add(new CardData(6, "Standard52/Green_back", "Standard52/8C"));
        cardDataListB.Add(new CardData(7, "Standard52/Red_back", "Standard52/7H"));
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
        decksData.Add(0, new DeckData(0, 5, -3.25f, cardDataListA.ToArray()));
        decksData.Add(1, new DeckData(0, -5, 3.25f, cardDataListA.ToArray()));
    }

    private void makeSpacesData()
    {
        spacesData.Add(0, new SpaceData(0, 0, 0));
        spacesData.Add(1, new SpaceData(1, 2.5f, 0));
        spacesData.Add(2, new SpaceData(2, 5, 0));
        spacesData.Add(3, new SpaceData(3, -2.5f, 0));
        spacesData.Add(4, new SpaceData(4, -5, 0));
    }
}