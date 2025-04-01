using UnityEngine;

public class TestCard : MonoBehaviour
{
    void Start()
    {
        CardData cardData = new CardData();
        cardData.Id = 0;
        cardData.Width = 2;
        cardData.Height = 3;
        cardData.BackSideSprite = Resources.Load<Sprite>("blue_back");
        cardData.FrontSideSprite = Resources.Load<Sprite>("2H");
        CardState cardState = GetComponent<CardState>();
        cardState.SetCardData(cardData);
    }
}
