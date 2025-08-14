using Mirror;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class TableDisplay : NetworkBehaviour
{
    [Header("Table Components")]
    [SerializeField] private GameObject tableSurface;
    private Table table;

    public override void OnStartClient()
    {
        LoggerReferences.Instance.TableDisplayLogger.LogMethod();
        base.OnStartClient();
        table = GetComponent<Table>();
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
            table.TableData.Width,
            table.TableData.Height
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

        string imagePath = table.TableData.SurfaceImagePath;
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