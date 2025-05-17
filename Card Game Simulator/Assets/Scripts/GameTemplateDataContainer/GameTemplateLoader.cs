using UnityEngine;

public class GameTemplateLoader : MonoBehaviour
{
    [SerializeField] private GameTemplateDataContainer gameTemplateDataContainer;

    void Start()
    {
        changeToTestGameTemplate();
    }

    private void changeToTestGameTemplate()
    {
        GameTemplate testGameTemplate = TestGameTemplateInitialization.GetTestTemplate();
        gameTemplateDataContainer.ChangeCurrentGameTemplate(testGameTemplate);
    }
}
