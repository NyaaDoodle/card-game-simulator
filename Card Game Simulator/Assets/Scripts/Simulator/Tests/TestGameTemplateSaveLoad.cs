using UnityEngine;

public class TestGameTemplateSaveLoad : MonoBehaviour
{
    private void Start()
    {
        // testFunc();
    }

    private void testFunc()
    {
        GameTemplate testGameTemplate = new TestGameTemplateInitialization().TestGameTemplate;
        string testGameTemplateJson = GameTemplateSaveLoad.Instance.SerializeGameTemplate(testGameTemplate);
        Debug.Log(testGameTemplateJson);
        GameTemplate deserializedGameTemplate =
            GameTemplateSaveLoad.Instance.DeserializeGameTemplate(testGameTemplateJson);
        Debug.Log(deserializedGameTemplate);
        Debug.Log($"Are equal? {testGameTemplate == deserializedGameTemplate}");
    }
}
