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
        return testGameTemplate;
    }

    private CardPool getTestCardPool()
    {
        // TODO implement
        CardData twoOfClubsCardData = new CardData();
        twoOfClubsCardData.FrontSideSprite = Resources.Load<Sprite>("2C");
        twoOfClubsCardData.BackSideSprite = testDefaultBackSideSprite;
        return null;
    }
}
