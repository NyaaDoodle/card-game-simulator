using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private InteractionMenuManager interactionMenuManager;

    private SelectionItem sourceSelection = new SelectionItem();
    private SelectionItem destinationSelection = new SelectionItem();
    private Player player;
    private bool isGameObjectsSet;
    private bool isPlayerSet;

    public void SetupGameObjectEvents()
    {
        LoggingManager.Instance.SelectionManagerLogger.LogMethod();
        subscribeToGameObjectsSelectionEvents();
        isGameObjectsSet = true;
        checkIfGameObjectsAndPlayerAreSet();
    }

    public void SetupPlayerHandEvents()
    {
        LoggingManager.Instance.SelectionManagerLogger.LogMethod();
        player = ManagerReferences.Instance.PlayerManager.LocalPlayer;
        subscribeToPlayerHandSelectionEvents(ManagerReferences.Instance.PlayerManager.LocalPlayerHandDisplay);
        isPlayerSet = true;
        checkIfGameObjectsAndPlayerAreSet();
    }

    private void checkIfGameObjectsAndPlayerAreSet()
    {
        LoggingManager.Instance.SelectionManagerLogger.LogMethod();
        if (isGameObjectsSet && isPlayerSet)
        {
            subscribeToInteractionMenuSelectionEvents();
        }
    }

    private void subscribeToGameObjectsSelectionEvents()
    {
        LoggingManager.Instance.SelectionManagerLogger.LogMethod();
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
        LoggingManager.Instance.SelectionManagerLogger.LogMethod();
        if (interactionMenuManager == null) return;
        interactionMenuManager.Cancelled += onInteractiveMenuCancelled;
        interactionMenuManager.SelectedDrawCard += onInteractiveMenuSelectedDrawCard;
        interactionMenuManager.SelectedFlipCard += onInteractiveMenuSelectedFlipCard;
        interactionMenuManager.SelectedSearch += onInteractiveMenuSelectedSearch;
        interactionMenuManager.SelectedShuffle += onInteractiveMenuSelectedShuffle;
        interactionMenuManager.SelectedPlaceCard += onInteractiveMenuSelectedPlaceCard;
        interactionMenuManager.SelectedFaceUp += onInteractiveMenuSelectedFaceUp;
        interactionMenuManager.SelectedFaceDown += onInteractiveMenuSelectedFaceDown;
    }

    private void unsubscribeFromInteractionMenuSelectionEvents()
    {
        LoggingManager.Instance.SelectionManagerLogger.LogMethod();
        if (interactionMenuManager == null) return;interactionMenuManager.Cancelled += onInteractiveMenuCancelled;
        interactionMenuManager.SelectedDrawCard -= onInteractiveMenuSelectedDrawCard;
        interactionMenuManager.SelectedFlipCard -= onInteractiveMenuSelectedFlipCard;
        interactionMenuManager.SelectedSearch -= onInteractiveMenuSelectedSearch;
        interactionMenuManager.SelectedShuffle -= onInteractiveMenuSelectedShuffle;
        interactionMenuManager.SelectedPlaceCard -= onInteractiveMenuSelectedPlaceCard;
        interactionMenuManager.SelectedFaceUp -= onInteractiveMenuSelectedFaceUp;
        interactionMenuManager.SelectedFaceDown -= onInteractiveMenuSelectedFaceDown;
    }

    private void subscribeToDeckSelectionEvents(IEnumerable<DeckDisplay> deckDisplays)
    {
        LoggingManager.Instance.SelectionManagerLogger.LogMethod();
        if (deckDisplays == null) return;
        foreach (DeckDisplay deckDisplay in deckDisplays)
        {
            deckDisplay.CardSelected += onStackableCardSelected;
            deckDisplay.StackableSelected += onStackableSelfSelected;
        }
    }
    
    private void unsubscribeFromDeckSelectionEvents(IEnumerable<DeckDisplay> deckDisplays)
    {
        LoggingManager.Instance.SelectionManagerLogger.LogMethod();
        if (deckDisplays == null) return;
        foreach (DeckDisplay deckDisplay in deckDisplays)
        {
            deckDisplay.CardSelected -= onStackableCardSelected;
            deckDisplay.StackableSelected -= onStackableSelfSelected;
        }
    }

    private void subscribeToSpaceSelectionEvents(IEnumerable<SpaceDisplay> spaceDisplays)
    {
        LoggingManager.Instance.SelectionManagerLogger.LogMethod();
        if (spaceDisplays == null) return;
        foreach (SpaceDisplay spaceDisplay in spaceDisplays)
        {
            spaceDisplay.CardSelected += onStackableCardSelected;
            spaceDisplay.StackableSelected += onStackableSelfSelected;
        }
    }
    
    private void unsubscribeFromSpaceSelectionEvents(IEnumerable<SpaceDisplay> spaceDisplays)
    {
        LoggingManager.Instance.SelectionManagerLogger.LogMethod();
        if (spaceDisplays == null) return;
        foreach (SpaceDisplay spaceDisplay in spaceDisplays)
        {
            spaceDisplay.CardSelected -= onStackableCardSelected;
            spaceDisplay.StackableSelected -= onStackableSelfSelected;
        }
    }

    private void subscribeToPlayerHandSelectionEvents(PlayerHandDisplay localPlayerHandDisplay)
    {
        LoggingManager.Instance.SelectionManagerLogger.LogMethod();
        localPlayerHandDisplay.CardSelected += onPlayerHandCardSelected;
    }

    private void onStackableCardSelected(CardCollection cardCollection, Card card)
    {
        LoggingManager.Instance.SelectionManagerLogger.LogMethod();
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
        LoggingManager.Instance.SelectionManagerLogger.LogMethod();
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
        LoggingManager.Instance.SelectionManagerLogger.LogMethod();
        if (sourceSelection.IsEmpty)
        { 
            sourceSelection.SelectedCardCollection = stackable;
            showStackableInteractionMenu(stackable);
        }
        else if (sourceSelection.SelectedCardCollection is PlayerHand && sourceSelection.SelectedCard != null)
        {
            destinationSelection.SelectedCardCollection = stackable;
            interactionMenuManager.ShowPlacingCardMenuItems();
        }
    }

    private void showStackableInteractionMenu(Stackable stackable)
    {
        LoggingManager.Instance.SelectionManagerLogger.LogMethod();
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
        LoggingManager.Instance.SelectionManagerLogger.LogMethod();
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
        LoggingManager.Instance.SelectionManagerLogger.LogMethod();
        cancelSelections();
    }

    private void onInteractiveMenuSelectedDrawCard()
    {
        LoggingManager.Instance.SelectionManagerLogger.LogMethod();
        if (sourceSelection.SelectedCardCollection is Stackable stackable && !stackable.IsEmpty)
        {
            player.CmdDrawCard(stackable);
        }
        cancelSelections();
    }

    private void onInteractiveMenuSelectedFlipCard()
    {
        LoggingManager.Instance.SelectionManagerLogger.LogMethod();
        if (sourceSelection.SelectedCardCollection is Stackable stackable && !stackable.IsEmpty)
        {
            player.CmdFlipCard(stackable);
        }
        cancelSelections();
    }

    private void onInteractiveMenuSelectedShuffle()
    {
        LoggingManager.Instance.SelectionManagerLogger.LogMethod();
        if (sourceSelection.SelectedCardCollection is Stackable stackable && stackable.Cards.Count > 1)
        {
            player.CmdShuffleStackable(stackable);
        }
        cancelSelections();
    }

    private void onInteractiveMenuSelectedSearch()
    {
        LoggingManager.Instance.SelectionManagerLogger.LogMethod();
        if (sourceSelection.SelectedCardCollection is Stackable stackable && stackable.Cards.Count > 1)
        {
            Debug.Log("Deck search not implemented");
        }
        cancelSelections();
    }

    private void onInteractiveMenuSelectedPlaceCard()
    {
        // TODO add popup message informing user that they need to select a deck/space to place the card
        LoggingManager.Instance.SelectionManagerLogger.LogMethod();
    }

    private void onInteractiveMenuSelectedFaceUp()
    {
        LoggingManager.Instance.SelectionManagerLogger.LogMethod();
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
        LoggingManager.Instance.SelectionManagerLogger.LogMethod();
        if (sourceSelection.SelectedCardCollection is PlayerHand playerHand && sourceSelection.SelectedCard != null
                                                                            && destinationSelection
                                                                                    .SelectedCardCollection is Stackable
                                                                                stackable && !playerHand.IsEmpty)
        {
            player.CmdPlaceCardFaceDown(sourceSelection.SelectedCard.Value, stackable);
        }
        cancelSelections();
    }

    private void cancelSelections()
    {
        LoggingManager.Instance.SelectionManagerLogger.LogMethod();
        sourceSelection.SetEmpty();
        destinationSelection.SetEmpty();
    }

    private void OnDestroy()
    {
        unsubscribeFromGameObjectsSelectionEvents();
        unsubscribeFromInteractionMenuSelectionEvents();
        isGameObjectsSet = false;
        player = null;
        isPlayerSet = false;
    }
}