using UnityEngine;

public class CardUtilities
{
    public static void AttachCardTransformAndCenter(GameObject card, Transform container)
    {
        if (card == null)
        {
            Debug.LogWarning("Card is null");
            return;
        }

        RectTransform cardRectTransform = card.GetComponent<RectTransform>();
        if (cardRectTransform == null)
        {
            Debug.LogWarning("Card doesn't have a RectTransform component");
            return;
        }

        cardRectTransform.SetParent(container, false);
        cardRectTransform.anchoredPosition = Vector2.zero;
    }
}