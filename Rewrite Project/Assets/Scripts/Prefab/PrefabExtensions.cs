using UnityEngine;

public static class PrefabExtensions
{
    public static CardDisplay InstantiateCardDisplay(this GameObject prefab,
                                                          CardState cardState,
                                                          Transform parent)
    {
        CardDisplay cardDisplay = GameObject.Instantiate(prefab, parent).GetComponent<CardDisplay>();
        cardDisplay.Setup(cardState);
        return cardDisplay;
    }

    public static DeckDisplay InstantiateDeckDisplay(this GameObject prefab,
                                                               DeckState deckState,
                                                               Transform parent)
    {
        DeckDisplay deckDisplay = GameObject.Instantiate(prefab, parent).GetComponent<DeckDisplay>();
        deckDisplay.Setup(deckState);
        return deckDisplay;
    }

    public static SpaceDisplay InstantiateSpaceDisplay(this GameObject prefab,
                                                     SpaceState spaceState,
                                                     Transform parent)
    {
        SpaceDisplay spaceDisplay = GameObject.Instantiate(prefab, parent).GetComponent<SpaceDisplay>();
        spaceDisplay.Setup(spaceState);
        return spaceDisplay;
    }

    public static PlayerHandDisplay InstantiatePlayerHandDisplay(this GameObject prefab,
                                                       PlayerHandState playerHandState,
                                                       Transform parent)
    {
        PlayerHandDisplay playerHandDisplay = GameObject.Instantiate(prefab, parent).GetComponent<PlayerHandDisplay>();
        playerHandDisplay.Setup(playerHandState);
        return playerHandDisplay;
    }
}