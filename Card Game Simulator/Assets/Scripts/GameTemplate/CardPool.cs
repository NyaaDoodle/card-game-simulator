using System.Collections.Generic;

public class CardPool
{
    private readonly Dictionary<int, CardData> cardDataDictionary;

    public CardPool()
    {
        cardDataDictionary = new Dictionary<int, CardData>();
    }

    public void AddCardData(CardData cardDataToAdd)
    {
        // TODO existence checking
        cardDataDictionary.Add(cardDataToAdd.Id, cardDataToAdd);
    }

    public void RemoveCardData(int cardId)
    {
        // TODO existence checking
        cardDataDictionary.Remove(cardId);
    }

    public CardData GetCardData(int cardId)
    {
        // TODO existence checking
        return cardDataDictionary[cardId];
    }
}