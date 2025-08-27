using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class TableDisplay : MonoBehaviour
{
    [SerializeField] private GameObject tableSurface;
    [SerializeField] private Sprite fallbackTableSurfaceSprite;
    public Table Table { get; private set; }

    public void Setup(Table table)
    {
        Table = table;
        updateTableDisplay();
    }

    private void updateTableDisplay()
    {
        resizeTable();
        loadSurfaceImage();
    }

    private void resizeTable()
    {
        Vector2 tableSize = new Vector2(
            Table.TableData.Width,
            Table.TableData.Height
        );
        gameObject.GetComponent<RectTransform>().sizeDelta = tableSize;
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

        string imagePath = Table.TableData.SurfaceImagePath;
        if (string.IsNullOrEmpty(imagePath))
        {
            Debug.LogWarning("Surface image path is empty or null");
        }
        
        SimulatorImageLoader.LoadImageLocalPath(imagePath, surfaceImage, fallbackTableSurfaceSprite);
    }
}