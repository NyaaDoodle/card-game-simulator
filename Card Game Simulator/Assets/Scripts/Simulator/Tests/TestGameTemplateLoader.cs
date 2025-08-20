using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class TestGameTemplateLoader : MonoBehaviour
{
    private void Start()
    {
        testFunc();
    }

    private void testFunc()
    {
        readGameTemplates();
    }

    private void saveTestTemplate()
    {
        const string filename = GameTemplateLoader.TemplateFilename;
        GameTemplate testGameTemplate = new TestGameTemplateInitialization().TestGameTemplate;
        string path = createTemplateDirectory(testGameTemplate);
        string filePath = Path.Combine(path, filename);
        string json = GameTemplateLoader.Instance.SerializeGameTemplate(testGameTemplate, Formatting.Indented);
        File.WriteAllText(filePath, json);
        Debug.Log($"Wrote to {filePath}");
    }

    private string createTemplateDirectory(GameTemplate gameTemplate)
    {
        string path = DataDirectoryManager.Instance.TemplatesDirectoryPath;
        string templateId = gameTemplate.Id;
        string templateDirectory = Path.Combine(path, templateId);
        Directory.CreateDirectory(templateDirectory);
        Debug.Log($"Created directory {templateDirectory}");
        return templateDirectory;
    }

    private void readGameTemplates()
    {
        Debug.Log("Attempting to load game templates");
        List<GameTemplate> loadedGameTemplates = GameTemplateLoader.Instance.LoadGameTemplates();
        foreach (GameTemplate loadedGameTemplate in loadedGameTemplates)
        {
            Debug.Log(loadedGameTemplate);
        }
    }
}
