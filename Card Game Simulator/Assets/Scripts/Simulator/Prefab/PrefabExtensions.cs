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

    public static Table InstantiateTable(this GameObject prefab, TableData tableData)
    {
        GameObject tableGameObject = GameObject.Instantiate(prefab);
        NetworkServer.Spawn(tableGameObject);
        Table table = tableGameObject.GetComponent<Table>();
        table.Setup(tableData);
        tableGameObject.GetComponent<TableDisplay>().Setup(table);
        return table;
    }

    public static Deck InstantiateDeck(this GameObject prefab, DeckData deckData)
    {
        GameObject deckGameObject = GameObject.Instantiate(prefab);
        NetworkServer.Spawn(deckGameObject);
        Deck deck = deckGameObject.GetComponent<Deck>();
        deck.Setup(deckData);
        deckGameObject.GetComponent<DeckDisplay>().Setup(deck);
        return deck;
    }

    public static Space InstantiateSpace(this GameObject prefab, SpaceData spaceData)
    {
        GameObject spaceGameObject = GameObject.Instantiate(prefab);
        NetworkServer.Spawn(spaceGameObject);
        Space space = spaceGameObject.GetComponent<Space>();
        space.Setup(spaceData);
        spaceGameObject.GetComponent<SpaceDisplay>().Setup(space);
        return space;
    }

    public static CardSelectionMenu InstantiateCardSelectionMenu(this GameObject prefab, CardCollection cardCollection)
    {
        Transform parent = ContainerReferences.Instance.InteractionMenuItemsContainer;
        CardSelectionMenu cardSelectionMenu = GameObject.Instantiate(prefab, parent).GetComponent<CardSelectionMenu>();
        cardSelectionMenu.Setup(cardCollection);
        return cardSelectionMenu;
    }

    public static DeckMenu InstantiateDeckMenu(this GameObject prefab, Deck deck)
    {
        Transform parent = ContainerReferences.Instance.InteractionMenuItemsContainer;
        DeckMenu deckMenu = GameObject.Instantiate(prefab, parent).GetComponent<DeckMenu>();
        deckMenu.Setup(deck);
        return deckMenu;
    }

    public static InstanceMenu InstantiateInstanceMenu(this GameObject prefab)
    {
        Transform parent = ContainerReferences.Instance.InteractionMenuItemsContainer;
        InstanceMenu instanceMenu = GameObject.Instantiate(prefab, parent).GetComponent<InstanceMenu>();
        return instanceMenu;
    }

    public static PlacingCardMenu InstantiatePlacingCardMenu(this GameObject prefab)
    {
        Transform parent = ContainerReferences.Instance.InteractionMenuItemsContainer;
        PlacingCardMenu placingCardMenu = GameObject.Instantiate(prefab, parent).GetComponent<PlacingCardMenu>();
        return placingCardMenu;
    }

    public static SpaceMenu InstantiateSpaceMenu(this GameObject prefab, Space space)
    {
        Transform parent = ContainerReferences.Instance.InteractionMenuItemsContainer;
        SpaceMenu spaceMenu = GameObject.Instantiate(prefab, parent).GetComponent<SpaceMenu>();
        spaceMenu.Setup(space);
        return spaceMenu;
    }
}