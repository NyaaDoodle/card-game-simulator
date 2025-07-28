using UnityEngine;

public class UIUtilities
{
    public static void CenterAnchor(RectTransform rectTransform)
    {
        if (rectTransform == null)
        {
            Debug.LogWarning("RectTransform is null, possibly game object doesn't contain a RectTransform component");
            return;
        }

        rectTransform.anchoredPosition = Vector2.zero;
    }

    public static void CenterAnchor(GameObject uiObject)
    {
        if (uiObject == null)
        {
            Debug.LogWarning("Game object is null");
            return;
        }
        
        RectTransform rectTransform = uiObject.GetComponent<RectTransform>();
        CenterAnchor(rectTransform);
    }

    public static void Resize(RectTransform rectTransform, Vector2 resizeVector)
    {
        if (rectTransform == null)
        {
            Debug.LogWarning("RectTransform is null, possibly game object doesn't contain a RectTransform component");
            return;
        }

        rectTransform.sizeDelta = resizeVector;
    }

    public static void ResizeAndCenterAnchor(RectTransform rectTransform, Vector2 resizeVector)
    {
        if (rectTransform == null)
        {
            Debug.LogWarning("RectTransform is null, possibly game object doesn't contain a RectTransform component");
            return;
        }

        Resize(rectTransform, resizeVector);
        CenterAnchor(rectTransform);
    }

    public static void ResizeAndCenterAnchor(GameObject uiObject, Vector2 resizeVector)
    {
        if (uiObject == null)
        {
            Debug.LogWarning("Game object is null");
            return;
        }
        
        RectTransform rectTransform = uiObject.GetComponent<RectTransform>();
        ResizeAndCenterAnchor(rectTransform, resizeVector);
    }
}