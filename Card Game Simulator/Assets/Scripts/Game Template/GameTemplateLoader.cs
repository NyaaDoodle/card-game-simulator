using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class GameTemplateLoader
{
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
        string templateFileInDirectory = Path.Combine(pathToTemplate, DataDirectoryManager.TemplateDataFilename);
        if (!File.Exists(templateFileInDirectory))
        {
            Debug.LogWarning("Game template file (template.json) does not exist");
            return null;
        }
        string gameTemplateJson = File.ReadAllText(templateFileInDirectory);
        return DeserializeGameTemplate(gameTemplateJson);
    }

    public static void DeleteGameTemplate(string templateId)
    {
        string templateDirectory = Path.Combine(DataDirectoryManager.Instance.TemplatesDirectoryPath, templateId);
        string imagesDirectory = Path.Combine(DataDirectoryManager.Instance.ImagesDirectoryPath, templateId);
        string thumbnailsDirectory = Path.Combine(DataDirectoryManager.Instance.ThumbnailsDirectoryPath, templateId);
        if (Directory.Exists(templateDirectory))
        {
            Directory.Delete(templateDirectory);
        }

        if (Directory.Exists(imagesDirectory))
        {
            Directory.Delete(imagesDirectory);
        }

        if (Directory.Exists(thumbnailsDirectory))
        {
            Directory.Delete(thumbnailsDirectory);
        }
    }
}
