using UnityEngine;
using UnityEngine.UI;

public class ActionMenuManager : MonoBehaviour
{
    [SerializeField] private GameInstanceState gameInstanceState;
    [SerializeField] private GameObject menuObjectsContainer;
    [SerializeField] private SelectionManager selectionManager;

    [SerializeField] private Button drawCardButton;
    [SerializeField] private Button flipCardButton;
    [SerializeField] private Button placeCardButton;
    [SerializeField] private Button cancelSelectionButton;

    void Start()
    {
        selectionManager.SelectionChanged += onSelectionChanged;
        selectionManager.SelectionCleared += onSelectionCleared;
    }

    private void onSelectionChanged(GameObject selectedObject, SelectionType selectionType)
    {
        Activate();
        setActionOptions(selectedObject, selectionType);
    }

    private void onSelectionCleared()
    {
        setActionOptions(null, SelectionType.None);
        Deactivate();
    }

    public void Activate()
    {
        if (!menuObjectsContainer.activeSelf)
        {
            menuObjectsContainer.SetActive(true);
        }
    }

    public void Deactivate()
    {
        if (menuObjectsContainer.activeSelf)
        {
            menuObjectsContainer.SetActive(false);
        }
    }

    private void setActionOptions(GameObject selectedObject, SelectionType selectionType)
    {
        if (selectionManager != null && selectionManager.HasSelection)
        {
            switch (selectionType)
            {
                //case SelectionType.PlayerCard:
                    //deactivateDrawCardButton();
                    //deactivateFlipCardButton();
                    //activatePlaceCardButton(selectedObject, selectionType);
                //    break;
                case SelectionType.Deck:
                case SelectionType.Space:
                    activateDrawCardButton(selectedObject, selectionType);
                    activateFlipCardButton(selectedObject, selectionType);
                    deactivatePlaceCardButton();
                    break;
                default:
                    deactivateDrawCardButton();
                    deactivateFlipCardButton();
                    deactivatePlaceCardButton();
                    break;
            }
        }
    }

    private void activateDrawCardButton(GameObject selectedObject, SelectionType selectionType)
    {
        drawCardButton.interactable = true;
        drawCardButton.onClick.AddListener(
            () =>
                {
                    GameObject drawnCard = selectionManager.CurrentSelectedObject.GetComponent<StackableState>().DrawCard();
                    if (drawnCard != null)
                    {
                        gameInstanceState.PlayerHandObjects[0].GetComponent<PlayerHandState>().AddCardToHand(drawnCard);
                    }
                });
    }

    private void activateFlipCardButton(GameObject selectedObject, SelectionType selectionType)
    {
        flipCardButton.interactable = true;
        flipCardButton.onClick.AddListener(
            () =>
                {
                    selectionManager.CurrentSelectedObject.GetComponent<StackableState>().FlipTopCard();
                });
    }

    private void activatePlaceCardButton(GameObject selectedObject, SelectionType selectionType)
    {
        //placeCardButton.interactable = true;
        //placeCardButton.onClick.AddListener(
        //    () =>
        //        {
        //            GameObject removedCard = selectionManager.CurrentSelectedObject.GetComponent<PlayerHandState>()
        //                .RemoveSelectedCard();
        //            // ...
        //        });
    }

    private void deactivateDrawCardButton()
    {
        drawCardButton.onClick.RemoveAllListeners();
        drawCardButton.interactable = false;
    }

    private void deactivateFlipCardButton()
    {
        flipCardButton.onClick.RemoveAllListeners();
        flipCardButton.interactable = false;
    }

    private void deactivatePlaceCardButton()
    {
        placeCardButton.onClick.RemoveAllListeners();
        placeCardButton.interactable = false;
    }
}
