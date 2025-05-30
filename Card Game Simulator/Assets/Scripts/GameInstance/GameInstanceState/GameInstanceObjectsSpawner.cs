using System;
using UnityEngine;

[RequireComponent(typeof(GameInstanceState))]
public class GameInstanceObjectsSpawner : MonoBehaviour
{
    private GameInstanceState gameInstanceState;
    private GameTemplate gameTemplate;

    [SerializeField] private GameObject tablePrefab;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject cardDeckPrefab;
    [SerializeField] private GameObject cardSpacePrefab;
    [SerializeField] private GameObject playerHandPrefab;

    void Awake()
    {
        gameInstanceState = GetComponent<GameInstanceState>();
    }

    void Start()
    {
        gameInstanceState.NewGameTemplateLoaded += gameTemplateDataContainer_OnNewGameTemplateLoaded;
        if (gameInstanceState.IsGameTemplateLoaded)
        {
            gameTemplate = gameInstanceState.GameTemplate;
            spawnGameInstanceObjects();
        }
    }

    private void gameTemplateDataContainer_OnNewGameTemplateLoaded(GameTemplate _)
    {
        gameTemplate = gameInstanceState.GameTemplate;
        spawnGameInstanceObjects();
    }

    private void spawnGameInstanceObjects()
    {
        spawnTable();
        spawnCardDecks();
        spawnCardSpaces();
        spawnPlayerHands();
    }

    private void spawnTable()
    {
        GameObject tableObject = Instantiate(tablePrefab);
        gameInstanceState.TableObject = tableObject;
        TableBehaviour tableBehaviour = tableObject.GetComponent<TableBehaviour>();
        tableBehaviour.Initialize(gameTemplate.TableData);
    }

    private void spawnCardDecks()
    {
        foreach (DeckData cardDeckData in gameTemplate.DecksData.Values)
        {
            spawnCardDeck(cardDeckData);
        }
    }

    private void spawnCardDeck(DeckData deckData)
    {
        GameObject cardDeckObject = Instantiate(cardDeckPrefab);
        gameInstanceState.CardDeckObjects.Add(cardDeckObject);
        placeObjectAtLocation(cardDeckObject, deckData.LocationOnTable);
        StackableState cardDeckStackableState = cardDeckObject.GetComponent<StackableState>();
        spawnCardsOfDeck(cardDeckStackableState, deckData);
    }

    private void spawnCardsOfDeck(StackableState cardDeckStackableState, DeckData deckData)
    {
        foreach (int cardId in deckData.CardIds)
        {
            CardData cardData = gameTemplate.CardPool[cardId];
            GameObject cardObject = spawnCard(cardData);
            cardDeckStackableState.AddCardToBottom(cardObject);
        }
    }

    private void spawnCardSpaces()
    {
        foreach (SpaceData cardSpaceData in gameTemplate.SpacesData.Values)
        {
            spawnCardSpace(cardSpaceData);
        }
    }

    private void spawnCardSpace(SpaceData spaceData)
    {
        GameObject cardSpaceObject = Instantiate(cardSpacePrefab);
        gameInstanceState.CardSpaceObjects.Add(cardSpaceObject);
        placeObjectAtLocation(cardSpaceObject, spaceData.LocationOnTable);
    }

    private void placeObjectAtLocation(GameObject gameObject, Tuple<float, float> locationOnTable)
    {
        float x = locationOnTable.Item1;
        float y = locationOnTable.Item2;
        gameObject.transform.localPosition = new Vector3(x, y, 0);
    }

    private GameObject spawnCard(CardData cardData)
    {
        GameObject cardObject = Instantiate(cardPrefab);
        gameInstanceState.CardObjects.Add(cardObject);
        CardState cardState = cardObject.GetComponent<CardState>();
        cardState.Initialize(cardData);
        return cardObject;
    }

    private void spawnPlayerHands()
    {
        // TODO implement
    }
}
