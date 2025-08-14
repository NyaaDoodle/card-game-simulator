using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private InteractionMenuManager interactionMenuManager;

    private SelectionItem sourceSelection = new SelectionItem();
    private SelectionItem destinationSelection = new SelectionItem();
    private Player player;
    private GameInstanceManager gameInstanceManager;

    public void Setup(Player player)
    {
        LoggerReferences.Instance.SelectionManagerLogger.LogMethod();
        this.player = player;
        gameInstanceManager = ManagerReferences.Instance.GameInstanceManager;
        subscribeToGameObjectsSelectionEvents();
        subscribeToInteractionMenuSelectionEvents();
    }

    private void subscribeToGameObjectsSelectionEvents()
    {
        LoggerReferences.Instance.SelectionManagerLogger.LogMethod();
        if (gameInstanceManager == null) return;
        subscribeToDeckSelectionEvents(gameInstanceManager.Decks);
        subscribeToSpaceSelectionEvents(gameInstanceManager.Spaces);
        subscribeToPlayerHandSelectionEvents();
    }

    private void subscribeToInteractionMenuSelectionEvents()
    {
        LoggerReferences.Instance.SelectionManagerLogger.LogMethod();
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

    private void subscribeToDeckSelectionEvents(IEnumerable<Deck> decks)
    {
        LoggerReferences.Instance.SelectionManagerLogger.LogMethod();
        foreach (Deck deck in decks)
        {
            DeckDisplay deckDisplay = deck.GetComponent<DeckDisplay>();
            deckDisplay.CardSelected += onStackableCardSelected;
            deckDisplay.StackableSelected += onStackableSelfSelected;
        }
    }

    private void subscribeToSpaceSelectionEvents(IEnumerable<Space> spaces)
    {
        LoggerReferences.Instance.SelectionManagerLogger.LogMethod();
        foreach (Space space in spaces)
        {
            SpaceDisplay spaceDisplay = space.GetComponent<SpaceDisplay>();
            spaceDisplay.CardSelected += onStackableCardSelected;
            spaceDisplay.StackableSelected += onStackableSelfSelected;
        }
    }

    private void subscribeToPlayerHandSelectionEvents()
    {
        LoggerReferences.Instance.SelectionManagerLogger.LogMethod();
        PlayerHandDisplay playerHandDisplay = player.GetComponent<PlayerHandDisplay>();
        playerHandDisplay.CardSelected += onPlayerHandCardSelected;
    }

    private void onStackableCardSelected(CardCollection cardCollection, Card card)
    {
        LoggerReferences.Instance.SelectionManagerLogger.LogMethod();
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
        LoggerReferences.Instance.SelectionManagerLogger.LogMethod();
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
        LoggerReferences.Instance.SelectionManagerLogger.LogMethod();
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
        LoggerReferences.Instance.SelectionManagerLogger.LogMethod();
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
        LoggerReferences.Instance.SelectionManagerLogger.LogMethod();
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
        LoggerReferences.Instance.SelectionManagerLogger.LogMethod();
        cancelSelections();
    }

    private void onInteractiveMenuSelectedDrawCard()
    {
        LoggerReferences.Instance.SelectionManagerLogger.LogMethod();
        if (sourceSelection.SelectedCardCollection is Stackable stackable && !stackable.IsEmpty)
        {
            player.CmdDrawCard(stackable);
        }
        cancelSelections();
    }

    private void onInteractiveMenuSelectedFlipCard()
    {
        LoggerReferences.Instance.SelectionManagerLogger.LogMethod();
        if (sourceSelection.SelectedCardCollection is Stackable stackable && !stackable.IsEmpty)
        {
            player.CmdFlipCard(stackable);
        }
        cancelSelections();
    }

    private void onInteractiveMenuSelectedShuffle()
    {
        LoggerReferences.Instance.SelectionManagerLogger.LogMethod();
        if (sourceSelection.SelectedCardCollection is Stackable stackable && stackable.Cards.Count > 1)
        {
            player.CmdShuffleStackable(stackable);
        }
        cancelSelections();
    }

    private void onInteractiveMenuSelectedSearch()
    {
        LoggerReferences.Instance.SelectionManagerLogger.LogMethod();
        if (sourceSelection.SelectedCardCollection is Stackable stackable && stackable.Cards.Count > 1)
        {
            Debug.Log("Deck search not implemented");
        }
        cancelSelections();
    }

    private void onInteractiveMenuSelectedPlaceCard()
    {
        // TODO add popup message informing user that they need to select a deck/space to place the card
        LoggerReferences.Instance.SelectionManagerLogger.LogMethod();
    }

    private void onInteractiveMenuSelectedFaceUp()
    {
        LoggerReferences.Instance.SelectionManagerLogger.LogMethod();
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
        LoggerReferences.Instance.SelectionManagerLogger.LogMethod();
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
        LoggerReferences.Instance.SelectionManagerLogger.LogMethod();
        sourceSelection.SetEmpty();
        destinationSelection.SetEmpty();
    }
}