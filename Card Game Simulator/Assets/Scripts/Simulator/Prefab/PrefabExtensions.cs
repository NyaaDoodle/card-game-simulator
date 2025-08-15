using Mirror;
using UnityEngine;

public static class PrefabExtensions
{
    public static CardDisplay InstantiateCardDisplay(this GameObject prefab,
                                                          Card card,
                                                          Transform parent)
    {
        LoggerReferences.Instance.PrefabExtensionsLogger.LogMethod();
        CardDisplay cardDisplay = GameObject.Instantiate(prefab, parent).GetComponent<CardDisplay>();
        cardDisplay.Setup(card);
        return cardDisplay;
    }

    public static Table InstantiateTable(this GameObject prefab, TableData tableData)
    {
        LoggerReferences.Instance.PrefabExtensionsLogger.LogMethod();
        GameObject tableGameObject = GameObject.Instantiate(prefab);
        NetworkServer.Spawn(tableGameObject);
        Table table = tableGameObject.GetComponent<Table>();
        table.Setup(tableData);
        return table;
    }

    public static TableDisplay InstantiateTableDisplay(this GameObject prefab, Table table)
    {
        LoggerReferences.Instance.PrefabExtensionsLogger.LogMethod();
        Transform parent = ContainerReferences.Instance.TableContainer;
        TableDisplay tableDisplay = GameObject.Instantiate(prefab, parent).GetComponent<TableDisplay>();
        tableDisplay.Setup(table);
        return tableDisplay;
    }

    public static Deck InstantiateDeck(this GameObject prefab, DeckData deckData)
    {
        LoggerReferences.Instance.PrefabExtensionsLogger.LogMethod();
        GameObject deckGameObject = GameObject.Instantiate(prefab);
        NetworkServer.Spawn(deckGameObject);
        Deck deck = deckGameObject.GetComponent<Deck>();
        deck.Setup(deckData);
        return deck;
    }

    public static Space InstantiateSpace(this GameObject prefab, SpaceData spaceData)
    {
        LoggerReferences.Instance.PrefabExtensionsLogger.LogMethod();
        GameObject spaceGameObject = GameObject.Instantiate(prefab);
        NetworkServer.Spawn(spaceGameObject);
        Space space = spaceGameObject.GetComponent<Space>();
        space.Setup(spaceData);
        return space;
    }

    public static PlayerHandDisplay InstantiatePlayerHandDisplay(this GameObject prefab, PlayerHand playerHand)
    {
        LoggerReferences.Instance.PrefabExtensionsLogger.LogMethod();
        Transform parent = ContainerReferences.Instance.PlayerHandContainer;
        PlayerHandDisplay playerHandDisplay = GameObject.Instantiate(prefab, parent).GetComponent<PlayerHandDisplay>();
        //playerHandDisplay.Setup(playerHand);
        return playerHandDisplay;
    }

    public static CardSelectionMenu InstantiateCardSelectionMenu(this GameObject prefab, CardCollection cardCollection)
    {
        LoggerReferences.Instance.PrefabExtensionsLogger.LogMethod();
        Transform parent = ContainerReferences.Instance.InteractionMenuItemsContainer;
        CardSelectionMenu cardSelectionMenu = GameObject.Instantiate(prefab, parent).GetComponent<CardSelectionMenu>();
        cardSelectionMenu.Setup(cardCollection);
        return cardSelectionMenu;
    }

    public static DeckMenu InstantiateDeckMenu(this GameObject prefab, Deck deck)
    {
        LoggerReferences.Instance.PrefabExtensionsLogger.LogMethod();
        Transform parent = ContainerReferences.Instance.InteractionMenuItemsContainer;
        DeckMenu deckMenu = GameObject.Instantiate(prefab, parent).GetComponent<DeckMenu>();
        deckMenu.Setup(deck);
        return deckMenu;
    }

    public static InstanceMenu InstantiateInstanceMenu(this GameObject prefab)
    {
        LoggerReferences.Instance.PrefabExtensionsLogger.LogMethod();
        Transform parent = ContainerReferences.Instance.InteractionMenuItemsContainer;
        InstanceMenu instanceMenu = GameObject.Instantiate(prefab, parent).GetComponent<InstanceMenu>();
        return instanceMenu;
    }

    public static PlacingCardMenu InstantiatePlacingCardMenu(this GameObject prefab)
    {
        LoggerReferences.Instance.PrefabExtensionsLogger.LogMethod();
        Transform parent = ContainerReferences.Instance.InteractionMenuItemsContainer;
        PlacingCardMenu placingCardMenu = GameObject.Instantiate(prefab, parent).GetComponent<PlacingCardMenu>();
        return placingCardMenu;
    }

    public static SpaceMenu InstantiateSpaceMenu(this GameObject prefab, Space space)
    {
        LoggerReferences.Instance.PrefabExtensionsLogger.LogMethod();
        Transform parent = ContainerReferences.Instance.InteractionMenuItemsContainer;
        SpaceMenu spaceMenu = GameObject.Instantiate(prefab, parent).GetComponent<SpaceMenu>();
        spaceMenu.Setup(space);
        return spaceMenu;
    }
}