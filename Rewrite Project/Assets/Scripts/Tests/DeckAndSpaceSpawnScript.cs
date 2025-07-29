using System;
using System.Collections.Generic;
using UnityEngine;

public class DeckAndSpaceSpawnScript : MonoBehaviour
{
    [SerializeField] private GameObject deckPrefab;
    [SerializeField] private GameObject spacePrefab;
    [SerializeField] private GameObject container;

    void Start()
    {
        List<CardData> cardsA = getTestCardsA();
        DeckDisplay deckDisplay = spawnTestDeck(cardsA);
        SpaceDisplay spaceDisplay = spawnTestSpace();
    }

    private List<CardData> getTestCardsA()
    {
        List<CardData> cardsA = new List<CardData>();
        cardsA.Add(
            new CardData()
                {
                    Id = 0,
                    BackSideSpritePath = "Standard52/Gray_back",
                    FrontSideSpritePath = "Standard52/2C"
                });
        cardsA.Add(
            new CardData()
                {
                    Id = 1,
                    BackSideSpritePath = "Standard52/Green_back",
                    FrontSideSpritePath = "Standard52/3D"
                });
        cardsA.Add(
            new CardData()
                {
                    Id = 2,
                    BackSideSpritePath = "Standard52/Red_back",
                    FrontSideSpritePath = "Standard52/4H"
                });
        cardsA.Add(
            new CardData()
                {
                    Id = 3,
                    BackSideSpritePath = "Standard52/Yellow_back",
                    FrontSideSpritePath = "Standard52/5S"
                });
        return cardsA;
    }

    private List<CardData> getTestCardsB()
    {
        List<CardData> cardsB = new List<CardData>();
        cardsB.Add(
            new CardData()
                {
                    Id = 4,
                    BackSideSpritePath = "Standard52/blue_back",
                    FrontSideSpritePath = "Standard52/10S"
                });
        cardsB.Add(
            new CardData()
                {
                    Id = 5,
                    BackSideSpritePath = "Standard52/purple_back",
                    FrontSideSpritePath = "Standard52/9D"
                });
        cardsB.Add(
            new CardData()
                {
                    Id = 6,
                    BackSideSpritePath = "Standard52/Green_back",
                    FrontSideSpritePath = "Standard52/8C"
                });
        cardsB.Add(
            new CardData()
                {
                    Id = 7,
                    BackSideSpritePath = "Standard52/Red_back",
                    FrontSideSpritePath = "Standard52/7H"
                });
        return cardsB;
    }

    private DeckDisplay spawnTestDeck(List<CardData> cards)
    {
        DeckData deckData = new DeckData()
                                    {
                                        Id = 0, LocationOnTable = new Tuple<float, float>(-2, 0), Cards = cards
                                    };
        DeckState deckState = new DeckState(deckData);
        DeckDisplay deckDisplay = deckPrefab.InstantiateDeckDisplay(deckState, container.transform);
        return deckDisplay;
    }

    private SpaceDisplay spawnTestSpace()
    {
        SpaceData spaceData = new SpaceData() { Id = 0, LocationOnTable = new Tuple<float, float>(2, 0) };
        SpaceState spaceState = new SpaceState(spaceData);
        SpaceDisplay spaceDisplay = spacePrefab.InstantiateSpaceDisplay(spaceState, container.transform);
        return spaceDisplay;
    }
}
