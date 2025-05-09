using UnityEngine;

public class ResizeSprite
{
    public static void RecalculateSpriteScale(GameObject objectWithSprite, float width, float height)
    {
        resetObjectScale(objectWithSprite);
        SpriteRenderer objectSpriteRenderer = objectWithSprite.GetComponent<SpriteRenderer>();
        float originalWidth = objectSpriteRenderer.bounds.size.x;
        float newWidth = width / originalWidth;
        float originalHeight = objectSpriteRenderer.bounds.size.y;
        float newHeight = height / originalHeight;
        objectWithSprite.transform.localScale = new Vector3(newWidth, newHeight);
    }

    private static void resetObjectScale(GameObject objectToReset)
    {
        objectToReset.transform.localScale = Vector3.one;
    }
}
