using System.Collections.Generic;
using Mirror;

public class GameInstanceManager : NetworkBehaviour
{
    public GameTemplate GameTemplate { get; private set; }
    public Table Table { get; private set; }
    public readonly List<Deck> Decks = new List<Deck>();
    public readonly List<Space> Spaces = new List<Space>();

    public override void OnStartServer()
    {
        LoggerReferences.Instance.GameInstanceManagerLogger.LogMethod();
        base.OnStartServer();
        loadGameTemplate();
        spawnGameObjects();
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
        Deck deck = PrefabReferences.Instance.CardDeckPrefab.InstantiateDeck(deckData);
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
        Space space = PrefabReferences.Instance.CardSpacePrefab.InstantiateSpace(spaceData);
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
            Destroy(deck.gameObject);
        }

        foreach (Space space in Spaces)
        {
            Destroy(space.gameObject);
        }
    }
}
