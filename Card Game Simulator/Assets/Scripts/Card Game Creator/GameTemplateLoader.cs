using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class GameTemplateLoader : MonoBehaviour
{
    public static GameTemplateLoader Instance { get; private set; }
    public const string TemplateFilename = "template.json";
    private TraceLogger logger;

    private void Awake()
    {
        initializeInstance();
        initializeLogger(false);
    }

    private void initializeInstance()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void initializeLogger(bool isEnabled)
    {
        logger = new TraceLogger();
        logger.IsEnabled = isEnabled;
    }

    public List<GameTemplate> LoadGameTemplates()
    {
        logger.LogMethod();
        List<GameTemplate> gameTemplates = new List<GameTemplate>();
        logger.LogList("gameTemplates", gameTemplates);
        string templatesDirectoryPath = DataDirectoryManager.Instance.TemplatesDirectoryPath;
        logger.LogVariable("templatesDirectoryPath", templatesDirectoryPath);
        string[] directories = Directory.GetDirectories(templatesDirectoryPath);
        logger.LogEnumerable("directories", directories);
        foreach (string directory in directories)
        {
            logger.LogVariable("directory", directory);
            string templateFileInDirectory = Path.Combine(directory, TemplateFilename);
            if (File.Exists(templateFileInDirectory))
            {
                string gameTemplateJson = File.ReadAllText(templateFileInDirectory);
                logger.LogVariable("gameTemplateJson", gameTemplateJson);
                GameTemplate gameTemplate = DeserializeGameTemplate(gameTemplateJson);
                logger.LogVariable("gameTemplate", gameTemplate);
                gameTemplates.Add(gameTemplate);
                logger.LogList("gameTemplates", gameTemplates);
            }
        }
        return gameTemplates;
    }

    public string SerializeGameTemplate(GameTemplate gameTemplate, Formatting formatting = Formatting.None)
    {
        return JsonConvert.SerializeObject(gameTemplate, formatting);
    }

    public GameTemplate DeserializeGameTemplate(string json)
    {
        return JsonConvert.DeserializeObject<GameTemplate>(json);
    }

    public GameTemplate LoadGameTemplate()
    {
        throw new NotImplementedException();
    }
}
