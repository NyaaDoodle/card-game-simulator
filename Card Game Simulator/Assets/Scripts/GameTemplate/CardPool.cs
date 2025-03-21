using System.Collections.Generic;
using Assets.Scripts.GameTemplate.Exceptions;
using UnityEngine;

public class CardPool
{
    private readonly Dictionary<int, CardData> cardDataDictionary;

    public CardPool()
    {
        cardDataDictionary = new Dictionary<int, CardData>();
    }

    public int AddCardData(CardData cardDataToAdd)
    {
        // Adds the parameter card data to the card pool, returns the card's ID in the pool.
        int cardId = cardDataDictionary.Count + 1;
        cardDataToAdd.Id = cardId;
        bool isAddSuccessful = cardDataDictionary.TryAdd(cardDataToAdd.Id, cardDataToAdd);
        if (!isAddSuccessful)
        {
            Debug.Log($"Card with ID {cardDataToAdd.Id} already exists in card pool.");
            cardId = -1;
        }
        return cardId;
    }

    public void RemoveCardData(int cardId)
    {
        cardDataDictionary.Remove(cardId);
    }

    public CardData GetCardData(int cardId)
    {
        bool isGetSuccessful = cardDataDictionary.TryGetValue(cardId, out CardData cardData);
        if (isGetSuccessful)
        {
            return cardData;
        }
        else
        {
            throw new CardNotInCardPoolException();
        }
    }
}