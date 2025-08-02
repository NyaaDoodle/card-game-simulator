using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private RectTransform interactionMenuObject;
    [SerializeField] private RectTransform interactionMenuItemsContainer;
    [SerializeField] private GameObject deckMenuItemsPrefab;
    [SerializeField] private GameObject spaceMenuItemsPrefab;
    [SerializeField] private GameObject CardSelectionMenuItemsPrefab;
    [SerializeField] private GameObject PlacingCardMenuItemsPrefab;
    [SerializeField] private GameObject mainMenuItemsPrefab;

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

    public void ShowDeckActionMenu(Deck deck)
    {
        destroyCurrentMenuItemsObject();
        showInteractionMenu();
    }

    public void ShowSpaceActionMenu(Space space)
    {
        destroyCurrentMenuItemsObject();
        showInteractionMenu();
    }

    public void ShowCardSelectionActionMenu(CardCollection sourceCardCollection)
    {
        destroyCurrentMenuItemsObject();
        showInteractionMenu();
    }

    public void ShowPlacingCardMenuItems(Card card)
    {
        destroyCurrentMenuItemsObject();
        showInteractionMenu();
    }

    public void ShowInstanceMenu()
    {
        destroyCurrentMenuItemsObject();
        showInteractionMenu();
        currentMenuItemsObject = Instantiate(mainMenuItemsPrefab, interactionMenuItemsContainer);
        addInstanceMenuItemListeners();
    }

    public void CloseMenu()
    {
        hideInteractionMenu();
        destroyCurrentMenuItemsObject();
    }

    protected virtual void OnCancelled()
    {
        Cancelled?.Invoke();
        CloseMenu();
    }

    protected virtual void OnSelectedDrawCard()
    {
        SelectedDrawCard?.Invoke();
        CloseMenu();
    }

    protected virtual void OnSelectedFlipCard()
    {
        SelectedFlipCard?.Invoke();
        CloseMenu();
    }

    protected virtual void OnSelectedShuffle()
    {
        SelectedShuffle?.Invoke();
        CloseMenu();
    }

    protected virtual void OnSelectedSearch()
    {
        SelectedSearch?.Invoke();
        CloseMenu();
    }

    protected virtual void OnSelectedPlaceCard()
    {
        SelectedPlaceCard?.Invoke();
        CloseMenu();
    }

    protected virtual void OnSelectedFaceUp()
    {
        SelectedFaceUp?.Invoke();
        CloseMenu();
    }

    protected virtual void OnSelectedFaceDown()
    {
        SelectedFaceDown?.Invoke();
        CloseMenu();
    }

    private void addInstanceMenuItemListeners()
    {
        InstanceMenu instanceMenu = currentMenuItemsObject.GetComponent<InstanceMenu>();
        if (instanceMenu == null)
        {
            Debug.LogError("Menu doesn't contain InstanceMenu component");
            return;
        }
        instanceMenu.ManageScoresButton.onClick.AddListener(() => Debug.Log("Score management not implemented"));
        instanceMenu.LeaveGameButton.onClick.AddListener(quitApplication);
        setCancelSelectionButton(instanceMenu.CancelSelectionButton);
    }

    private void destroyCurrentMenuItemsObject()
    {
        if (currentMenuItemsObject != null)
        {
            Destroy(currentMenuItemsObject);
        }
        currentMenuItemsObject = null;
    }

    private void showInteractionMenu()
    {
        interactionMenuObject.gameObject.SetActive(true);
    }

    private void hideInteractionMenu()
    {
        interactionMenuObject.gameObject.SetActive(false);
    }

    private void setCancelSelectionButton(Button cancelSelectionButton)
    {
        cancelSelectionButton.onClick.AddListener(OnCancelled);
    }

    private void quitApplication()
    {
        #if UNITY_STANDALONE
            Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
