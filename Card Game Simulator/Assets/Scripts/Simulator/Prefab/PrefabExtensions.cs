using UnityEngine;

public static class PrefabExtensions
{
    public static CardDisplay InstantiateCardDisplay(this GameObject prefab,
                                                          Card card,
                                                          Transform parent)
    {
        CardDisplay cardDisplay = GameObject.Instantiate(prefab, parent).GetComponent<CardDisplay>();
        cardDisplay.Setup(card);
        return cardDisplay;
    }

    public static DeckDisplay InstantiateDeckDisplay(this GameObject prefab,
                                                               Deck deck,
                                                               Transform parent)
    {
        DeckDisplay deckDisplay = GameObject.Instantiate(prefab, parent).GetComponent<DeckDisplay>();
        deckDisplay.Setup(deck);
        return deckDisplay;
    }

    public static SpaceDisplay InstantiateSpaceDisplay(this GameObject prefab,
                                                     Space space,
                                                     Transform parent)
    {
        SpaceDisplay spaceDisplay = GameObject.Instantiate(prefab, parent).GetComponent<SpaceDisplay>();
        spaceDisplay.Setup(space);
        return spaceDisplay;
    }

    public static PlayerHandDisplay InstantiatePlayerHandDisplay(this GameObject prefab,
                                                       PlayerHand playerHand,
                                                       Transform parent)
    {
        PlayerHandDisplay playerHandDisplay = GameObject.Instantiate(prefab, parent).GetComponent<PlayerHandDisplay>();
        playerHandDisplay.Setup(playerHand);
        return playerHandDisplay;
    }

    public static TableDisplay InstantiateTableDisplay(this GameObject prefab,
                                                                 Table table,
                                                                 Transform parent)
    {
        TableDisplay tableDisplay = GameObject.Instantiate(prefab, parent).GetComponent<TableDisplay>();
        tableDisplay.Setup(table);
        return tableDisplay;
    }
}