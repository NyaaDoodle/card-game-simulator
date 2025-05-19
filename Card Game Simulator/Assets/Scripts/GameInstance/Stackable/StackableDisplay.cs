using UnityEngine;

[RequireComponent(typeof(StackableState))]
public class StackableDisplay : MonoBehaviour
{
    [SerializeField] private GameObject stackableHighlightObject;
    private StackableState stackableState;

    void Awake()
    {
        stackableState = GetComponent<StackableState>();
    }

    void Start()
    {
        stackableState.CardsChanged += bundleState_OnCardsChanged;
        updateViewableCard();
    }

    private void updateViewableCard()
    {
        // TODO less inefficient version...
        foreach (GameObject card in stackableState.Cards)
        {
            CardState cardState = card.GetComponent<CardState>();

            if (card == stackableState.TopCard)
            {
                cardState.ShowCard();
            }
            else
            {
                cardState.HideCard();
            }
        }
        updateHighlightScale();
    }

    private void updateHighlightScale()
    {
        const float defaultWidth = 2f;
        const float defaultHeight = 3f;
        const float scaleFactor = 0.1f;

        float setWidth = defaultWidth;
        float setHeight = defaultHeight;

        GameObject topCardObject = stackableState.TopCard;
        if (topCardObject != null)
        {
            CardState cardState = topCardObject.GetComponent<CardState>();
            setWidth = cardState.Width ?? 0;
            setHeight = cardState.Height ?? 0;
            
        }
        Vector3 scaleVector = new Vector3(setWidth + scaleFactor, setHeight + scaleFactor, 1);
        stackableHighlightObject.transform.localScale = scaleVector;
    }

    private void bundleState_OnCardsChanged(StackableState _)
    {
        updateViewableCard();
    }
}
