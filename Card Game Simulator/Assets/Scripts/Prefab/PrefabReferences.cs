using System;
using UnityEngine;

public class PrefabReferences : MonoBehaviour
{
    public static PrefabReferences Instance { get; private set; }

    public GameObject TablePrefab;
    public GameObject TableDisplayPrefab;
    public GameObject DeckPrefab;
    public GameObject DeckDisplayPrefab;
    public GameObject SpacePrefab;
    public GameObject SpaceDisplayPrefab;
    public GameObject PlayerPrefab;
    public GameObject PlayerHandDisplayPrefab;
    public GameObject GameTemplateSelectionEntityPrefab;
    public GameObject CardSelectionEntityPrefab;
    public GameObject DeckSelectionEntityPrefab;
    public GameObject SpaceSelectionEntityPrefab;
    public GameObject CardSelectionModalWindowPrefab;
    public GameObject GameTemplateSelectionModalWindowPrefab;
    public GameObject DeckSelectionModalWindowPrefab;
    public GameObject SpaceSelectionModalWindowPrefab;
    public GameObject MobileImageMethodModalWindowPrefab;
    public GameObject PlayerScoreEntityPrefab;
    
    public bool IsReady { get; private set; }

    void Awake()
    {
        initializeInstance();
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

    private void onReady()
    {
        IsReady = true;
    }
}