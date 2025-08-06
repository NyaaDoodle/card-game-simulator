using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class GameInstanceLoader
{
    public TableDisplay TableDisplay { get; private set; }
    public List<DeckDisplay> DeckDisplays { get; } = new List<DeckDisplay>();
    public List<SpaceDisplay> SpaceDisplays { get; } = new List<SpaceDisplay>();
    public List<PlayerHandDisplay> PlayerHandDisplays { get; } = new List<PlayerHandDisplay>();

    private GameInstance gameInstance;
    private GameTemplate gameTemplate;

    public GameInstance LoadGameInstance(GameTemplate gameTemplate)
    {
        this.gameTemplate = gameTemplate;
        gameInstance = new GameInstance(gameTemplate);
        spawnObjects();
        return gameInstance;
    }

    private void spawnObjects()
    {
        spawnTable();
        spawnDecks();
        spawnSpaces();
        spawnPlayers();
    }

    private void spawnTable()
    {
        Table table = new Table(gameTemplate.TableData);
        gameInstance.Table = table;
        TableDisplay tableDisplay = PrefabReferences.Instance.TablePrefab.InstantiateTableDisplay(table);
        TableDisplay = tableDisplay;
    }

    private void spawnDecks()
    {
        foreach (DeckData deckData in gameTemplate.DecksData.Values)
        {
            spawnDeck(deckData);
        }
    }

    private void spawnDeck(DeckData deckData)
    {
        Deck deck = new Deck(deckData);
        gameInstance.Decks.Add(deck);
        DeckDisplay deckDisplay = PrefabReferences.Instance.CardDeckPrefab.InstantiateDeckDisplay(
            deck,
            ContainerReferences.Instance.TableObjectsContainer);
        NetworkServer.Spawn(deckDisplay.gameObject);
        DeckDisplays.Add(deckDisplay);
    }

    private void spawnSpaces()
    {
        foreach (SpaceData spaceData in gameTemplate.SpacesData.Values)
        {
            spawnSpace(spaceData);
        }
    }

    private void spawnSpace(SpaceData spaceData)
    {
        Space space = new Space(spaceData);
        gameInstance.Spaces.Add(space);
        SpaceDisplay spaceDisplay = PrefabReferences.Instance.CardSpacePrefab.InstantiateSpaceDisplay(
            space,
            ContainerReferences.Instance.TableObjectsContainer);
        NetworkServer.Spawn(spaceDisplay.gameObject);
        SpaceDisplays.Add(spaceDisplay);
    }

    private void spawnPlayers()
    {
        // TODO implement
        Player player = new Player(new PlayerHand());
        gameInstance.Players.Add(player);
        PlayerHandDisplay playerHandDisplay = PrefabReferences.Instance.PlayerHandPrefab.InstantiatePlayerHandDisplay(
            player.PlayerHand,
            ContainerReferences.Instance.PlayerHandContainer);
        PlayerHandDisplays.Add(playerHandDisplay);
    }

    public void DespawnLeftoverObjects()
    {
        GameObject.Destroy(TableDisplay.gameObject);

        foreach (DeckDisplay deckDisplay in DeckDisplays)
        {
            GameObject.Destroy(deckDisplay.gameObject);
        }

        foreach (SpaceDisplay spaceDisplay in SpaceDisplays)
        {
            GameObject.Destroy(spaceDisplay.gameObject);
        }

        foreach (PlayerHandDisplay playerHandDisplay in PlayerHandDisplays)
        {
            GameObject.Destroy(playerHandDisplay.gameObject);
        }
    }
}
