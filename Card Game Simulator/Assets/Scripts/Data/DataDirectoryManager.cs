using System;
using System.IO;
using UnityEngine;

public class DataDirectoryManager : MonoBehaviour
{
    public static DataDirectoryManager Instance { get; private set; }
    
    public string DataDirectoryPath { get; private set; }
    public string TemplatesDirectoryPath { get; private set; }
    public string ImagesDirectoryPath { get; private set; }
    
    public const string TemplatesDirectoryName = "templates";
    public const string ImagesDirectoryName = "images";
    
    public const string TemplateDataFilename = "template.json";

    public bool IsReady { get; private set; } = false;

    private void Awake()
    {
        initializeInstance();
        setDirectoryPaths();
        checkForDirectoriesExistence();
        onReady();
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
    
    private void setDirectoryPaths()
    {
        DataDirectoryPath = Application.persistentDataPath;
        TemplatesDirectoryPath = Path.Combine(DataDirectoryPath, TemplatesDirectoryName);
        ImagesDirectoryPath = Path.Combine(DataDirectoryPath, ImagesDirectoryName);
    }
    
    private void checkForDirectoriesExistence()
    {
        if (!Directory.Exists(DataDirectoryPath))
        {
            Debug.LogError("Unity Persistent Data Directory does not exist");
            return;
        }

        if (!Directory.Exists(TemplatesDirectoryPath))
        {
            Directory.CreateDirectory(TemplatesDirectoryPath);
        }

        if (!Directory.Exists(ImagesDirectoryPath))
        {
            Directory.CreateDirectory(ImagesDirectoryPath);
        }
    }

    private void onReady()
    {
        IsReady = true;
    }
}
