using System.Collections.Generic;
using Mirror;

public class GameInstanceManager : NetworkBehaviour
{
    public GameTemplate GameTemplate { get; private set; } // Not synchronized on the network
    
    // Network-synced objects
    [SyncVar] private Table table;
    private readonly SyncList<Deck> decks = new SyncList<Deck>();
    private readonly SyncList<Space> spaces = new SyncList<Space>();
    
    // Client-local objects
    public TableDisplay TableDisplay { get; private set; }
    public List<DeckDisplay> DeckDisplays { get; } = new List<DeckDisplay>();
    public List<SpaceDisplay> SpaceDisplays { get; } = new List<SpaceDisplay>();
    
    public Table Table
    {
        get
        {
            return table;
        }
        private set
        {
            table = value;
        }
    }

    public IList<Deck> Decks => decks;

    public IList<Space> Spaces => spaces;
    

    public override void OnStartServer()
    {
        LoggerReferences.Instance.GameInstanceManagerLogger.LogMethod();
        base.OnStartServer();
        loadGameTemplate();
        spawnGameObjects();
    }

    void Start()
    {
        if (!isClient) return;
        spawnDisplayObjects();
    }

    public override void OnStopClient()
    {
        LoggerReferences.Instance.GameInstanceManagerLogger.LogMethod();
        despawnDisplayObjects();
        base.OnStopClient();
    }

    public override void OnStopServer()
    {
        LoggerReferences.Instance.GameInstanceManagerLogger.LogMethod();
        despawnGameObjects();
        base.OnStopServer();
    }

    private void loadGameTemplate()
    {
        LoggerReferences.Instance.GameInstanceManagerLogger.LogMethod();
        GameTemplateLoader gameTemplateLoader = new GameTemplateLoader();
        GameTemplate = gameTemplateLoader.LoadGameTemplate();
    }

    private void spawnGameObjects()
    {
        LoggerReferences.Instance.GameInstanceManagerLogger.LogMethod();
        spawnTable();
        spawnDecks();
        spawnSpaces();
    }

    private void spawnTable()
    {
        LoggerReferences.Instance.GameInstanceManagerLogger.LogMethod();
        Table = PrefabExtensions.InstantiateTable(GameTemplate.TableData);
    }
    
    private void spawnTableDisplay()
    {
        LoggerReferences.Instance.GameInstanceManagerLogger.LogMethod();
        if (Table != null)
        {
            TableDisplay = PrefabExtensions.InstantiateTableDisplay(Table);
        }
    }

    private void spawnDecks()
    {
        LoggerReferences.Instance.GameInstanceManagerLogger.LogMethod();
        foreach (DeckData deckData in GameTemplate.DecksData.Values)
        {
            spawnDeck(deckData);
        }
    }

    private void spawnDeckDisplays()
    {
        LoggerReferences.Instance.GameInstanceManagerLogger.LogMethod();
        foreach (Deck deck in Decks)
        {
            spawnDeckDisplay(deck);
        }
    }

    private void spawnDeck(DeckData deckData)
    {
        LoggerReferences.Instance.GameInstanceManagerLogger.LogMethod();
        Deck deck = PrefabExtensions.InstantiateDeck(deckData);
        Decks.Add(deck);
    }

    private void spawnDeckDisplay(Deck deck)
    {
        LoggerReferences.Instance.GameInstanceManagerLogger.LogMethod();
        DeckDisplay deckDisplay = PrefabExtensions.InstantiateDeckDisplay(deck);
        DeckDisplays.Add(deckDisplay);
    }

    private void spawnSpaces()
    {
        LoggerReferences.Instance.GameInstanceManagerLogger.LogMethod();
        foreach (SpaceData spaceData in GameTemplate.SpacesData.Values)
        {
            spawnSpace(spaceData);
        }
    }

    private void spawnSpaceDisplays()
    {
        LoggerReferences.Instance.GameInstanceManagerLogger.LogMethod();
        foreach (Space space in Spaces)
        {
            spawnSpaceDisplay(space);
        }
    }

    private void spawnSpace(SpaceData spaceData)
    {
        LoggerReferences.Instance.GameInstanceManagerLogger.LogMethod();
        Space space = PrefabExtensions.InstantiateSpace(spaceData);
        Spaces.Add(space);
    }

    private void spawnSpaceDisplay(Space space)
    {
        LoggerReferences.Instance.GameInstanceManagerLogger.LogMethod();
        SpaceDisplay spaceDisplay = PrefabExtensions.InstantiateSpaceDisplay(space);
        SpaceDisplays.Add(spaceDisplay);
    }

    private void despawnGameObjects()
    {
        LoggerReferences.Instance.GameInstanceManagerLogger.LogMethod();
        if (Table != null)
        {
            Destroy(Table.gameObject);
        }
        
        foreach (Deck deck in Decks)
        {
            if (deck != null)
            {
                Destroy(deck.gameObject);
            }
        }

        foreach (Space space in Spaces)
        {
            if (space != null)
            {
                Destroy(space.gameObject);
            }
        }
    }

    private void spawnDisplayObjects()
    {
        LoggerReferences.Instance.GameInstanceManagerLogger.LogMethod();
        spawnTableDisplay();
        spawnDeckDisplays();
        spawnSpaceDisplays();
    }

    private void despawnDisplayObjects()
    {
        LoggerReferences.Instance.GameInstanceManagerLogger.LogMethod();
        if (TableDisplay != null)
        {
            Destroy(TableDisplay.gameObject);
        }

        foreach (DeckDisplay deckDisplay in DeckDisplays)
        {
            if (deckDisplay != null)
            {
                Destroy(deckDisplay.gameObject);
            }
        }
        
        foreach (SpaceDisplay spaceDisplay in SpaceDisplays)
        {
            if (spaceDisplay != null)
            {
                Destroy(spaceDisplay.gameObject);
            }
        }
    }
}
