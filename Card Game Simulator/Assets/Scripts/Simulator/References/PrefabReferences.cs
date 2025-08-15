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

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}