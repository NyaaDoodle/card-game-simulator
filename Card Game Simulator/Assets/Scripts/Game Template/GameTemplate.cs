using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

public readonly struct GameTemplate : IEquatable<GameTemplate>
{
    public string Id { get; }
    public GameTemplateDetails GameTemplateDetails { get; }
    public TableData TableData { get; }
    public CardData[] CardPool { get; }
    public DeckData[] DecksData { get; }
    public SpaceData[] SpacesData { get; }

    [JsonConstructor]
    public GameTemplate(
        string id,
        GameTemplateDetails gameTemplateDetails,
        TableData tableData,
        IEnumerable<CardData> cardPool,
        IEnumerable<DeckData> decksData,
        IEnumerable<SpaceData> spacesData)
    {
        Id = id;
        GameTemplateDetails = gameTemplateDetails;
        TableData = tableData;
        CardPool = cardPool.ToArray();
        DecksData = decksData.ToArray();
        SpacesData = spacesData.ToArray();
    }

    public bool Equals(GameTemplate other)
    {
        return other.Id == this.Id && other.GameTemplateDetails == this.GameTemplateDetails
                                   && other.TableData == this.TableData && other.CardPool != null
                                   && this.CardPool != null && other.CardPool.SequenceEqual(this.CardPool)
                                   && other.DecksData != null && this.DecksData != null
                                   && other.DecksData.SequenceEqual(this.DecksData) && other.SpacesData != null
                                   && this.SpacesData != null && other.SpacesData.SequenceEqual(this.SpacesData);
    }

    public override bool Equals(object obj)
    {
        return obj is GameTemplate gameTemplate && this.Equals(gameTemplate);
    }

    public override int GetHashCode()
    {
        HashCode hashcode = new HashCode();
        hashcode.Add(Id);
        hashcode.Add(GameTemplateDetails);
        hashcode.Add(TableData);
        if (CardPool != null)
        {
            foreach (CardData cardData in CardPool)
            {
                hashcode.Add(cardData);
            }
        }
        if (DecksData != null)
        {
            foreach (DeckData deckData in DecksData)
            {
                hashcode.Add(deckData);
            }
        }
        if (SpacesData != null)
        {
            foreach (SpaceData spaceData in SpacesData)
            {
                hashcode.Add(spaceData);
            }
        }
        return hashcode.ToHashCode();
    }

    public static bool operator==(GameTemplate gameTemplate1, GameTemplate gameTemplate2)
    {
        return gameTemplate1.Equals(gameTemplate2);
    }

    public static bool operator!=(GameTemplate gameTemplate1, GameTemplate gameTemplate2)
    {
        return !gameTemplate1.Equals(gameTemplate2);
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("Game Template");
        stringBuilder.AppendLine($"Id: {Id}");
        stringBuilder.AppendLine($"GameTemplateDetails: {GameTemplateDetails}");
        stringBuilder.AppendLine($"TableData: {TableData}");
        stringBuilder.AppendLine($"CardPool:");
        if (CardPool != null)
        {
            foreach (CardData cardData in CardPool)
            {
                stringBuilder.AppendLine(cardData.ToString());
            }
        }
        else
        {
            stringBuilder.AppendLine("null");
        }
        
        if (DecksData != null)
        {
            foreach (DeckData deckData in DecksData)
            {
                stringBuilder.AppendLine(deckData.ToString());
            }
        }
        else
        {
            stringBuilder.AppendLine("null");
        }
        
        if (SpacesData != null)
        {
            foreach (SpaceData spaceData in SpacesData)
            {
                stringBuilder.AppendLine(spaceData.ToString());
            }
        }
        else
        {
            stringBuilder.AppendLine("null");
        }

        return stringBuilder.ToString();
    }
    
    public Dictionary<string, CardData> GetCardDataDictionaryFromCardIds(IEnumerable<string> cardIds)
    {
        // Returns a dictionary (Card ID to corresponding Card Data) for the specified card IDs.
        // If a card ID is not found on CardPool, the function proceeds to the next card ID.
        Dictionary<string, CardData> cardPoolAsDictionary = GetCardPoolAsDictionary();
        Dictionary<string, CardData> resultDictionary = new Dictionary<string, CardData>();
        foreach (string cardId in cardIds)
        {
            if (cardPoolAsDictionary.TryGetValue(cardId, out CardData cardData))
            {
                resultDictionary.Add(cardId, cardData);
            }
        }
        return resultDictionary;
    }

    public Dictionary<string, CardData> GetCardPoolAsDictionary()
    {
        Dictionary<string, CardData> cardPoolAsDictionary = new Dictionary<string, CardData>();
        foreach (CardData cardData in CardPool)
        {
            cardPoolAsDictionary.Add(cardData.Id, cardData);
        }
        return cardPoolAsDictionary;
    }

    public List<CardData> GetCardDataListFromCardIds(IEnumerable<string> cardIds)
    {
        List<CardData> list = new List<CardData>();
        foreach (string cardId in cardIds)
        {
            CardData? cardDataMaybe = findCardById(cardId);
            if (cardDataMaybe != null)
            {
                list.Add(cardDataMaybe.Value);
            }
        }
        return list;
    }

    private CardData? findCardById(string cardId)
    {
        foreach (CardData cardData in CardPool)
        {
            if (cardData.Id == cardId)
            {
                return cardData;
            }
        }
        return null;
    }
}
