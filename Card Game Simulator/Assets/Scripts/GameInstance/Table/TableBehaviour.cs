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
        resetSurfaceScale();
        float originalWidth = surfaceSpriteRenderer.bounds.size.x;
        float newWidth = tableWidth / originalWidth;
        float originalHeight = surfaceSpriteRenderer.bounds.size.y;
        float newHeight = tableHeight / originalHeight;
        surfaceObject.transform.localScale = new Vector3(newWidth, newHeight, 1);
    }

    private void resetSurfaceScale()
    {
        surfaceObject.transform.localScale = Vector3.one;
    }

    private void recalculateBorderScale()
    {
        const float borderToTableScaleFactor = 1.05f;
        float borderWidth = tableWidth * borderToTableScaleFactor;
        float borderHeight = tableHeight * borderToTableScaleFactor;
        borderObject.transform.localScale = new Vector3(borderWidth, borderHeight, 1);
    }
}
