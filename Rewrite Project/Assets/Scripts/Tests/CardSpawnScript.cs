using UnityEngine;

public class CardSpawnScript : MonoBehaviour
{
    [SerializeField] private GameObject cardTableDisplayPrefab;
    [SerializeField] private RectTransform cardContainer;
    void Start()
    {
        CardData cardData = new CardData() { Id = 0, BackSideSpritePath = "1", FrontSideSpritePath = "2" };
        CardState cardState = new CardState(cardData);
        CardTableDisplay cardDisplay = cardTableDisplayPrefab.InstantiateCardTableDisplay(cardState, cardContainer);
    }
}
