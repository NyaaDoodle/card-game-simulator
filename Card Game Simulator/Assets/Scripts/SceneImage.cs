using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SceneImage : MonoBehaviour
{
    [SerializeField] private float width = 1f;
    [SerializeField] private float height = 1f;
    private SpriteRenderer spriteRenderer;
    private float originalWidth;
    private float originalHeight;

    void Awake()
    {
        initializeFields();
        updateSize();
    }

    void OnValidate()
    {
        if (spriteRenderer == null) return;
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
