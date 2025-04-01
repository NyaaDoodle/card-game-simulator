using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ResizeSpriteRenderScript : MonoBehaviour
{
    private float width = 1f;
    private float height = 1f;
    private SpriteRenderer spriteRenderer;
    private float originalWidth;
    private float originalHeight;

    public float Width
    {
        get => width;
        set
        {
            width = value;
            updateSize();
        }
    }

    public float Height
    {
        get => height;
        set
        {
            height = value;
            updateSize();
        }
    }

    void Start()
    {
        initializeFields();
        updateSize();
    }

    private void initializeFields()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Sprite originalSprite = spriteRenderer.sprite;
        originalWidth = originalSprite.bounds.size.x;
        originalHeight = originalSprite.bounds.size.y;
    }

    private void updateSize()
    {
        if (spriteRenderer == null) return;
        float scaleX = width / originalWidth;
        float scaleY = height / originalHeight;
        spriteRenderer.transform.localScale = new Vector3(scaleX, scaleY, 1f);
    }
}
