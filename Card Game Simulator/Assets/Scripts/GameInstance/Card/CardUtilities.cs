using UnityEngine;

public class CardUtilities
{
    public static void AttachCardTransformAndCenter(GameObject card, Transform container)
    {
        RectTransform cardRectTransform = card.GetComponent<RectTransform>();
        if (cardRectTransform != null)
        {
            // Attaching to container
            cardRectTransform.SetParent(container, false);
            // Centering according to container
            cardRectTransform.anchoredPosition = Vector2.zero;
            cardRectTransform.localScale = Vector3.one;
            cardRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            cardRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            cardRectTransform.pivot = new Vector2(0.5f, 0.5f);
        }
    }
}