using System.Collections.Generic;

public class Deck : Stackable
{
    public DeckData DeckData => (DeckData)StackableData;
    public void Setup(DeckData deckData, GameTemplate gameTemplate)
    {
        base.Setup(deckData);
        IEnumerable<CardData> startingCardsData =
            gameTemplate.GetCardDataListFromCardIds(deckData.StartingCardIds);
        addStartingCards(startingCardsData);
    }

    private void addStartingCards(IEnumerable<CardData> startingCardsData)
    {
        List<Card> startingCards = new List<Card>();
        foreach (CardData cardData in startingCardsData)
        {
            startingCards.Add(new Card(cardData));
        }
        base.AddCards(startingCards);
    }
}