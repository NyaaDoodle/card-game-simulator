using UnityEngine;

public class PrefabReferences : MonoBehaviour
{
    public static PrefabReferences Instance { get; private set; }

    public GameObject TablePrefab;
    public GameObject CardDeckPrefab;
    public GameObject CardSpacePrefab;
    public GameObject PlayerPrefab;
    public GameObject CardTableDisplayPrefab;

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