using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private InteractionMenuManager interactionMenuManager;

    private SelectionItem sourceSelection = new SelectionItem();
    private SelectionItem destinationSelection = new SelectionItem();
    private PlayerAction? currentPlayerAction;
    private Player player;
    private bool isGameObjectsSet;
    private bool isPlayerSet;

    public void SetupGameObjectEvents()
    {
        subscribeToGameObjectsSelectionEvents();
        isGameObjectsSet = true;
        checkIfGameObjectsAndPlayerAreSet();
    }

    public void SetupPlayerHandEvents()
    {
        player = ManagerReferences.Instance.PlayerManager.LocalPlayer;
        subscribeToPlayerHandSelectionEvents(ManagerReferences.Instance.PlayerManager.LocalPlayerHandDisplay);
        isPlayerSet = true;
        checkIfGameObjectsAndPlayerAreSet();
    }

    private void checkIfGameObjectsAndPlayerAreSet()
    {
        if (isGameObjectsSet && isPlayerSet)
        {
            subscribeToInteractionMenuSelectionEvents();
        }
    }

    private void subscribeToGameObjectsSelectionEvents()
    {
        subscribeToDeckSelectionEvents(ManagerReferences.Instance.GameInstanceManager.DeckDisplays);
        subscribeToSpaceSelectionEvents(ManagerReferences.Instance.GameInstanceManager.SpaceDisplays);
    }

    private void unsubscribeFromGameObjectsSelectionEvents()
    {
        unsubscribeFromDeckSelectionEvents(ManagerReferences.Instance.GameInstanceManager.DeckDisplays);
        unsubscribeFromSpaceSelectionEvents(ManagerReferences.Instance.GameInstanceManager.SpaceDisplays);
        
    }

    private void subscribeToInteractionMenuSelectionEvents()
    {
        if (interactionMenuManager == null) return;
        interactionMenuManager.Cancelled += onInteractiveMenuCancelled;
        interactionMenuManager.SelectedDrawCard += onInteractiveMenuSelectedDrawCard;
        interactionMenuManager.SelectedFlipCard += onInteractiveMenuSelectedFlipCard;
        interactionMenuManager.SelectedSearch += onInteractiveMenuSelectedSearch;
        interactionMenuManager.SelectedShuffle += onInteractiveMenuSelectedShuffle;
        interactionMenuManager.SelectedPlaceCard += onInteractiveMenuSelectedPlaceCard;
        interactionMenuManager.SelectedFaceUp += onInteractiveMenuSelectedFaceUp;
        interactionMenuManager.SelectedFaceDown += onInteractiveMenuSelectedFaceDown;
        interactionMenuManager.SelectedTakeAllCards += onInteractiveMenuSelectedTakeAllCards;
        interactionMenuManager.SelectedTransferCards += onInteractiveMenuSelectedTransferCards;
    }

    private void unsubscribeFromInteractionMenuSelectionEvents()
    {
        if (interactionMenuManager == null) return;
        interactionMenuManager.Cancelled += onInteractiveMenuCancelled;
        interactionMenuManager.SelectedDrawCard -= onInteractiveMenuSelectedDrawCard;
        interactionMenuManager.SelectedFlipCard -= onInteractiveMenuSelectedFlipCard;
        interactionMenuManager.SelectedSearch -= onInteractiveMenuSelectedSearch;
        interactionMenuManager.SelectedShuffle -= onInteractiveMenuSelectedShuffle;
        interactionMenuManager.SelectedPlaceCard -= onInteractiveMenuSelectedPlaceCard;
        interactionMenuManager.SelectedFaceUp -= onInteractiveMenuSelectedFaceUp;
        interactionMenuManager.SelectedFaceDown -= onInteractiveMenuSelectedFaceDown;
        interactionMenuManager.SelectedTakeAllCards -= onInteractiveMenuSelectedTakeAllCards;
        interactionMenuManager.SelectedTransferCards -= onInteractiveMenuSelectedTransferCards;
    }

    private void subscribeToDeckSelectionEvents(IEnumerable<DeckDisplay> deckDisplays)
    {
        if (deckDisplays == null) return;
        foreach (DeckDisplay deckDisplay in deckDisplays)
        {
            deckDisplay.CardSelected += onStackableCardSelected;
            deckDisplay.StackableSelected += onStackableSelfSelected;
        }
    }
    
    private void unsubscribeFromDeckSelectionEvents(IEnumerable<DeckDisplay> deckDisplays)
    {
        if (deckDisplays == null) return;
        foreach (DeckDisplay deckDisplay in deckDisplays)
        {
            deckDisplay.CardSelected -= onStackableCardSelected;
            deckDisplay.StackableSelected -= onStackableSelfSelected;
        }
    }

    private void subscribeToSpaceSelectionEvents(IEnumerable<SpaceDisplay> spaceDisplays)
    {
        if (spaceDisplays == null) return;
        foreach (SpaceDisplay spaceDisplay in spaceDisplays)
        {
            spaceDisplay.CardSelected += onStackableCardSelected;
            spaceDisplay.StackableSelected += onStackableSelfSelected;
        }
    }
    
    private void unsubscribeFromSpaceSelectionEvents(IEnumerable<SpaceDisplay> spaceDisplays)
    {
        if (spaceDisplays == null) return;
        foreach (SpaceDisplay spaceDisplay in spaceDisplays)
        {
            spaceDisplay.CardSelected -= onStackableCardSelected;
            spaceDisplay.StackableSelected -= onStackableSelfSelected;
        }
    }

    private void subscribeToPlayerHandSelectionEvents(PlayerHandDisplay localPlayerHandDisplay)
    {
        localPlayerHandDisplay.CardSelected += onPlayerHandCardSelected;
    }

    private void onStackableCardSelected(CardCollection cardCollection, Card card)
    {
        if (cardCollection is Stackable stackable)
        {
            onStackableSelected(stackable);
        }
        else
        {
            cancelSelections();
        }
    }

    private void onStackableSelfSelected(CardCollection cardCollection)
    {
        if (cardCollection is Stackable stackable)
        {
            onStackableSelected(stackable);
        }
        else
        {
            cancelSelections();
        }
    }

    private void onStackableSelected(Stackable stackable)
    {
        if (sourceSelection.IsEmpty)
        { 
            sourceSelection.SelectedCardCollection = stackable;
            showStackableInteractionMenu(stackable);
        }
        else if (currentPlayerAction == PlayerAction.PlaceCard && sourceSelection.SelectedCardCollection is PlayerHand
                                                               && sourceSelection.SelectedCard != null)
        {
            destinationSelection.SelectedCardCollection = stackable;
            interactionMenuManager.ShowPlacingCardMenuItems();
        }
        else if (currentPlayerAction == PlayerAction.TransferCards && sourceSelection.SelectedCardCollection is Stackable origin)
        {
            destinationSelection.SelectedCardCollection = stackable;
            player.CmdTransferCards(origin, stackable);
            cancelSelections();
        }
    }

    private void showStackableInteractionMenu(Stackable stackable)
    {
        if (stackable is Deck deck)
        {
            interactionMenuManager.ShowDeckActionMenu(deck);
        }
        else if (stackable is Space space)
        {
            interactionMenuManager.ShowSpaceActionMenu(space);
        }
        else
        {
            cancelSelections();
        }
    }

    private void onPlayerHandCardSelected(CardCollection cardCollection, Card selectedCard)
    {
        if (cardCollection is PlayerHand playerHand)
        {
            if (sourceSelection.IsEmpty)
            {
                sourceSelection.SelectedCardCollection = playerHand;
                sourceSelection.SelectedCard = selectedCard;
                interactionMenuManager.ShowCardSelectionActionMenu(playerHand);
            }
            else
            {
                cancelSelections();
            }
        }
        else
        {
            cancelSelections();
        }
    }

    private void onInteractiveMenuCancelled()
    {
        cancelSelections();
    }

    private void onInteractiveMenuSelectedDrawCard()
    {
        currentPlayerAction = PlayerAction.DrawCard;
        if (sourceSelection.SelectedCardCollection is Stackable stackable && !stackable.IsEmpty)
        {
            player.CmdDrawCard(stackable);
        }
        cancelSelections();
    }

    private void onInteractiveMenuSelectedFlipCard()
    {
        currentPlayerAction = PlayerAction.FlipCard;
        if (sourceSelection.SelectedCardCollection is Stackable stackable && !stackable.IsEmpty)
        {
            player.CmdFlipCard(stackable);
        }
        cancelSelections();
    }

    private void onInteractiveMenuSelectedShuffle()
    {
        currentPlayerAction = PlayerAction.Shuffle;
        if (sourceSelection.SelectedCardCollection is Stackable stackable && stackable.Cards.Count > 1)
        {
            player.CmdShuffleStackable(stackable);
        }
        cancelSelections();
    }

    private void onInteractiveMenuSelectedSearch()
    {
        currentPlayerAction = PlayerAction.Search;
        if (sourceSelection.SelectedCardCollection is Stackable stackable && stackable.Cards.Count > 1)
        {
            InputActionsController.Instance.IsDragInputActionActive = false;
            ModalWindowManager.OpenCardSelectionModalWindow("Select a Card to Take:", stackable.Cards,
                (card) =>
                    {
                        ModalWindowManager.CloseCurrentWindow();
                        InputActionsController.Instance.IsDragInputActionActive = true;
                        player.CmdTakeCard(stackable, card);
                        cancelSelections();
                    },
                null,
                () =>
                    {
                        ModalWindowManager.CloseCurrentWindow();
                        InputActionsController.Instance.IsDragInputActionActive = true;
                        cancelSelections();
                    });
        }
    }

    private void onInteractiveMenuSelectedPlaceCard()
    {
        currentPlayerAction = PlayerAction.PlaceCard;
        // TODO add popup message informing user that they need to select a deck/space to place the card
    }

    private void onInteractiveMenuSelectedFaceUp()
    {
        if (sourceSelection.SelectedCardCollection is PlayerHand playerHand && sourceSelection.SelectedCard != null
                                                                            && destinationSelection
                                                                                    .SelectedCardCollection is Stackable
                                                                                stackable && !playerHand.IsEmpty)
        {
            player.CmdPlaceCardFaceUp(sourceSelection.SelectedCard.Value, stackable);
        }
        cancelSelections();
    }

    private void onInteractiveMenuSelectedFaceDown()
    {
        if (sourceSelection.SelectedCardCollection is PlayerHand playerHand && sourceSelection.SelectedCard != null
                                                                            && destinationSelection
                                                                                    .SelectedCardCollection is Stackable
                                                                                stackable && !playerHand.IsEmpty)
        {
            player.CmdPlaceCardFaceDown(sourceSelection.SelectedCard.Value, stackable);
        }
        cancelSelections();
    }

    private void onInteractiveMenuSelectedTakeAllCards()
    {
        currentPlayerAction = PlayerAction.TakeAllCards;
        if (sourceSelection.SelectedCardCollection is Stackable stackable && !stackable.IsEmpty)
        {
            player.CmdTakeAllCards(stackable);
        }
        cancelSelections();
    }

    private void onInteractiveMenuSelectedTransferCards()
    {
        currentPlayerAction = PlayerAction.TransferCards;
        // TODO add popup message informing user that they need to select a deck/space to place the card
    }

    private void cancelSelections()
    {
        sourceSelection.SetEmpty();
        destinationSelection.SetEmpty();
        currentPlayerAction = null;
    }

    private void OnDestroy()
    {
        cancelSelections();
        unsubscribeFromGameObjectsSelectionEvents();
        unsubscribeFromInteractionMenuSelectionEvents();
        isGameObjectsSet = false;
        player = null;
        isPlayerSet = false;
    }
}