using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class GameTemplateLoader
{
    public const string TemplateFilename = "template.json";

    public static List<GameTemplate> LoadGameTemplates()
    {
        List<GameTemplate> gameTemplates = new List<GameTemplate>();
        string templatesDirectoryPath = DataDirectoryManager.Instance.TemplatesDirectoryPath;
        string[] directories = Directory.GetDirectories(templatesDirectoryPath);
        foreach (string directory in directories)
        {
            GameTemplate? loadedTemplateFromDirectory = LoadGameTemplateFromPath(directory);
            if (loadedTemplateFromDirectory != null)
            {
                gameTemplates.Add(loadedTemplateFromDirectory.Value);
            }
        }
        return gameTemplates;
    }

    public static string SerializeGameTemplate(GameTemplate gameTemplate, Formatting formatting = Formatting.None)
    {
        return JsonConvert.SerializeObject(gameTemplate, formatting);
    }

    public static GameTemplate DeserializeGameTemplate(string json)
    {
        return JsonConvert.DeserializeObject<GameTemplate>(json);
    }

    public static GameTemplate? LoadGameTemplateFromId(string templateId)
    {
        string templatesDirectoryPath = DataDirectoryManager.Instance.TemplatesDirectoryPath;
        string templateToLoadDirectoryPath = Path.Combine(templatesDirectoryPath, templateId);
        return LoadGameTemplateFromPath(templateToLoadDirectoryPath);
    }

    public static GameTemplate? LoadGameTemplateFromPath(string pathToTemplate)
    {
        if (!Directory.Exists(pathToTemplate))
        {
            Debug.LogWarning("Path to game template does not exist");
            return null;
        }
        string templateFileInDirectory = Path.Combine(pathToTemplate, TemplateFilename);
        if (!File.Exists(templateFileInDirectory))
        {
            Debug.LogWarning("Game template file (template.json) does not exist");
            return null;
        }
        string gameTemplateJson = File.ReadAllText(templateFileInDirectory);
        return DeserializeGameTemplate(gameTemplateJson);
    }
}
