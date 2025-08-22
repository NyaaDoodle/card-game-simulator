using System;
using System.IO;
using Extensions.Unity.ImageLoader;
using UnityEngine;
using UnityEngine.UI;

public class SimulatorImageLoader : MonoBehaviour
{
    public static SimulatorImageLoader Instance { get; private set; }
    
    private TraceLogger logger;
    private const bool isLoggerEnabled = false;
    private void Awake()
    {
        initializeInstance();
        initializeLogger();
        ImageLoader.Init();
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
    
    private void initializeLogger()
    {
        logger = new TraceLogger();
        logger.IsEnabled = isLoggerEnabled;
    }

    public void LoadImage(string localPath, Action<Sprite> onSuccessAction, Action<Exception> onFailureAction)
    {
        string fullPath = GetFullImagePath(localPath);
        ImageLoader.LoadSprite(fullPath)
            .Consume(onSuccessAction)
            .Failed(onFailureAction)
            .Forget();
    }

    public void LoadImage(string localPath, Image imageComponent, Sprite fallbackSprite)
    {
        LoadImage(localPath,
            (sprite) => imageComponent.sprite = sprite,
            (exception) =>
                {
                    string fullPath = GetFullImagePath(localPath);
                    logger.Log($"Failed to load image from {fullPath}");
                    logger.LogException(exception);
                    imageComponent.sprite = fallbackSprite;
                });
    }

    public string GetFullImagePath(string localPath)
    {
        // localPath needs to be local from the point of the persistent data directory
        return Path.Combine(DataDirectoryManager.Instance.DataDirectoryPath, localPath);
    }
}
