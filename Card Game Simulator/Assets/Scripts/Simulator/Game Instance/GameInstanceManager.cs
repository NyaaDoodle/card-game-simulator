using UnityEngine;

public class GameInstanceManager : MonoBehaviour
{
    public GameInstance GameInstance { get; private set; }
    public GameTemplate GameTemplate { get; private set; }

    [Header("Spawn Prefabs")]
    [SerializeField] private GameObject tablePrefab;
    [SerializeField] private GameObject deckPrefab;
    [SerializeField] private GameObject spacePrefab;
    [SerializeField] private GameObject playerHandPrefab;

    [Header("Containers")]
    [SerializeField] private RectTransform tableContainer;
    [SerializeField] private RectTransform tableObjectsContainer;
    [SerializeField] private RectTransform playerHandContainer;

    private readonly GameTemplateLoader gameTemplateLoader = new GameTemplateLoader();
    private readonly GameInstanceLoader gameInstanceLoader = new GameInstanceLoader();

    void Start()
    { 
        loadGameTemplate();
        loadGameInstance();
    }

    private void loadGameTemplate()
    {
        GameTemplate = gameTemplateLoader.LoadGameTemplate();
    }

    private void loadGameInstance()
    {
        SpawnPrefabSetup spawnPrefabSetup = getSpawnPrefabSetup();
        GameInstance = gameInstanceLoader.LoadGameInstance(GameTemplate, spawnPrefabSetup);
    }

    private SpawnPrefabSetup getSpawnPrefabSetup()
    {
        return new SpawnPrefabSetup()
                   {
                       TablePrefab = tablePrefab,
                       DeckPrefab = deckPrefab,
                       SpacePrefab = spacePrefab,
                       PlayerHandPrefab = playerHandPrefab,
                       TableContainer = tableContainer,
                       TableObjectsContainer = tableObjectsContainer,
                       PlayerHandContainer = playerHandContainer
                   };
    }
}
