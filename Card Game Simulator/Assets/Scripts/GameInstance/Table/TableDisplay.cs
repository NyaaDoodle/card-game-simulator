using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(TableState))]
public class TableDisplay : MonoBehaviour
{
    [Header("Table Components")]
    [SerializeField] private GameObject tableSurface;
    [SerializeField] private GameObject tableBorder;

    [Header("Border Settings")]
    [SerializeField] private float borderWidth;
    [SerializeField] private Color borderColor;

    private TableState tableState;
    private TableData tableData;

    void Awake()
    {
        tableState = GetComponent<TableState>();
    }

    void Start()
    {
        tableState.TableDataChanged += onTableDataChanged;
        if (tableState.TableData != null)
        {
            tableData = tableState.TableData;
            updateTableDisplay();
        }
    }

    void OnDestroy()
    {
        tableState.TableDataChanged -= onTableDataChanged;
    }

    private void onTableDataChanged(TableData newTableData)
    {
        tableData = newTableData;
        updateTableDisplay();
    }

    private void updateTableDisplay()
    {
        if (tableData == null)
        {
            Debug.LogError("Table data is null when attempting to update table display");
            return;
        }
        (Vector2 surfaceSize, Vector2 borderSize) = getSurfaceAndBorderSizes();
        setupTableContainer(borderSize);
        setupTableSurface(surfaceSize);
        setupTableBorder(borderSize);
        loadSurfaceImage();
    }

    private Tuple<Vector2, Vector2> getSurfaceAndBorderSizes()
    {
        Vector2 surfaceSize = new Vector2(
            tableData.Width,
            tableData.Height
        );
        Vector2 borderSize = new Vector2(
            surfaceSize.x + (borderWidth * 2),
            surfaceSize.y + (borderWidth * 2)
        );
        return new Tuple<Vector2, Vector2>(surfaceSize, borderSize);
    }

    private void setupTableContainer(Vector2 tableSize)
    {
        UIUtilities.ResizeAndCenterAnchor(this.gameObject, tableSize);
    }

    private void setupTableSurface(Vector2 surfaceSize)
    {
        if (tableSurface == null)
        {
            Debug.LogError("Table surface object is null");
            return;
        }

        UIUtilities.ResizeAndCenterAnchor(tableSurface, surfaceSize);
    }

    private void setupTableBorder(Vector2 borderSize)
    {
        if (tableBorder == null)
        {
            Debug.LogError("Table border object is null");
            return;
        }

        UIUtilities.ResizeAndCenterAnchor(tableBorder, borderSize);
        Image borderImage = tableBorder.GetComponent<Image>();
        borderImage.color = borderColor;
    }

    private void loadSurfaceImage()
    {
        if (tableSurface == null)
        {
            Debug.LogError("Table surface object is null");
            return;
        }

        Image surfaceImage = tableSurface.GetComponent<Image>();
        if (surfaceImage == null)
        {
            Debug.LogError("Surface image object doesn't contain Image component");
            return;
        }

        if (string.IsNullOrEmpty(tableData.SurfaceImagePath))
        {
            Debug.LogWarning("Surface image path is empty or null");
        }

        // TODO implement loading images from the game template folder
        Sprite surfaceSprite = Resources.Load<Sprite>(tableData.SurfaceImagePath);
        if (surfaceSprite != null)
        {
            surfaceImage.sprite = surfaceSprite;
        }
        else
        {
            Debug.LogWarning($"Could not load table surface sprite in path: {tableData.SurfaceImagePath}");
            surfaceImage.color = Color.green;
        }
    }
}