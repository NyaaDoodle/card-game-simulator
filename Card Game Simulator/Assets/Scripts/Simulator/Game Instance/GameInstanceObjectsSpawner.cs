using System;
using UnityEngine;

[RequireComponent(typeof(GameInstanceState))]
public class GameInstanceObjectsSpawner : MonoBehaviour
{
    //private GameInstanceState gameInstanceState;
    //private GameTemplate gameTemplate;

    //[Header("Prefabs to Spawn")]
    //[SerializeField] private GameObject tablePrefab;
    //[SerializeField] private GameObject cardPrefab;
    //[SerializeField] private GameObject cardDeckPrefab;
    //[SerializeField] private GameObject cardSpacePrefab;
    //[SerializeField] private GameObject playerHandPrefab;

    //[Header("UI Containers")]
    //[SerializeField] private RectTransform tableObjectsContainer;
    //[SerializeField] private RectTransform playerHandContainer;

    //void Awake()
    //{
    //    gameInstanceState = GetComponent<GameInstanceState>();
    //}

    //void Start()
    //{
    //    gameInstanceState.NewGameTemplateLoaded += gameTemplateDataContainer_OnNewGameTemplateLoaded;
    //    if (gameInstanceState.IsGameTemplateLoaded)
    //    {
    //        gameTemplate = gameInstanceState.GameTemplate;
    //        spawnGameInstanceObjects();
    //    }
    //}

    //void OnDestroy()
    //{
    //    if (gameInstanceState != null)
    //    {
    //        gameInstanceState.NewGameTemplateLoaded -= gameTemplateDataContainer_OnNewGameTemplateLoaded;
    //    }
    //}

    //private void gameTemplateDataContainer_OnNewGameTemplateLoaded(GameTemplate _)
    //{
    //    gameTemplate = gameInstanceState.GameTemplate;
    //    spawnGameInstanceObjects();
    //}

    //private void spawnGameInstanceObjects()
    //{
    //    spawnTable();
    //    spawnCardDecks();
    //    spawnCardSpaces();
    //    spawnPlayerHands();
    //}

    //private void spawnTable()
    //{
    //    GameObject tableObject = Instantiate(tablePrefab, tableObjectsContainer);
    //    gameInstanceState.TableObject = tableObject;
    //    TableState tableState = tableObject.GetComponent<TableState>();
    //    tableState.Initialize(gameTemplate.TableData);

    //    RectTransform tableRectTransform = tableObject.GetComponent<RectTransform>();
    //    tableRectTransform.anchoredPosition = Vector2.zero;
    //}

    //private void spawnCardDecks()
    //{
    //    foreach (DeckData cardDeckData in gameTemplate.DecksData.Values)
    //    {
    //        spawnCardDeck(cardDeckData);
    //    }
    //}

    //private void spawnCardDeck(DeckData deckData)
    //{
    //    GameObject cardDeckObject = Instantiate(cardDeckPrefab, tableObjectsContainer);
    //    gameInstanceState.CardDeckObjects.Add(cardDeckObject);
    //    placeObjectAtLocation(cardDeckObject, deckData.LocationOnTable);

    //    CardDeckBehaviour cardDeckBehaviour = cardDeckObject.GetComponent<CardDeckBehaviour>();
    //    cardDeckBehaviour.Initialize(deckData);

    //    StackableState cardDeckStackableState = cardDeckObject.GetComponent<StackableState>();
    //    spawnCardsOfDeck(cardDeckStackableState, deckData);
    //}

    //private void spawnCardsOfDeck(StackableState cardDeckStackableState, DeckData deckData)
    //{
    //    foreach (int cardId in deckData.CardIds)
    //    {
    //        CardData cardData = gameTemplate.CardPool[cardId];
    //        GameObject cardObject = spawnCard(cardData);
    //        cardDeckStackableState.AddCardToBottom(cardObject);
    //    }
    //}

    //private void spawnCardSpaces()
    //{
    //    foreach (SpaceData cardSpaceData in gameTemplate.SpacesData.Values)
    //    {
    //        spawnCardSpace(cardSpaceData);
    //    }
    //}

    //private void spawnCardSpace(SpaceData spaceData)
    //{
    //    GameObject cardSpaceObject = Instantiate(cardSpacePrefab, tableObjectsContainer);
    //    gameInstanceState.CardSpaceObjects.Add(cardSpaceObject);
    //    placeObjectAtLocation(cardSpaceObject, spaceData.LocationOnTable);
    //    CardSpaceBehaviour cardSpaceBehaviour = cardSpaceObject.GetComponent<CardSpaceBehaviour>();
    //    cardSpaceBehaviour.Initialize(spaceData);
    //}

    //private void placeObjectAtLocation(GameObject gameObject, Tuple<float, float> locationOnTable)
    //{
    //    RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
    //    if (rectTransform == null)
    //    {
    //        Debug.LogError($"GameObject {gameObject.name} does not have a RectTransform component");
    //        return;
    //    }
    //    float xOnUI = locationOnTable.Item1 * UIConstants.CanvasScaleFactor;
    //    float yOnUI = locationOnTable.Item2 * UIConstants.CanvasScaleFactor;
    //    rectTransform.anchoredPosition = new Vector2(xOnUI, yOnUI);
    //}

    //private GameObject spawnCard(CardData cardData)
    //{
    //    GameObject cardObject = Instantiate(cardPrefab);
    //    gameInstanceState.CardObjects.Add(cardObject);
    //    CardState cardState = cardObject.GetComponent<CardState>();
    //    cardState.Initialize(cardData);
    //    return cardObject;
    //}

    //private void spawnPlayerHands()
    //{
    //    // TODO implement
    //    GameObject playerHand = Instantiate(playerHandPrefab, playerHandContainer);
    //    gameInstanceState.PlayerHandObjects.Add(playerHand);
    //}
}
