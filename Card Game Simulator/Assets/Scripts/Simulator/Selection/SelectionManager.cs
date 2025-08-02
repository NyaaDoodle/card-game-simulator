using System;

public class SelectionManager
{
    private GameInstance gameInstance;
    private Tuple<CardCollection, Card> selectedSource;
    private CardCollection selectedDestination;

    public SelectionManager(GameInstance gameInstance)
    {
        this.gameInstance = gameInstance;
        subscribeToGameObjectsSelectionEvents();
    }

    private void subscribeToGameObjectsSelectionEvents()
    {
        subscribeToDeckSelectionEvents();
        subscribeToSpaceSelectionEvents();
        subscribeToPlayerHandSelectionEvents();
    }

    private void subscribeToDeckSelectionEvents()
    {
        foreach (Deck deck in gameInstance.Decks)
        {
            deck.CardSelected += onDeckCardSelected;
            deck.StackableSelected += onDeckStackableSelected;
        }
    }

    private void subscribeToSpaceSelectionEvents()
    {
        foreach (Space space in gameInstance.Spaces)
        {
            space.CardSelected += onSpaceCardSelected;
            space.StackableSelected += onSpaceStackableSelected;
        }
    }

    private void subscribeToPlayerHandSelectionEvents()
    {
        foreach (Player player in gameInstance.Players)
        {
            PlayerHand playerHand = player.PlayerHand;
            playerHand.CardSelected += onPlayerHandCardSelected;
        }
    }

    private void onDeckCardSelected(CardCollection cardCollection, Card selectedCard)
    {
        if (cardCollection is Deck deck)
        {
            
        }
    }

    private void onDeckStackableSelected(CardCollection cardCollection)
    {

    }

    private void onSpaceCardSelected(CardCollection cardCollection, Card selectedCard)
    {

    }

    private void onSpaceStackableSelected(CardCollection cardCollection)
    {

    }

    private void onPlayerHandCardSelected(CardCollection cardCollection, Card selectedCard)
    {

    }
}