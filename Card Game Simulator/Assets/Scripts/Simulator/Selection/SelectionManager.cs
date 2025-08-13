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
        this.player = player;
        gameInstanceManager = ManagerReferences.Instance.GameInstanceManager;
        subscribeToGameObjectsSelectionEvents();
        subscribeToInteractionMenuSelectionEvents();
    }

    private void subscribeToGameObjectsSelectionEvents()
    {
        if (gameInstanceManager == null) return;
        subscribeToDeckSelectionEvents(gameInstanceManager.Decks);
        subscribeToSpaceSelectionEvents(gameInstanceManager.Spaces);
        subscribeToPlayerHandSelectionEvents();
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

    private void subscribeToDeckSelectionEvents(IEnumerable<Deck> decks)
    {
        foreach (Deck deck in decks)
        {
            DeckDisplay deckDisplay = deck.GetComponent<DeckDisplay>();
            deckDisplay.CardSelected += onStackableCardSelected;
            deckDisplay.StackableSelected += onStackableSelfSelected;
        }
    }

    private void subscribeToSpaceSelectionEvents(IEnumerable<Space> spaces)
    {
        foreach (Space space in spaces)
        {
            SpaceDisplay spaceDisplay = space.GetComponent<SpaceDisplay>();
            spaceDisplay.CardSelected += onStackableCardSelected;
            spaceDisplay.StackableSelected += onStackableSelfSelected;
        }
    }

    private void subscribeToPlayerHandSelectionEvents()
    {
        PlayerHandDisplay playerHandDisplay = player.GetComponent<PlayerHandDisplay>();
        playerHandDisplay.CardSelected += onPlayerHandCardSelected;
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
        TraceLogger.LogMethod();
        TraceLogger.LogVariable("playerName", sourceSelection.SelectedCardCollection);
        TraceLogger.LogVariable("selectedCard", sourceSelection.SelectedCardCollection);
        TraceLogger.LogVariable("stackable", destinationSelection.SelectedCardCollection);
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
        TraceLogger.LogMethod();
        TraceLogger.LogVariable("playerName", sourceSelection.SelectedCardCollection);
        TraceLogger.LogVariable("selectedCard", sourceSelection.SelectedCardCollection);
        TraceLogger.LogVariable("stackable", destinationSelection.SelectedCardCollection);
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
}