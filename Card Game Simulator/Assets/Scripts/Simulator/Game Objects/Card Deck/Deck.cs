using System.Collections.Generic;

public class Deck : Stackable
{
    public DeckData DeckData => (DeckData)StackableData;
    public void Setup(DeckData deckData)
    {
        LoggingManager.Instance.DeckLogger.LogMethod();
        base.Setup(deckData);
        addStartingCards();
    }

    private void addStartingCards()
    {
        LoggingManager.Instance.DeckLogger.LogMethod();
        List<Card> startingCards = new List<Card>();
        foreach (CardData cardData in DeckData.StartingCards)
        {
            startingCards.Add(new Card(cardData));
        }
        base.AddCards(startingCards);
    }
}