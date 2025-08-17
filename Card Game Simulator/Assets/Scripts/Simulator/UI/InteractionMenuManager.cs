using System;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InteractionMenuManager : MonoBehaviour
{
    [SerializeField] private RectTransform interactionMenuObject;
    [SerializeField] private RectTransform interactionMenuItemsContainer;
    [SerializeField] private Button cancelSelectionArea;
    [SerializeField] private GameObject deckMenuItemsPrefab;
    [SerializeField] private GameObject spaceMenuItemsPrefab;
    [SerializeField] private GameObject cardSelectionMenuItemsPrefab;
    [SerializeField] private GameObject placingCardMenuItemsPrefab;
    [SerializeField] private GameObject instanceMenuItemsPrefab;

    private GameObject currentMenuItemsObject = null;

    // Player Action Events
    public event Action Cancelled;
    public event Action SelectedDrawCard;
    public event Action SelectedFlipCard;
    public event Action SelectedShuffle;
    public event Action SelectedSearch;
    public event Action SelectedPlaceCard;
    public event Action SelectedFaceUp;
    public event Action SelectedFaceDown;

    void Start()
    {
        LoggingManager.Instance.InteractionMenuManagerLogger.LogMethod();
        cancelSelectionArea.onClick.AddListener(OnCancelled);
    }

    public void ShowDeckActionMenu(Deck deck)
    {
        LoggingManager.Instance.InteractionMenuManagerLogger.LogMethod();
        destroyCurrentMenuItemsObject();
        showInteractionMenu();
        DeckMenu deckMenu = deckMenuItemsPrefab.InstantiateDeckMenu(deck);
        currentMenuItemsObject = deckMenu.gameObject;
        addDeckMenuItemListeners(deckMenu);
    }

    public void ShowSpaceActionMenu(Space space)
    {
        LoggingManager.Instance.InteractionMenuManagerLogger.LogMethod();
        destroyCurrentMenuItemsObject();
        showInteractionMenu();
        SpaceMenu spaceMenu = spaceMenuItemsPrefab.InstantiateSpaceMenu(space);
        currentMenuItemsObject = spaceMenu.gameObject;
        addSpaceMenuItemListeners(spaceMenu);
    }

    public void ShowCardSelectionActionMenu(CardCollection sourceCardCollection)
    {
        LoggingManager.Instance.InteractionMenuManagerLogger.LogMethod();
        destroyCurrentMenuItemsObject();
        showInteractionMenu();
        CardSelectionMenu cardSelectionMenu =
            cardSelectionMenuItemsPrefab.InstantiateCardSelectionMenu(sourceCardCollection);
        currentMenuItemsObject = cardSelectionMenu.gameObject;
        addCardSelectionMenuItemListeners(cardSelectionMenu);
    }

    public void ShowPlacingCardMenuItems()
    {
        LoggingManager.Instance.InteractionMenuManagerLogger.LogMethod();
        destroyCurrentMenuItemsObject();
        showInteractionMenu();
        PlacingCardMenu placingCardMenu =
            placingCardMenuItemsPrefab.InstantiatePlacingCardMenu();
        currentMenuItemsObject = placingCardMenu.gameObject;
        addPlacingCardMenuItemListeners(placingCardMenu);
    }

    public void ShowInstanceMenu()
    {
        LoggingManager.Instance.InteractionMenuManagerLogger.LogMethod();
        destroyCurrentMenuItemsObject();
        showInteractionMenu();
        InstanceMenu instanceMenu = instanceMenuItemsPrefab.InstantiateInstanceMenu();
        currentMenuItemsObject = instanceMenu.gameObject;
        addInstanceMenuItemListeners(instanceMenu);
    }

    public void CloseMenu()
    {
        LoggingManager.Instance.InteractionMenuManagerLogger.LogMethod();
        hideInteractionMenu();
        destroyCurrentMenuItemsObject();
    }

    protected virtual void OnCancelled()
    {
        LoggingManager.Instance.InteractionMenuManagerLogger.LogMethod();
        if (currentMenuItemsObject == null) return;
        Cancelled?.Invoke();
        CloseMenu();
    }

    protected virtual void OnSelectedDrawCard()
    {
        LoggingManager.Instance.InteractionMenuManagerLogger.LogMethod();
        if (currentMenuItemsObject == null) return;
        SelectedDrawCard?.Invoke();
        CloseMenu();
    }

    protected virtual void OnSelectedFlipCard()
    {
        LoggingManager.Instance.InteractionMenuManagerLogger.LogMethod();
        if (currentMenuItemsObject == null) return;
        SelectedFlipCard?.Invoke();
        CloseMenu();
    }

    protected virtual void OnSelectedShuffle()
    {
        LoggingManager.Instance.InteractionMenuManagerLogger.LogMethod();
        if (currentMenuItemsObject == null) return;
        SelectedShuffle?.Invoke();
        CloseMenu();
    }

    protected virtual void OnSelectedSearch()
    {
        LoggingManager.Instance.InteractionMenuManagerLogger.LogMethod();
        if (currentMenuItemsObject == null) return;
        SelectedSearch?.Invoke();
        CloseMenu();
    }

    protected virtual void OnSelectedPlaceCard()
    {
        LoggingManager.Instance.InteractionMenuManagerLogger.LogMethod();
        if (currentMenuItemsObject == null) return;
        SelectedPlaceCard?.Invoke();
        CloseMenu();
    }

    protected virtual void OnSelectedFaceUp()
    {
        LoggingManager.Instance.InteractionMenuManagerLogger.LogMethod();
        if (currentMenuItemsObject == null) return;
        SelectedFaceUp?.Invoke();
        CloseMenu();
    }

    protected virtual void OnSelectedFaceDown()
    {
        LoggingManager.Instance.InteractionMenuManagerLogger.LogMethod();
        if (currentMenuItemsObject == null) return;
        SelectedFaceDown?.Invoke();
        CloseMenu();
    }

    private void addInstanceMenuItemListeners(InstanceMenu instanceMenu)
    {
        LoggingManager.Instance.InteractionMenuManagerLogger.LogMethod();
        instanceMenu.ManageScoresButton.onClick.AddListener(() => Debug.Log("Score management not implemented"));
        instanceMenu.LeaveGameButton.onClick.AddListener(returnToMainMenu);
        setCancelSelectionButton(instanceMenu.CancelSelectionButton);
    }

    private void addPlacingCardMenuItemListeners(PlacingCardMenu placingCardMenu)
    {
        LoggingManager.Instance.InteractionMenuManagerLogger.LogMethod();
        placingCardMenu.FaceUpButton.onClick.AddListener(OnSelectedFaceUp);
        placingCardMenu.FaceDownButton.onClick.AddListener(OnSelectedFaceDown);
        setCancelSelectionButton(placingCardMenu.CancelSelectionButton);
    }

    private void addCardSelectionMenuItemListeners(CardSelectionMenu cardSelectionMenu)
    {
        LoggingManager.Instance.InteractionMenuManagerLogger.LogMethod();
        cardSelectionMenu.PlaceCardButton.onClick.AddListener(OnSelectedPlaceCard);
        setCancelSelectionButton(cardSelectionMenu.CancelSelectionButton);
    }

    private void addSpaceMenuItemListeners(SpaceMenu spaceMenu)
    {
        LoggingManager.Instance.InteractionMenuManagerLogger.LogMethod();
        spaceMenu.DrawCardButton.onClick.AddListener(OnSelectedDrawCard);
        spaceMenu.FlipCardButton.onClick.AddListener(OnSelectedFlipCard);
        setCancelSelectionButton(spaceMenu.CancelSelectionButton);
    }

    private void addDeckMenuItemListeners(DeckMenu deckMenu)
    {
        LoggingManager.Instance.InteractionMenuManagerLogger.LogMethod();
        deckMenu.DrawCardButton.onClick.AddListener(OnSelectedDrawCard);
        deckMenu.FlipCardButton.onClick.AddListener(OnSelectedFlipCard);
        deckMenu.ShuffleDeckButton.onClick.AddListener(OnSelectedShuffle);
        deckMenu.SearchDeckButton.onClick.AddListener(OnSelectedSearch);
        setCancelSelectionButton(deckMenu.CancelSelectionButton);
    }

    private void destroyCurrentMenuItemsObject()
    {
        LoggingManager.Instance.InteractionMenuManagerLogger.LogMethod();
        if (currentMenuItemsObject != null)
        {
            Destroy(currentMenuItemsObject);
        }
        currentMenuItemsObject = null;
    }

    private void showInteractionMenu()
    {
        LoggingManager.Instance.InteractionMenuManagerLogger.LogMethod();
        interactionMenuObject.gameObject.SetActive(true);
    }

    private void hideInteractionMenu()
    {
        LoggingManager.Instance.InteractionMenuManagerLogger.LogMethod();
        interactionMenuObject.gameObject.SetActive(false);
    }

    private void setCancelSelectionButton(Button cancelSelectionButton)
    {
        LoggingManager.Instance.InteractionMenuManagerLogger.LogMethod();
        cancelSelectionButton.onClick.AddListener(OnCancelled);
    }

    private void returnToMainMenu()
    {
        LoggingManager.Instance.InteractionMenuManagerLogger.LogMethod();
        SimulatorNetworkManager.singleton.StopGame();
    }
}
