using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public static class PrefabExtensions
{
    public static CardDisplay InstantiateCardDisplay(this GameObject prefab, Card card, Transform parent)
    {
        CardDisplay cardDisplay = GameObject.Instantiate(prefab, parent).GetComponent<CardDisplay>();
        cardDisplay.Setup(card);
        return cardDisplay;
    }

    public static Table InstantiateTable(TableData tableData)
    {
        GameObject tableGameObject = GameObject.Instantiate(PrefabReferences.Instance.TablePrefab);
        NetworkServer.Spawn(tableGameObject);
        Table table = tableGameObject.GetComponent<Table>();
        table.Setup(tableData);
        return table;
    }

    public static TableDisplay InstantiateTableDisplay(Table table)
    {
        Transform parent = ContainerReferences.Instance.TableContainer;
        TableDisplay tableDisplay = GameObject.Instantiate(PrefabReferences.Instance.TableDisplayPrefab, parent)
            .GetComponent<TableDisplay>();
        tableDisplay.Setup(table);
        return tableDisplay;
    }

    public static Deck InstantiateDeck(DeckData deckData, GameTemplate gameTemplate)
    {
        GameObject deckGameObject = GameObject.Instantiate(PrefabReferences.Instance.DeckPrefab);
        NetworkServer.Spawn(deckGameObject);
        Deck deck = deckGameObject.GetComponent<Deck>();
        deck.Setup(deckData, gameTemplate);
        return deck;
    }

    public static DeckDisplay InstantiateDeckDisplay(Deck deck)
    {
        Transform parent = ContainerReferences.Instance.TableObjectsContainer;
        DeckDisplay deckDisplay = GameObject.Instantiate(PrefabReferences.Instance.DeckDisplayPrefab, parent)
            .GetComponent<DeckDisplay>();
        deckDisplay.Setup(deck);
        return deckDisplay;
    }

    public static Space InstantiateSpace(SpaceData spaceData)
    {
        GameObject spaceGameObject = GameObject.Instantiate(PrefabReferences.Instance.SpacePrefab);
        NetworkServer.Spawn(spaceGameObject);
        Space space = spaceGameObject.GetComponent<Space>();
        space.Setup(spaceData);
        return space;
    }
    
    public static SpaceDisplay InstantiateSpaceDisplay(Space space)
    {
        Transform parent = ContainerReferences.Instance.TableObjectsContainer;
        SpaceDisplay spaceDisplay = GameObject.Instantiate(PrefabReferences.Instance.SpaceDisplayPrefab, parent)
            .GetComponent<SpaceDisplay>();
        spaceDisplay.Setup(space);
        return spaceDisplay;
    }

    public static Player InstantiatePlayer(NetworkConnectionToClient clientConnection)
    {
        const string defaultPlayerName = "Player";
        Player player = GameObject.Instantiate(PrefabReferences.Instance.PlayerPrefab).GetComponent<Player>();
        player.Setup(clientConnection.connectionId, defaultPlayerName);
        NetworkServer.AddPlayerForConnection(clientConnection, player.gameObject);
        return player;
    }

    public static PlayerHandDisplay InstantiatePlayerHandDisplay(PlayerHand playerHand)
    {
        Transform parent = ContainerReferences.Instance.PlayerHandContainer;
        PlayerHandDisplay playerHandDisplay = GameObject
            .Instantiate(PrefabReferences.Instance.PlayerHandDisplayPrefab, parent).GetComponent<PlayerHandDisplay>();
        playerHandDisplay.Setup(playerHand);
        return playerHandDisplay;
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

    public static GameTemplateSelectionEntity InstantiateGameTemplateSelectionEntity(
        GameTemplate gameTemplate,
        Action<GameTemplate> onSelectAction,
        Transform parent)
    {
        GameTemplateSelectionEntity gameTemplateSelectionEntity = GameObject
            .Instantiate(PrefabReferences.Instance.GameTemplateSelectionEntityPrefab, parent)
            .GetComponent<GameTemplateSelectionEntity>();
        gameTemplateSelectionEntity.Setup(gameTemplate, onSelectAction);
        return gameTemplateSelectionEntity;
    }

    public static CardSelectionEntity InstantiateCardSelectionEntity(
        CardData cardData,
        Action<CardData> onSelectCard,
        Transform parent)
    {
        CardSelectionEntity cardSelectionEntity = GameObject
            .Instantiate(PrefabReferences.Instance.CardSelectionEntityPrefab, parent)
            .GetComponent<CardSelectionEntity>();
        cardSelectionEntity.Setup(cardData, onSelectCard);
        return cardSelectionEntity;
    }
    
    public static CardSelectionEntity InstantiateCardSelectionEntity(
        Card card,
        Action<Card> onSelectCard,
        Transform parent)
    {
        CardSelectionEntity cardSelectionEntity = GameObject
            .Instantiate(PrefabReferences.Instance.CardSelectionEntityPrefab, parent)
            .GetComponent<CardSelectionEntity>();
        cardSelectionEntity.Setup(card, onSelectCard);
        return cardSelectionEntity;
    }

    public static DeckSelectionEntity InstantiateDeckSelectionEntity(
        DeckData deckData,
        Action<DeckData> onSelectAction,
        Transform parent)
    {
        DeckSelectionEntity deckSelectionEntity = GameObject
            .Instantiate(PrefabReferences.Instance.DeckSelectionEntityPrefab, parent)
            .GetComponent<DeckSelectionEntity>();
        deckSelectionEntity.Setup(deckData, onSelectAction);
        return deckSelectionEntity;
    }

    public static SpaceSelectionEntity InstantiateSpaceSelectionEntity(
        SpaceData spaceData,
        Action<SpaceData> onSelectAction,
        Transform parent)
    {
        SpaceSelectionEntity spaceSelectionEntity = GameObject
            .Instantiate(PrefabReferences.Instance.SpaceSelectionEntityPrefab, parent)
            .GetComponent<SpaceSelectionEntity>();
        spaceSelectionEntity.Setup(spaceData, onSelectAction);
        return spaceSelectionEntity;
    }

    public static CardSelectionModalWindow InstantiateCardSelectionModalWindow(
        string titleText,
        IEnumerable<CardData> cardsData,
        Action<CardData> onSelectCard,
        Action onAddButtonSelect,
        Action onBackButtonSelect)
    {
        CardSelectionModalWindow cardSelectionModalWindow = GameObject
            .Instantiate(PrefabReferences.Instance.CardSelectionModalWindowPrefab)
            .GetComponent<CardSelectionModalWindow>();
        cardSelectionModalWindow.transform.SetAsLastSibling();
        cardSelectionModalWindow.Setup(titleText, cardsData, onSelectCard, onAddButtonSelect, onBackButtonSelect);
        return cardSelectionModalWindow;
    }
    
    public static CardSelectionModalWindow InstantiateCardSelectionModalWindow(
        string titleText,
        IEnumerable<Card> cards,
        Action<Card> onSelectCard,
        Action onAddButtonSelect,
        Action onBackButtonSelect)
    {
        CardSelectionModalWindow cardSelectionModalWindow = GameObject
            .Instantiate(PrefabReferences.Instance.CardSelectionModalWindowPrefab)
            .GetComponent<CardSelectionModalWindow>();
        cardSelectionModalWindow.transform.SetAsLastSibling();
        cardSelectionModalWindow.Setup(titleText, cards, onSelectCard, onAddButtonSelect, onBackButtonSelect);
        return cardSelectionModalWindow;
    }

    public static GameTemplateSelectionModalWindow InstantiateGameTemplateSelectionModalWindow(
        string titleText,
        Action<GameTemplate> onSelectGameTemplate,
        Action onAddButtonSelect,
        Action onBackButtonSelect)
    {
        GameTemplateSelectionModalWindow gameTemplateSelectionModalWindow = GameObject
            .Instantiate(PrefabReferences.Instance.GameTemplateSelectionModalWindowPrefab)
            .GetComponent<GameTemplateSelectionModalWindow>();
        gameTemplateSelectionModalWindow.transform.SetAsLastSibling();
        gameTemplateSelectionModalWindow.Setup(titleText, onSelectGameTemplate, onAddButtonSelect, onBackButtonSelect);
        return gameTemplateSelectionModalWindow;
    }

    public static DeckSelectionModalWindow InstantiateDeckSelectionModalWindow(
        string titleText,
        IEnumerable<DeckData> decksData,
        Action<DeckData> onSelectDeck,
        Action onAddButtonSelect,
        Action onBackButtonSelect)
    {
        DeckSelectionModalWindow deckSelectionModalWindow = GameObject
            .Instantiate(PrefabReferences.Instance.DeckSelectionModalWindowPrefab)
            .GetComponent<DeckSelectionModalWindow>();
        deckSelectionModalWindow.transform.SetAsLastSibling();
        deckSelectionModalWindow.Setup(titleText, decksData, onSelectDeck, onAddButtonSelect, onBackButtonSelect);
        return deckSelectionModalWindow;
    }

    public static SpaceSelectionModalWindow InstantiateSpaceSelectionModalWindow(
        string titleText,
        IEnumerable<SpaceData> spacesData,
        Action<SpaceData> onSelectSpace,
        Action onAddButtonSelect,
        Action onBackButtonSelect)
    {
        SpaceSelectionModalWindow spaceSelectionModalWindow = GameObject
            .Instantiate(PrefabReferences.Instance.SpaceSelectionModalWindowPrefab)
            .GetComponent<SpaceSelectionModalWindow>();
        spaceSelectionModalWindow.transform.SetAsLastSibling();
        spaceSelectionModalWindow.Setup(titleText, spacesData, onSelectSpace, onAddButtonSelect, onBackButtonSelect);
        return spaceSelectionModalWindow;
    }

    public static MobileImageMethodModalWindow InstantiateMobileImageMethodModalWindow(
        Action<Texture2D> onImageLoaded,
        Action onCancel,
        Action onBackButtonSelect)
    {
        MobileImageMethodModalWindow mobileImageMethodModalWindow = GameObject
            .Instantiate(PrefabReferences.Instance.MobileImageMethodModalWindowPrefab)
            .GetComponent<MobileImageMethodModalWindow>();
        mobileImageMethodModalWindow.transform.SetAsLastSibling();
        mobileImageMethodModalWindow.Setup(onImageLoaded, onCancel, onBackButtonSelect);
        return mobileImageMethodModalWindow;
    }
}