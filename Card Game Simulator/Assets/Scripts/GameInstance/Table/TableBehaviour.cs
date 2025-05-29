using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class TableBehaviour : MonoBehaviour
{
    [Header("Table Components")]
    [SerializeField] private GameObject tableSurface;
    [SerializeField] private GameObject tableBorder;

    [Header("Border Settings")]
    [SerializeField] private float borderWidth = 10f;
    [SerializeField] private Color borderColor = Color.black;

    private TableData tableData;
    private RectTransform tableRectTransform;

    public TableData TableData => tableData;

    public void Awake()
    {
        tableRectTransform = GetComponent<RectTransform>();
    }

    public void Initialize(TableData data)
    {
        tableData = data;
        updateTableDisplay();
    }

    public Vector2 GetTableSize()
    {
        return tableRectTransform.sizeDelta;
    }

    public bool IsPositionOnTable(Vector2 positionOnCanvas)
    {
        Vector2 tableSize = GetTableSize();
        float halfWidth = tableSize.x / 2f;
        float halfHeight = tableSize.y / 2f;
        bool isWithinHorizontalTableBounds = positionOnCanvas.x >= -halfWidth && positionOnCanvas.x <= halfWidth;
        bool isWithinVerticalTableBounds = positionOnCanvas.y >= -halfHeight && positionOnCanvas.y <= halfHeight;
        return isWithinHorizontalTableBounds && isWithinVerticalTableBounds;
    }

    private void updateTableDisplay()
    {
        if (tableData == null) return;
        (Vector2 surfaceSize, Vector2 borderSize) = getSurfaceAndBorderSizes();
        setupTableContainer(borderSize);
        setupTableSurface(surfaceSize);
        setupTableBorder(borderSize);
        loadSurfaceImage();
    }

    private Tuple<Vector2, Vector2> getSurfaceAndBorderSizes()
    {
        Vector2 surfaceSize = new Vector2(
            tableData.Width * UIConstants.CanvasScaleFactor,
            tableData.Height * UIConstants.CanvasScaleFactor
        );
        Vector2 borderSize = new Vector2(
            surfaceSize.x + (borderWidth * 2),
            surfaceSize.y + (borderWidth * 2)
        );
        return new Tuple<Vector2, Vector2>(surfaceSize, borderSize);
    }

    private void setupTableContainer(Vector2 tableSize)
    {
        tableRectTransform.sizeDelta = tableSize;
        tableRectTransform.anchoredPosition = Vector2.zero;
    }

    private void setupTableSurface(Vector2 surfaceSize)
    {
        if (tableSurface != null)
        {
            RectTransform rectTransform = tableSurface.GetComponent<RectTransform>();
            rectTransform.sizeDelta = surfaceSize;
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }

    private void setupTableBorder(Vector2 borderSize)
    {
        if (tableBorder != null)
        {
            RectTransform rectTransform = tableBorder.GetComponent<RectTransform>();
            rectTransform.sizeDelta = borderSize;
            rectTransform.anchoredPosition = Vector2.zero;

            Image borderImage = tableBorder.GetComponent<Image>();
            borderImage.color = borderColor;
        }
    }

    private void loadSurfaceImage()
    {
        Image surfaceImage = tableSurface.GetComponent<Image>();
        if (surfaceImage != null && !string.IsNullOrEmpty(tableData.SurfaceImagePath))
        {
            Sprite surfaceSprite = Resources.Load<Sprite>(tableData.SurfaceImagePath);
            if (surfaceSprite != null)
            {
                surfaceImage.sprite = surfaceSprite;
            }
            else
            {
                Debug.LogWarning($"Could not load table surface sprite: {tableData.SurfaceImagePath}");
                // Default to green table color
                surfaceImage.color = new Color(0.2f, 0.6f, 0.2f, 1f); 
            }
        }
    }
}
