using System.Collections.Generic;

public class CardUtilities
{
    public static LinkedList<Card> CreateCards(ICollection<CardData> cardDataCollection)
    {
        LinkedList<Card> cards = new LinkedList<Card>();
        foreach (CardData cardData in cardDataCollection)
        {
            Card card = new Card(cardData);
            cards.AddLast(card);
        }
        return cards;
    }
}
