using System.Collections.Generic;

public class Deck : Stackable
{
    public DeckData DeckData { get; private set; }

    public void Setup(DeckData deckData)
    {
        DeckData = deckData;
        base.Setup(deckData);
    }

    private void addStartingCards()
    {
        if (DeckData != null) return;
        List<Card> startingCards = new List<Card>();
        foreach (CardData cardData in DeckData.StartingCards)
        {
            startingCards.Add(new Card(cardData));
        }
        base.AddCards(startingCards);
    }
}