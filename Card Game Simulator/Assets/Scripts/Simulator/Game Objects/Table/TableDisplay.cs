using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class TableDisplay : MonoBehaviour
{
    [SerializeField] private GameObject tableSurface;
    public Table Table { get; private set; }

    public void Setup(Table table)
    {
        LoggerReferences.Instance.TableDisplayLogger.LogMethod();
        Table = table;
        updateTableDisplay();
    }

    private void updateTableDisplay()
    {
        LoggerReferences.Instance.TableDisplayLogger.LogMethod();
        resizeTable();
        loadSurfaceImage();
    }

    private void resizeTable()
    {
        LoggerReferences.Instance.TableDisplayLogger.LogMethod();
        Vector2 tableSize = new Vector2(
            Table.TableData.Width,
            Table.TableData.Height
        );
        gameObject.GetComponent<RectTransform>().sizeDelta = tableSize;
    }

    private void loadSurfaceImage()
    {
        LoggerReferences.Instance.TableDisplayLogger.LogMethod();
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

        // TODO implement loading images from the game template folder
        Sprite surfaceSprite = Resources.Load<Sprite>(imagePath);
        if (surfaceSprite != null)
        {
            surfaceImage.sprite = surfaceSprite;
        }
        else
        {
            Debug.LogWarning($"Could not load table surface sprite in path: {imagePath}");
            surfaceImage.color = Color.green;
        }
    }
}