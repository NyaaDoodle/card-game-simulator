using Mirror;

public class GameInstanceManager : NetworkBehaviour
{
    [SyncVar] private GameTemplate gameTemplate;
    [SyncVar] private Table table;
    public readonly SyncList<Deck> Decks = new SyncList<Deck>();
    public readonly SyncList<Space> Spaces = new SyncList<Space>();
    public readonly SyncDictionary<int, Player> Players = new SyncDictionary<int, Player>();

    public Table Table => table;
    public GameTemplate GameTemplate => gameTemplate;
    public Player LocalPlayer { get; private set; }

    public override void OnStartServer()
    {
        loadGameTemplate();
        spawnGameObjects();
    }

    public override void OnStopServer()
    {
        despawnGameObjects();
    }

    public void AddPlayer(NetworkConnectionToClient conn)
    {
        Player clientPlayer = PrefabReferences.Instance.PlayerPrefab.InstantiatePlayer(conn, "Player");
        Players.Add(clientPlayer.Id, clientPlayer);
        onPlayerAdd(conn);
    }

    public void RemovePlayer(NetworkConnectionToClient conn)
    {
        if (Players.TryGetValue(conn.connectionId, out Player player))
        {
            Destroy(player.gameObject);
            Players.Remove(conn.connectionId);
            onPlayerRemoved(conn);
        }
    }

    private void onPlayerAdd(NetworkConnectionToClient target)
    {
        LocalPlayer = Players[target.connectionId];
        ManagerReferences.Instance.SelectionManager.Setup(LocalPlayer);
    }

    private void onPlayerRemoved(NetworkConnectionToClient target)
    {
        LocalPlayer = null;
    }

    private void loadGameTemplate()
    {
        GameTemplateLoader gameTemplateLoader = new GameTemplateLoader();
        gameTemplate = gameTemplateLoader.LoadGameTemplate();
    }

    private void spawnGameObjects()
    {
        spawnTable();
        spawnDecks();
        spawnSpaces();
    }

    private void spawnTable()
    {
        table = PrefabReferences.Instance.TablePrefab.InstantiateTable(GameTemplate.TableData);
    }

    private void spawnDecks()
    {
        foreach (DeckData deckData in GameTemplate.DecksData.Values)
        {
            spawnDeck(deckData);
        }
    }

    private void spawnDeck(DeckData deckData)
    {
        Deck deck = PrefabReferences.Instance.CardDeckPrefab.InstantiateDeck(deckData);
        Decks.Add(deck);
    }

    private void spawnSpaces()
    {
        foreach (SpaceData spaceData in GameTemplate.SpacesData.Values)
        {
            spawnSpace(spaceData);
        }
    }

    private void spawnSpace(SpaceData spaceData)
    {
        Space space = PrefabReferences.Instance.CardSpacePrefab.InstantiateSpace(spaceData);
        Spaces.Add(space);
    }

    private void despawnGameObjects()
    {
        if (Table != null)
        {
            Destroy(Table.gameObject);
        }
        
        foreach (Deck deck in Decks)
        {
            Destroy(deck.gameObject);
        }

        foreach (Space space in Spaces)
        {
            Destroy(space.gameObject);
        }

        foreach (Player player in Players.Values)
        {
            Destroy(player.gameObject);
        }
    }
}
