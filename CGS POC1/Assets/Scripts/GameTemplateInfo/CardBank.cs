using System.Collections.Generic;
using UnityEngine;

public class CardBank
{
    private Dictionary<string, CardInfo> cardIdToCardInfo { get; set; } = new Dictionary<string, CardInfo>();

    public void AddCardInfo(CardInfo cardInfoToAdd)
    {
        cardIdToCardInfo.Add(cardInfoToAdd.Id, cardInfoToAdd);
    }
}
