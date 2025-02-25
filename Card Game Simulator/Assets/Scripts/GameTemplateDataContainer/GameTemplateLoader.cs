using UnityEngine;

public class GameTemplateLoader : MonoBehaviour
{
    [SerializeField]
    private GameTemplateDataContainer gameTemplateDataContainer;

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
        const float testTableWidth = 10;
        const float testTableHeight = 10;
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
        return null;
    }
}
