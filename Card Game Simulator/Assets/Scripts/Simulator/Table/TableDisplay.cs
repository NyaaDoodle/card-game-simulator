using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class TableDisplay : MonoBehaviour
{
    [Header("Table Components")]
    [SerializeField] private GameObject tableSurface;

    public Table Table { get; private set; }

    public void Setup(Table table)
    {
        Table = table;
        if (Table != null)
        {
            updateTableDisplay();
        }
    }

    private void updateTableDisplay()
    {
        if (Table == null)
        {
            Debug.LogError("Table is null when attempting to update table display");
            return;
        }
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

        if (string.IsNullOrEmpty(Table.TableData.SurfaceImagePath))
        {
            Debug.LogWarning("Surface image path is empty or null");
        }

        // TODO implement loading images from the game template folder
        Sprite surfaceSprite = Resources.Load<Sprite>(Table.TableData.SurfaceImagePath);
        if (surfaceSprite != null)
        {
            surfaceImage.sprite = surfaceSprite;
        }
        else
        {
            Debug.LogWarning($"Could not load table surface sprite in path: {Table.TableData.SurfaceImagePath}");
            surfaceImage.color = Color.green;
        }
    }
}