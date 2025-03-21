using Unity.VisualScripting;
using UnityEngine;

public class GameTemplateLoader : MonoBehaviour
{
    [SerializeField] private GameTemplateDataContainer gameTemplateDataContainer;
    [SerializeField] private float testTableWidth;
    [SerializeField] private float testTableHeight;
    [SerializeField] private Sprite testDefaultBackSideSprite;

    void Start()
    {
        changeToTestGameTemplate();
    }

    private void changeToTestGameTemplate()
    {
        GameTemplate testGameTemplate = getTestGameTemplate();
        gameTemplateDataContainer.ChangeCurrentGameTemplate(testGameTemplate);
    }

    private GameTemplate getTestGameTemplate()
    {
        const string testGameTemplateName = "Test Template";
        TableData testTableData = new TableData(testTableWidth, testTableHeight);
        CardPool testCardPool = getTestCardPool();
        GameTemplate testGameTemplate = new GameTemplate();
        testGameTemplate.Name = testGameTemplateName;
        testGameTemplate.TableData = testTableData;
        testGameTemplate.CardPool = testCardPool;
        // TODO implement test decks and placement locations
        return testGameTemplate;
    }

    private CardPool getTestCardPool()
    {
        CardPool testCardPool = new CardPool();
        CardData twoOfClubsCardData = new CardData();
        twoOfClubsCardData.FrontSideSprite = Resources.Load<Sprite>("2C");
        twoOfClubsCardData.BackSideSprite = testDefaultBackSideSprite;
        testCardPool.AddCardData(twoOfClubsCardData);
        CardData threeOfClubsCardData = new CardData();
        threeOfClubsCardData.FrontSideSprite = Resources.Load<Sprite>("3C");
        threeOfClubsCardData.BackSideSprite = testDefaultBackSideSprite;
        testCardPool.AddCardData(threeOfClubsCardData);
        CardData fourOfClubsCardData = new CardData();
        threeOfClubsCardData.FrontSideSprite = Resources.Load<Sprite>("4C");
        threeOfClubsCardData.BackSideSprite = testDefaultBackSideSprite;
        testCardPool.AddCardData(fourOfClubsCardData);
        CardData fiveOfClubsCardData = new CardData();
        threeOfClubsCardData.FrontSideSprite = Resources.Load<Sprite>("5C");
        threeOfClubsCardData.BackSideSprite = testDefaultBackSideSprite;
        testCardPool.AddCardData(fiveOfClubsCardData);
        return testCardPool;
    }
}
