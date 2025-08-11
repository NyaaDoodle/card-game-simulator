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
        TraceLogger.LogMethod();
        base.OnStartServer();
        loadGameTemplate();
        spawnGameObjects();
    }

    public override void OnStopServer()
    {
        despawnGameObjects();
        base.OnStopServer();
    }

    private void loadGameTemplate()
    {
        GameTemplateLoader gameTemplateLoader = new GameTemplateLoader();
        GameTemplate = gameTemplateLoader.LoadGameTemplate();
    }

    private void spawnGameObjects()
    {
        spawnTable();
        spawnDecks();
        spawnSpaces();
    }

    private void spawnTable()
    {
        Table = PrefabReferences.Instance.TablePrefab.InstantiateTable(GameTemplate.TableData);
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
    }
}
