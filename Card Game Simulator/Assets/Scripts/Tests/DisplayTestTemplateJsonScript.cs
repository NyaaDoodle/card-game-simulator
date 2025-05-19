using TMPro;
using UnityEngine;
using Newtonsoft.Json;

public class DisplayTestTemplateJsonScript : MonoBehaviour
{
    [SerializeField] private GameInstanceState gameInstanceState;
    [SerializeField] private TMP_Text textDisplay;

    void Start()
    {
        gameInstanceState.NewGameTemplateLoaded += gameInstanceStateOnNewGameTemplateLoaded;
        if (gameInstanceState.GameTemplate != null)
        {
            displayTestTemplateJson();
        }
    }

    private void displayTestTemplateJson()
    {
        string testTemplateJson = getTestTemplateJson();
        textDisplay.text = testTemplateJson;
        Debug.Log(testTemplateJson);
    }

    private void gameInstanceStateOnNewGameTemplateLoaded(GameTemplate _)
    {
        displayTestTemplateJson();
    }


    private string getTestTemplateJson()
    {
        return JsonConvert.SerializeObject(gameInstanceState.GameTemplate, Formatting.Indented);
    }
}
