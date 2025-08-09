using Mirror;
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
        NetworkServer.Spawn(deckDisplay.gameObject);
        deckDisplay.Setup(deck);
        return deckDisplay;
    }

    public static SpaceDisplay InstantiateSpaceDisplay(this GameObject prefab,
                                                     Space space,
                                                     Transform parent)
    {
        SpaceDisplay spaceDisplay = GameObject.Instantiate(prefab, parent).GetComponent<SpaceDisplay>();
        NetworkServer.Spawn(spaceDisplay.gameObject);
        spaceDisplay.Setup(space);
        return spaceDisplay;
    }

    public static PlayerHandDisplay InstantiatePlayerHandDisplay(this GameObject prefab,
                                                       Player player,
                                                       Transform parent)
    {
        PlayerHandDisplay playerHandDisplay = GameObject.Instantiate(prefab, parent).GetComponent<PlayerHandDisplay>();
        playerHandDisplay.Setup(player);
        return playerHandDisplay;
    }

    public static CardSelectionMenu InstantiateCardSelectionMenu(this GameObject prefab,
                                                                 CardCollection cardCollection,
                                                                 Transform parent)
    {
        CardSelectionMenu cardSelectionMenu = GameObject.Instantiate(prefab, parent).GetComponent<CardSelectionMenu>();
        cardSelectionMenu.Setup(cardCollection);
        return cardSelectionMenu;
    }

    public static TableDisplay InstantiateTableDisplay(this GameObject prefab, Table table)
    {
        TableDisplay tableDisplay = GameObject.Instantiate(prefab).GetComponent<TableDisplay>();
        NetworkServer.Spawn(tableDisplay.gameObject);
        tableDisplay.Setup(table);
        return tableDisplay;
    }

    public static DeckMenu InstantiateDeckMenu(this GameObject prefab,
                                                       Deck deck,
                                                       Transform parent)
    {
        DeckMenu deckMenu = GameObject.Instantiate(prefab, parent).GetComponent<DeckMenu>();
        deckMenu.Setup(deck);
        return deckMenu;
    }

    public static InstanceMenu InstantiateInstanceMenu(this GameObject prefab,
                                                       Transform parent)
    {
        InstanceMenu instanceMenu = GameObject.Instantiate(prefab, parent).GetComponent<InstanceMenu>();
        return instanceMenu;
    }

    public static PlacingCardMenu InstantiatePlacingCardMenu(this GameObject prefab,
                                                             Transform parent)
    {
        PlacingCardMenu placingCardMenu = GameObject.Instantiate(prefab, parent).GetComponent<PlacingCardMenu>();
        return placingCardMenu;
    }

    public static SpaceMenu InstantiateSpaceMenu(this GameObject prefab,
                                                       Space space,
                                                       Transform parent)
    {
        SpaceMenu spaceMenu = GameObject.Instantiate(prefab, parent).GetComponent<SpaceMenu>();
        spaceMenu.Setup(space);
        return spaceMenu;
    }
}