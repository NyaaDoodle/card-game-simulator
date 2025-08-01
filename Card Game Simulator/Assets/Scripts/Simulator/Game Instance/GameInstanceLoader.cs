using System.Collections.Generic;
using UnityEngine;

public class GameInstanceLoader
{
    public TableDisplay TableDisplay { get; private set; }
    public List<DeckDisplay> DeckDisplays { get; } = new List<DeckDisplay>();
    public List<SpaceDisplay> SpaceDisplays { get; } = new List<SpaceDisplay>();
    public List<PlayerHandDisplay> PlayerHandDisplays { get; } = new List<PlayerHandDisplay>();

    private GameInstance gameInstance;
    private GameTemplate gameTemplate;
    private SpawnPrefabSetup spawnPrefabSetup;

    public GameInstance LoadGameInstance(GameTemplate inputGameTemplate, SpawnPrefabSetup inputSpawnPrefabSetup)
    {
        gameTemplate = inputGameTemplate;
        spawnPrefabSetup = inputSpawnPrefabSetup;
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
        TableDisplay tableDisplay =
            spawnPrefabSetup.TablePrefab.InstantiateTableDisplay(table, spawnPrefabSetup.TableContainer);
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
        DeckDisplay deckDisplay =
            spawnPrefabSetup.DeckPrefab.InstantiateDeckDisplay(deck, spawnPrefabSetup.TableObjectsContainer);
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
        SpaceDisplay spaceDisplay =
            spawnPrefabSetup.SpacePrefab.InstantiateSpaceDisplay(space, spawnPrefabSetup.TableObjectsContainer);
        SpaceDisplays.Add(spaceDisplay);
    }

    private void spawnPlayers()
    {
        // TODO implement
        Player player = new Player(new PlayerHand());
        gameInstance.Players.Add(player);
        PlayerHandDisplay playerHandDisplay = spawnPrefabSetup.PlayerHandPrefab.InstantiatePlayerHandDisplay(
            player.PlayerHand,
            spawnPrefabSetup.PlayerHandContainer);
        PlayerHandDisplays.Add(playerHandDisplay);
    }

    public void DespawnLeftoverObjects()
    {
        GameObject.Destroy(TableDisplay);

        foreach (DeckDisplay deckDisplay in DeckDisplays)
        {
            GameObject.Destroy(deckDisplay);
        }

        foreach (SpaceDisplay spaceDisplay in SpaceDisplays)
        {
            GameObject.Destroy(spaceDisplay);
        }

        foreach (PlayerHandDisplay playerHandDisplay in PlayerHandDisplays)
        {
            GameObject.Destroy(playerHandDisplay);
        }
    }
}
