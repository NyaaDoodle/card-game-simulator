using System.Collections.Generic;

public class CardUtilities
{
    public static LinkedList<CardState> CreateCards(ICollection<CardData> cardDataCollection)
    {
        LinkedList<CardState> cards = new LinkedList<CardState>();
        foreach (CardData cardData in cardDataCollection)
        {
            CardState card = new CardState(cardData);
            cards.AddLast(card);
        }
        return cards;
    }
}
