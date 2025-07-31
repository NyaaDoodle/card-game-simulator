using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHandSpawnScript : MonoBehaviour
{
    [SerializeField] private GameObject playerHandPrefab;
    [SerializeField] private GameObject playerHandContainer;
    [SerializeField] private DeckAndSpaceSpawnScript deckAndSpaceSpawnScript;
    [SerializeField] private Button drawDeckButton;
    [SerializeField] private Button drawSpaceButton;
    [SerializeField] private Button flipDeckButton;
    [SerializeField] private Button flipSpaceButton;
    [SerializeField] private Button placeOnDeckButton;
    [SerializeField] private Button placeOnSpaceButton;
    [SerializeField] private Button shuffleDeckButton;
    [SerializeField] private Button shuffleSpaceButton;
    [SerializeField] private Button putMoreButton;
    [SerializeField] private Button randomDeckButton;

    private PlayerHand playerHandState;
    private PlayerHandDisplay playerHandDisplay;

    void Start()
    {
        spawnTestPlayer();
        setupButtons();
    }

    private void spawnTestPlayer()
    {
        playerHandState = new PlayerHand();
        playerHandDisplay =
            playerHandPrefab.InstantiatePlayerHandDisplay(playerHandState, playerHandContainer.transform);
    }

    private void setupButtons()
    {
        Deck deckState = deckAndSpaceSpawnScript.DeckState;
        Space spaceState = deckAndSpaceSpawnScript.SpaceState;
        List<CardData> cardsA = deckAndSpaceSpawnScript.CardsA;
        List<Card> cardStatesA = new List<Card>();
        foreach (CardData cardData in cardsA)
        {
            cardStatesA.Add(new Card(cardData));
        }
        List<CardData> cardsB = deckAndSpaceSpawnScript.CardsA;
        List<Card> cardStatesB = new List<Card>();
        foreach (CardData cardData in cardsB)
        {
            cardStatesB.Add(new Card(cardData));
        }
        drawDeckButton.onClick.AddListener(
            () =>
                {
                    Card drawnCard = deckState.DrawCard();
                    if (drawnCard != null)
                    {
                        playerHandState.AddCard(drawnCard);
                    }
                });
        drawSpaceButton.onClick.AddListener(
            () =>
                {
                    Card drawnCard = spaceState.DrawCard();
                    if (drawnCard != null)
                    {
                        playerHandState.AddCard(drawnCard);
                    }
                });
        flipDeckButton.onClick.AddListener(
            () =>
                {
                    deckState.FlipTopCard();
                });
        flipSpaceButton.onClick.AddListener(
            () =>
                {
                    spaceState.FlipTopCard();
                });
        placeOnDeckButton.onClick.AddListener(
            () =>
                {
                    Card takenCard = playerHandState.RemoveCardAtEnd();
                    if (takenCard != null)
                    {
                        deckState.AddCardToTop(takenCard);
                    }
                });
        placeOnSpaceButton.onClick.AddListener(
            () =>
                {
                    Card takenCard = playerHandState.RemoveCardAtEnd();
                    if (takenCard != null)
                    {
                        spaceState.AddCardToTop(takenCard);
                    }
                });
        shuffleDeckButton.onClick.AddListener(
            () =>
                {
                    deckState.Shuffle();
                });
        shuffleSpaceButton.onClick.AddListener(
            () =>
                {
                    spaceState.Shuffle();
                });
        putMoreButton.onClick.AddListener(
            () =>
                {
                    deckState.AddCards(cardStatesB);
                    spaceState.AddCards(cardStatesA);
                });
        randomDeckButton.onClick.AddListener(
            () =>
                {
                    Card takenCard = deckState.RemoveCard(Random.Range(0, deckState.Cards.Count));
                    if (takenCard != null)
                    {
                        playerHandState.AddCard(takenCard);
                    }
                });
    }
}
