using UnityEngine;

public class CardSpawnTest : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform cardContainer;
    [SerializeField] private string frontSideSpritePath = "Standard52/2C";
    [SerializeField] private string backSideSpritePath = "Standard52/Gray_back";
    
    void Start()
    {
        spawnTestCard();
    }

    private void spawnTestCard()
    {
        CardData cardData = new CardData()
                                {
                                    BackSideSpritePath = backSideSpritePath, FrontSideSpritePath = frontSideSpritePath
                                };
        GameObject cardObject = Instantiate(cardPrefab, cardContainer);
        CardState cardState = cardObject.GetComponent<CardState>();
        cardState.Initialize(cardData);
    }
}
