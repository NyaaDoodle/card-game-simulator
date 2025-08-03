using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private InteractionMenuManager interactionMenuManager;

    private SelectionItem sourceSelection = new SelectionItem();
    private SelectionItem destinationSelection = new SelectionItem();
    private Player player;

    public void Setup(GameInstance gameInstance, Player player)
    {
        this.player = player;
        subscribeToGameObjectsSelectionEvents(gameInstance);
        subscribeToInteractionMenuSelectionEvents();
    }

    private void subscribeToGameObjectsSelectionEvents(GameInstance gameInstance)
    {
        if (gameInstance == null) return;
        subscribeToDeckSelectionEvents(gameInstance.Decks);
        subscribeToSpaceSelectionEvents(gameInstance.Spaces);
        subscribeToPlayerHandSelectionEvents(gameInstance.Players);
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

    private void subscribeToDeckSelectionEvents(List<Deck> decks)
    {
        foreach (Deck deck in decks)
        {
            deck.CardSelected += onStackableCardSelected;
            deck.StackableSelected += onStackableSelfSelected;
        }
    }

    private void subscribeToSpaceSelectionEvents(List<Space> spaces)
    {
        foreach (Space space in spaces)
        {
            space.CardSelected += onStackableCardSelected;
            space.StackableSelected += onStackableSelfSelected;
        }
    }

    private void subscribeToPlayerHandSelectionEvents(List<Player> players)
    {
        foreach (Player player in players)
        {
            PlayerHand playerHand = player.PlayerHand;
            playerHand.CardSelected += onPlayerHandCardSelected;
        }
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
            Card drawnCard = stackable.DrawCard();
            player.PlayerHand.AddCard(drawnCard);
        }
        cancelSelections();
    }

    private void onInteractiveMenuSelectedFlipCard()
    {
        if (sourceSelection.SelectedCardCollection is Stackable stackable && !stackable.IsEmpty)
        {
            stackable.FlipTopCard();
        }
        cancelSelections();
    }

    private void onInteractiveMenuSelectedShuffle()
    {
        if (sourceSelection.SelectedCardCollection is Stackable stackable && stackable.Cards.Count > 1)
        {
            stackable.Shuffle();
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

    }

    private void onInteractiveMenuSelectedFaceUp()
    {
        if (placeCard())
        {
            sourceSelection.SelectedCard.FlipFaceUp();
        }
        cancelSelections();
    }

    private void onInteractiveMenuSelectedFaceDown()
    {
        if (placeCard())
        {
            sourceSelection.SelectedCard.FlipFaceDown();
        }
        cancelSelections();
    }

    private bool placeCard()
    {
        if (sourceSelection.SelectedCardCollection is PlayerHand playerHand && sourceSelection.SelectedCard != null
                                                                            && destinationSelection
                                                                                    .SelectedCardCollection is Stackable
                                                                                stackable && !playerHand.IsEmpty)
        {
            if (playerHand.RemoveCard(sourceSelection.SelectedCard))
            {
                stackable.AddCardToTop(sourceSelection.SelectedCard);
                return true;
            }
        }
        return false;
    }

    private void cancelSelections()
    {
        sourceSelection.SetEmpty();
        destinationSelection.SetEmpty();
    }
}