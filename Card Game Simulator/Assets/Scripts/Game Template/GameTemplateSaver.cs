using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class GameTemplateSaver
{
    public static void SaveGameTemplate(WorkingGameTemplate workingGameTemplate)
    {
        const string templateDataFilename = DataDirectoryManager.TemplateDataFilename;
        checkGameTemplateDirectoryExists(workingGameTemplate.Id);
        string templateFilePath = Path.Combine(
            getGameTemplateDirectory(workingGameTemplate.Id),
            templateDataFilename);
        GameTemplate gameTemplate = workingGameTemplate.ConvertToGameTemplate();
        try
        {
            string gameTemplateJson = SerializeGameTemplate(gameTemplate);
            checkGameTemplateFileExists(workingGameTemplate.Id);
            File.WriteAllText(templateFilePath, gameTemplateJson);
            Debug.Log($"Wrote game template file of {gameTemplate.Id} at {templateFilePath}");
            Debug.Log(gameTemplateJson);
        }
        catch (Exception e)
        {
            Debug.Log($"Failed to save game template file at {templateFilePath}");
            Debug.LogException(e);
        }
        
    }
    
    public static string SerializeGameTemplate(GameTemplate gameTemplate, Formatting formatting = Formatting.None)
    {
        return JsonConvert.SerializeObject(gameTemplate, formatting);
    }

    private static void checkGameTemplateDirectoryExists(string gameTemplateId)
    {
        string gameTemplateDataDirectory = getGameTemplateDirectory(gameTemplateId);
        if (!Directory.Exists(gameTemplateDataDirectory))
        {
            Directory.CreateDirectory(gameTemplateDataDirectory);
        }
    }

    private static void checkGameTemplateFileExists(string gameTemplateId)
    {
        string dataFilePath = Path.Combine(
            getGameTemplateDirectory(gameTemplateId),
            DataDirectoryManager.TemplateDataFilename);
        if (File.Exists(dataFilePath))
        {
            File.Delete(dataFilePath);
        }
    }

    private static string getGameTemplateDirectory(string gameTemplateId)
    {
        return Path.Combine(DataDirectoryManager.Instance.TemplatesDirectoryPath, gameTemplateId);
    }
}
