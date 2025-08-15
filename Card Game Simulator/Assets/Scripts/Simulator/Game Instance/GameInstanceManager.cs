using System.Collections.Generic;
using Mirror;
using UnityEngine;

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
    public PlayerHandDisplay LocalPlayerHandDisplay { get; private set; }

    public GameTemplate GameTemplate { get; private set; } // TODO synchronize on the network
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

    public override void OnStartClient()
    {
        base.OnStartClient();
        
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
        Table = PrefabReferences.Instance.TablePrefab.InstantiateTable(GameTemplate.TableData);
    }
    
    private void spawnTableDisplay()
    {
        LoggerReferences.Instance.GameInstanceManagerLogger.LogMethod();
        TableDisplay = PrefabReferences.Instance.TableDisplayPrefab.InstantiateTableDisplay(Table);
    }

    private void spawnDecks()
    {
        LoggerReferences.Instance.GameInstanceManagerLogger.LogMethod();
        foreach (DeckData deckData in GameTemplate.DecksData.Values)
        {
            spawnDeck(deckData);
        }
    }

    private void spawnDeck(DeckData deckData)
    {
        LoggerReferences.Instance.GameInstanceManagerLogger.LogMethod();
        Deck deck = PrefabReferences.Instance.DeckPrefab.InstantiateDeck(deckData);
        Decks.Add(deck);
    }

    private void spawnSpaces()
    {
        LoggerReferences.Instance.GameInstanceManagerLogger.LogMethod();
        foreach (SpaceData spaceData in GameTemplate.SpacesData.Values)
        {
            spawnSpace(spaceData);
        }
    }

    private void spawnSpace(SpaceData spaceData)
    {
        LoggerReferences.Instance.GameInstanceManagerLogger.LogMethod();
        Space space = PrefabReferences.Instance.SpacePrefab.InstantiateSpace(spaceData);
        Spaces.Add(space);
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
        if (Table != null)
        {
            spawnTableDisplay();
        }
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

        if (LocalPlayerHandDisplay != null)
        {
            Destroy(LocalPlayerHandDisplay.gameObject);
        }
    }
}
