using System.Collections.Generic;

public class Deck : Stackable
{
    public DeckData DeckData => (DeckData)StackableData;
    public void Setup(DeckData deckData)
    {
        LoggerReferences.Instance.DeckLogger.LogMethod();
        base.Setup(deckData);
        addStartingCards();
    }

    private void addStartingCards()
    {
        LoggerReferences.Instance.DeckLogger.LogMethod();
        List<Card> startingCards = new List<Card>();
        foreach (CardData cardData in DeckData.StartingCards)
        {
            startingCards.Add(new Card(cardData));
        }
        base.AddCards(startingCards);
    }
}