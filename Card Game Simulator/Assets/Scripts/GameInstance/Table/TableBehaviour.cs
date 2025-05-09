using UnityEngine;

public class TableBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject surfaceObject;
    [SerializeField] private GameObject borderObject;
    private SpriteRenderer surfaceSpriteRenderer;
    private float tableWidth;
    private float tableHeight;

    public void Awake()
    {
        surfaceSpriteRenderer = surfaceObject.GetComponent<SpriteRenderer>();
    }

    public void SetTableSize(float width, float height)
    {
        tableWidth = width;
        tableHeight = height;
        recalculateTableObjectsScale();
    }

    public void SetSurfaceSprite(Sprite surfaceSprite)
    {
        surfaceSpriteRenderer.sprite = surfaceSprite;
        recalculateTableObjectsScale();
    }

    private void recalculateTableObjectsScale()
    {
        recalculateSurfaceScale();
        recalculateBorderScale();
    }

    private void recalculateSurfaceScale()
    {
        ResizeSprite.RecalculateSpriteScale(surfaceObject, tableWidth, tableHeight);
    }

    private void recalculateBorderScale()
    {
        const float borderToTableScaleFactor = 1.05f;
        float borderWidth = tableWidth * borderToTableScaleFactor;
        float borderHeight = tableHeight * borderToTableScaleFactor;
        ResizeSprite.RecalculateSpriteScale(borderObject, borderWidth, borderHeight);
    }
}
