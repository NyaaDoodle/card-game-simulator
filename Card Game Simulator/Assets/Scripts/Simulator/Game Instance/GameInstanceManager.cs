using System.Collections.Generic;
using Mirror;

public class GameInstanceManager : NetworkBehaviour
{
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
        base.OnStartServer();
        GameTemplate gameTemplate = loadGameTemplate();
        spawnGameObjects(gameTemplate);
    }

    void Start()
    {
        if (!isClient) return;
        spawnDisplayObjects();
        setupSelectionManager();
    }

    public override void OnStopClient()
    {
        despawnDisplayObjects();
        base.OnStopClient();
    }

    public override void OnStopServer()
    {
        despawnGameObjects();
        base.OnStopServer();
    }

    private GameTemplate loadGameTemplate()
    {
        return CurrentPlayingGameTemplate.Instance.GameTemplate;
    }

    private void spawnGameObjects(GameTemplate gameTemplate)
    {
        spawnTable(gameTemplate.TableData);
        spawnDecks(gameTemplate.DecksData);
        spawnSpaces(gameTemplate.SpacesData);
    }

    private void spawnTable(TableData tableData)
    {
        Table = PrefabExtensions.InstantiateTable(tableData);
    }
    
    private void spawnTableDisplay()
    {
        if (Table != null)
        {
            TableDisplay = PrefabExtensions.InstantiateTableDisplay(Table);
        }
    }

    private void spawnDecks(DeckData[] decksData)
    {
        foreach (DeckData deckData in decksData)
        {
            spawnDeck(deckData);
        }
    }

    private void spawnDeckDisplays()
    {
        foreach (Deck deck in Decks)
        {
            spawnDeckDisplay(deck);
        }
    }

    private void spawnDeck(DeckData deckData)
    {
        Deck deck = PrefabExtensions.InstantiateDeck(deckData);
        Decks.Add(deck);
    }

    private void spawnDeckDisplay(Deck deck)
    {
        DeckDisplay deckDisplay = PrefabExtensions.InstantiateDeckDisplay(deck);
        DeckDisplays.Add(deckDisplay);
    }

    private void spawnSpaces(SpaceData[] spacesData)
    {
        foreach (SpaceData spaceData in spacesData)
        {
            spawnSpace(spaceData);
        }
    }

    private void spawnSpaceDisplays()
    {
        foreach (Space space in Spaces)
        {
            spawnSpaceDisplay(space);
        }
    }

    private void spawnSpace(SpaceData spaceData)
    {
        Space space = PrefabExtensions.InstantiateSpace(spaceData);
        Spaces.Add(space);
    }

    private void spawnSpaceDisplay(Space space)
    {
        SpaceDisplay spaceDisplay = PrefabExtensions.InstantiateSpaceDisplay(space);
        SpaceDisplays.Add(spaceDisplay);
    }

    private void despawnGameObjects()
    {
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
        spawnTableDisplay();
        spawnDeckDisplays();
        spawnSpaceDisplays();
    }

    private void despawnDisplayObjects()
    {
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

    private void setupSelectionManager()
    {
        ManagerReferences.Instance.SelectionManager.SetupGameObjectEvents();
    }
}
