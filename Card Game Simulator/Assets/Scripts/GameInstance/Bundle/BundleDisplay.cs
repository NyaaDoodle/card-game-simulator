using UnityEngine;

[RequireComponent(typeof(BundleState))]
public class BundleDisplay : MonoBehaviour
{
    [SerializeField] private GameObject bundleHighlightObject;
    void Start()
    {
        BundleState bundleState = GetComponent<BundleState>();
        
        bundleState.CardsChanged += bundleState_OnCardsChanged;
        updateViewableCard(bundleState);
    }

    private void updateViewableCard(BundleState bundleState)
    {
        // TODO less inefficient version...
        foreach (GameObject card in bundleState.Cards)
        {
            CardState cardState = card.GetComponent<CardState>();

            if (card == bundleState.TopCard)
            {
                cardState.ShowCard();
                updateHighlightScale(cardState);
            }
            else
            {
                cardState.HideCard();
            }
        }
    }

    private void updateHighlightScale(CardState cardState)
    {
        const float scaleFactor = 1.1f;
        float cardWidth = cardState.Width ?? 0;
        float cardHeight = cardState.Height ?? 0;
        Vector3 scaleVector = new Vector3(cardWidth * scaleFactor, cardHeight * scaleFactor, 1);

        bundleHighlightObject.transform.localScale = scaleVector;
    }

    private void bundleState_OnCardsChanged(BundleState bundleState)
    {
        updateViewableCard(bundleState);
    }
}
