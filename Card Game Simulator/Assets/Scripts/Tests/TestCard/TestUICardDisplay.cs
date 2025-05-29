using UnityEngine;

public class TestUICardDisplay : MonoBehaviour
{
    [SerializeField] private GameObject testCardObject;

    void Start()
    {
        CardData testCard = new CardData
                                {
                                    Id = 1,
                                    Name = "Test Card",
                                    FrontSideSpritePath = "Standard52/2C"
                                };
        CardState cardState = testCardObject.GetComponent<CardState>();
        cardState.SetCardData(testCard);
    }
}
