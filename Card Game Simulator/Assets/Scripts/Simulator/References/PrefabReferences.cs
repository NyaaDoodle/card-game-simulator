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

    void Awake()
    {
        initializeInstance();
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
}