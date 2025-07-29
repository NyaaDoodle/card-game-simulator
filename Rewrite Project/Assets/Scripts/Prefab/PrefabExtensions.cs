using UnityEngine;

public static class PrefabExtensions
{
    public static CardTableDisplay InstantiateCardTableDisplay(this GameObject prefab,
                                                          CardState cardState,
                                                          Transform parent)
    {
        CardTableDisplay cardDisplay = GameObject.Instantiate(prefab, parent).GetComponent<CardTableDisplay>();
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
}