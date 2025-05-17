using TMPro;
using UnityEngine;
using Newtonsoft.Json;

public class DisplayTestTemplateJsonScript : MonoBehaviour
{
    [SerializeField] private GameTemplateDataContainer gameTemplateDataContainer;
    [SerializeField] private TMP_Text textDisplay;

    void Start()
    {
        gameTemplateDataContainer.NewGameTemplateLoaded += gameTemplateDataContainer_OnNewGameTemplateLoaded;
        if (gameTemplateDataContainer.CurrentGameTemplate != null)
        {
            displayTestTemplateJson();
        }
    }

    private void displayTestTemplateJson()
    {
        string testTemplateJson = getTestTemplateJson();
        //string testTemplateJson = gameTemplateDataContainer.CurrentGameTemplate.ToString();
        textDisplay.text = testTemplateJson;
        Debug.Log(testTemplateJson);
    }

    private void gameTemplateDataContainer_OnNewGameTemplateLoaded(GameTemplate _)
    {
        displayTestTemplateJson();
    }


    private string getTestTemplateJson()
    {
        return JsonConvert.SerializeObject(gameTemplateDataContainer.CurrentGameTemplate);
    }
}
