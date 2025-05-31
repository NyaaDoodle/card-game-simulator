using UnityEngine;

public class PlayerHandTest : MonoBehaviour
{
    public GameObject playerHandPrefab;
    public GameObject cardPrefab;
    public Transform handContainer;
    public int cardsToSpawn;
    public GameInstanceState gameInstanceState;

    private GameObject handObject;
    private PlayerHandState playerHand;

    void Start()
    {
        handObject = Instantiate(playerHandPrefab, handContainer);
        gameInstanceState.PlayerHandObjects.Add(handObject);
        playerHand = handObject.GetComponent<PlayerHandState>();
        CardData cardData = new CardData()
                                {
                                    FrontSideSpritePath = "Standard52/Gray_back", BackSideSpritePath = "Standard52/AD"
                                };
        for (int i = 0; i < cardsToSpawn; i++)
        {
            GameObject card = Instantiate(cardPrefab);
            card.GetComponent<CardState>().Initialize(cardData);
            playerHand.AddCardToHand(card);
        }
    }
}
