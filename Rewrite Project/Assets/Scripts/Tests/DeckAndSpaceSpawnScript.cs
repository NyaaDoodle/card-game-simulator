using System;
using System.Collections.Generic;
using UnityEngine;

public class DeckAndSpaceSpawnScript : MonoBehaviour
{
    [SerializeField] private GameObject deckPrefab;
    [SerializeField] private GameObject spacePrefab;
    [SerializeField] private GameObject container;

    public Deck DeckState { get; private set; }
    public Space SpaceState { get; private set; }
    public List<CardData> CardsA { get; private set; }
    public List<CardData> CardsB { get; private set; }

    void Start()
    {
        makeTestCardsA();
        makeTestCardsB();
        DeckDisplay deckDisplay = spawnTestDeck(CardsA);
        SpaceDisplay spaceDisplay = spawnTestSpace();
    }

    private void makeTestCardsA()
    {
        CardsA = new List<CardData>();
        CardsA.Add(
            new CardData()
                {
                    Id = 0,
                    BackSideSpritePath = "Standard52/Gray_back",
                    FrontSideSpritePath = "Standard52/2C"
                });
        CardsA.Add(
            new CardData()
                {
                    Id = 1,
                    BackSideSpritePath = "Standard52/Green_back",
                    FrontSideSpritePath = "Standard52/3D"
                });
        CardsA.Add(
            new CardData()
                {
                    Id = 2,
                    BackSideSpritePath = "Standard52/Red_back",
                    FrontSideSpritePath = "Standard52/4H"
                });
        CardsA.Add(
            new CardData()
                {
                    Id = 3,
                    BackSideSpritePath = "Standard52/Yellow_back",
                    FrontSideSpritePath = "Standard52/5S"
                });
    }

    private void makeTestCardsB()
    {
        CardsB = new List<CardData>();
        CardsB.Add(
            new CardData()
                {
                    Id = 4,
                    BackSideSpritePath = "Standard52/blue_back",
                    FrontSideSpritePath = "Standard52/10S"
                });
        CardsB.Add(
            new CardData()
                {
                    Id = 5,
                    BackSideSpritePath = "Standard52/purple_back",
                    FrontSideSpritePath = "Standard52/9D"
                });
        CardsB.Add(
            new CardData()
                {
                    Id = 6,
                    BackSideSpritePath = "Standard52/Green_back",
                    FrontSideSpritePath = "Standard52/8C"
                });
        CardsB.Add(
            new CardData()
                {
                    Id = 7,
                    BackSideSpritePath = "Standard52/Red_back",
                    FrontSideSpritePath = "Standard52/7H"
                });
    }

    private DeckDisplay spawnTestDeck(List<CardData> cards)
    {
        DeckData deckData = new DeckData()
                                    {
                                        Id = 0, LocationOnTable = new Tuple<float, float>(-2, 0), Cards = cards
                                    };
        DeckState = new Deck(deckData);
        DeckDisplay deckDisplay = deckPrefab.InstantiateDeckDisplay(DeckState, container.transform);
        return deckDisplay;
    }

    private SpaceDisplay spawnTestSpace()
    {
        SpaceData spaceData = new SpaceData() { Id = 0, LocationOnTable = new Tuple<float, float>(2, 0) };
        SpaceState = new Space(spaceData);
        SpaceDisplay spaceDisplay = spacePrefab.InstantiateSpaceDisplay(SpaceState, container.transform);
        return spaceDisplay;
    }
}
