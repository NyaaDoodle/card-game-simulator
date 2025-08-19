using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class GameTemplateSaveLoad : MonoBehaviour
{
    public static GameTemplateSaveLoad Instance { get; private set; }
    
    private string dataDirectoryPath;
    private string templatesDirectoryPath;
    private string tempDirectoryPath;
    private string thumbnailsDirectoryPath;

    private void Awake()
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

    private void Start()
    {
        setDirectoryPaths();
        checkForDirectoriesExistence();
        Debug.Log(dataDirectoryPath);
    }

    private void setDirectoryPaths()
    {
        const string templatesDirectoryName = "templates";
        const string tempDirectoryName = "temp";
        const string thumbnailsDirectoryName = "thumbnails";
        
        LoggingManager.Instance.GameTemplateSaveLoadLogger.LogMethod();
        dataDirectoryPath = Application.persistentDataPath;
        templatesDirectoryPath = Path.Combine(dataDirectoryPath, templatesDirectoryName);
        tempDirectoryPath = Path.Combine(dataDirectoryPath, tempDirectoryName);
        thumbnailsDirectoryPath = Path.Combine(dataDirectoryPath, thumbnailsDirectoryName);
    }

    private void checkForDirectoriesExistence()
    {
        LoggingManager.Instance.GameTemplateSaveLoadLogger.LogMethod();
        if (!Directory.Exists(dataDirectoryPath))
        {
            Debug.LogError("Unity Persistent Data Directory does not exist");
            return;
        }

        if (!Directory.Exists(templatesDirectoryPath))
        {
            LoggingManager.Instance.GameTemplateSaveLoadLogger.Log($"Creating directory {templatesDirectoryPath}");
            Directory.CreateDirectory(templatesDirectoryPath);
        }

        if (!Directory.Exists(tempDirectoryPath))
        {
            LoggingManager.Instance.GameTemplateSaveLoadLogger.Log($"Creating directory {tempDirectoryPath}");
            Directory.CreateDirectory(tempDirectoryPath);
        }

        if (!Directory.Exists(thumbnailsDirectoryPath))
        {
            LoggingManager.Instance.GameTemplateSaveLoadLogger.Log($"Creating directory {thumbnailsDirectoryPath}");
            Directory.CreateDirectory(thumbnailsDirectoryPath);
        }
    }

    public string SerializeGameTemplate(GameTemplate gameTemplate)
    {
        return JsonConvert.SerializeObject(gameTemplate);
    }

    public GameTemplate DeserializeGameTemplate(string json)
    {
        return JsonConvert.DeserializeObject<GameTemplate>(json);
    }
}
