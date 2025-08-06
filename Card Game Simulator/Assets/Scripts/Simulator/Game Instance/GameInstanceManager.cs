using Mirror;
using UnityEngine;

public class GameInstanceManager : NetworkBehaviour
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

    [Header("Managers")]
    [SerializeField] private SelectionManager selectionManager;

    private readonly GameTemplateLoader gameTemplateLoader = new GameTemplateLoader();
    private readonly GameInstanceLoader gameInstanceLoader = new GameInstanceLoader();

    void Start()
    { 
        loadGameTemplate();
        loadGameInstance();
        setupSelectionManager();
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

    private void setupSelectionManager()
    {
        Player localPlayer = GameInstance.Players[0];
        selectionManager.Setup(GameInstance, localPlayer);
    }
}
