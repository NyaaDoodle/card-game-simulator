using System.Collections.Generic;
using System.IO;

public class TestGameTemplateInitialization
{
    public GameTemplate TestGameTemplate { get; private set; }

    private string id;
    private string imagesDirectory;
    private string thumbnailDirectory;
    private GameTemplateDetails gameTemplateDetails;
    private TableData tableData;
    private CardData[] cardPool;
    private DeckData[] decksData;
    private SpaceData[] spacesData;

    public TestGameTemplateInitialization()
    {
        const string magicString = "54cafadf-719a-4643-bfb5-ba94e43ee642";
        id = magicString;
        imagesDirectory = Path.Combine(DataDirectoryManager.Instance.ImagesDirectoryPath, id);
        thumbnailDirectory = Path.Combine(DataDirectoryManager.Instance.ThumbnailsDirectoryPath, id);
        makeGameTemplateDetails();
        makeTableData();
        makeCardPool();
        makeDecksData();
        makeSpacesData();
        makeTestTemplate();
    }

    private void makeTestTemplate()
    {
        TestGameTemplate = new GameTemplate(id, gameTemplateDetails, tableData, cardPool, decksData, spacesData);
    }

    private void makeGameTemplateDetails()
    {
        gameTemplateDetails = new GameTemplateDetails(
            "Test Game Template",
            "Melanie Sverdlov",
            "Test Game Template",
            Path.Combine(thumbnailDirectory, "thumbnail.png"));
    }

    private void makeTableData()
    {
        tableData = new TableData(20, 15, Path.Combine(imagesDirectory, "SimpleGreen.png"));
    }

    private void makeCardPool()
    {
        List<CardData> cardDataList = new List<CardData>();
        cardDataList.Add(new CardData("2C", "2C", "2C", Path.Combine(imagesDirectory, "Gray_back.jpg"), Path.Combine(imagesDirectory, "2C.jpg")));
        cardDataList.Add(new CardData("3D","3D", "3D", Path.Combine(imagesDirectory, "Green_back.jpg"), Path.Combine(imagesDirectory, "3D.jpg")));
        cardDataList.Add(new CardData("4H","4H", "4H", Path.Combine(imagesDirectory, "Red_back.jpg"), Path.Combine(imagesDirectory, "4H.jpg")));
        cardDataList.Add(new CardData("5S","5S", "5S", Path.Combine(imagesDirectory, "Yellow_back.jpg"), Path.Combine(imagesDirectory, "5S.jpg")));
        cardDataList.Add(new CardData("10S","10S", "10S", Path.Combine(imagesDirectory, "blue_back.jpg"), Path.Combine(imagesDirectory, "10S.jpg")));
        cardDataList.Add(new CardData("9D","9D", "9D", Path.Combine(imagesDirectory, "purple_back.jpg"), Path.Combine(imagesDirectory, "9D.jpg")));
        cardDataList.Add(new CardData("8C","8C", "8C", Path.Combine(imagesDirectory, "Green_back.jpg"), Path.Combine(imagesDirectory, "8C.jpg")));
        cardDataList.Add(new CardData("7H","7H", "7H", Path.Combine(imagesDirectory, "Red_back.jpg"), Path.Combine(imagesDirectory, "7H.jpg")));
        cardPool = cardDataList.ToArray();
    }

    private void makeDecksData()
    {
        List<DeckData> deckDataList = new List<DeckData>();
        deckDataList.Add(new DeckData("Deck 1","Deck 1", 5, -3.25f, 0, cardPool));
        deckDataList.Add(new DeckData("Deck 2","Deck 2", -5, 3.25f, 0, cardPool));
        decksData = deckDataList.ToArray();
    }

    private void makeSpacesData()
    {
        List<SpaceData> spaceDataList = new List<SpaceData>();
        spaceDataList.Add(new SpaceData("Space 1","Space 1", 0, 0, 0));
        spaceDataList.Add(new SpaceData("Space 2","Space 2", 2.5f, 0, 0));
        spaceDataList.Add(new SpaceData("Space 3","Space 3", 5, 0, 0));
        spaceDataList.Add(new SpaceData("Space 4","Space 4", -2.5f, 0, 0));
        spaceDataList.Add(new SpaceData("Space 5","Space 5", -5, 0, 0));
        spacesData = spaceDataList.ToArray();
    }
}