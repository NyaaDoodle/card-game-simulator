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
    }

    private void unsubscribeFromInteractionMenuSelectionEvents()
    {
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
        else if (sourceSelection.SelectedCardCollection is PlayerHand && sourceSelection.SelectedCard != null)
        {
            destinationSelection.SelectedCardCollection = stackable;
            interactionMenuManager.ShowPlacingCardMenuItems();
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
        if (sourceSelection.SelectedCardCollection is Stackable stackable && !stackable.IsEmpty)
        {
            player.CmdDrawCard(stackable);
        }
        cancelSelections();
    }

    private void onInteractiveMenuSelectedFlipCard()
    {
        if (sourceSelection.SelectedCardCollection is Stackable stackable && !stackable.IsEmpty)
        {
            player.CmdFlipCard(stackable);
        }
        cancelSelections();
    }

    private void onInteractiveMenuSelectedShuffle()
    {
        if (sourceSelection.SelectedCardCollection is Stackable stackable && stackable.Cards.Count > 1)
        {
            player.CmdShuffleStackable(stackable);
        }
        cancelSelections();
    }

    private void onInteractiveMenuSelectedSearch()
    {
        if (sourceSelection.SelectedCardCollection is Stackable stackable && stackable.Cards.Count > 1)
        {
            Debug.Log("Deck search not implemented");
        }
        cancelSelections();
    }

    private void onInteractiveMenuSelectedPlaceCard()
    {
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

    private void cancelSelections()
    {
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