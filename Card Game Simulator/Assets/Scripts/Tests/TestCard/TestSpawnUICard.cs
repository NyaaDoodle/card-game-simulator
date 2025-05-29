using UnityEngine;

public class TestSpawnUICard : MonoBehaviour
{
    [SerializeField] private GameObject testUICardObject;
    [SerializeField] private Transform cardContainer;

    void Start()
    {
        spawnTestCard();
    }

    private void spawnTestCard()
    {
        GameObject cardObject = Instantiate(testUICardObject, cardContainer);
        CardState cardState = cardObject.GetComponent<CardState>();
        CardData testCard = new CardData
                                {
                                    Id = 1,
                                    Name = "Test Card",
                                    FrontSideSpritePath = "Standard52/2C"
                                };
        cardState.SetCardData(testCard);
    }
}
