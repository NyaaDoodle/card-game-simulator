using Mirror;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class TableDisplay : NetworkBehaviour
{
    [Header("Table Components")]
    [SerializeField] private GameObject tableSurface;

    [SyncVar(hook = nameof(onTableChanged))] private Table table;

    public override void OnStartClient()
    {
        attachToTableContainer();
    }

    [Server]
    public void Setup(Table table)
    {
        Debug.Log("Reached Setup");
        this.table = table;
    }

    private void onTableChanged(Table oldValue, Table newValue)
    {
        Debug.Log("Reached onTableChanged");
        Debug.Log($"{table.TableData.Width}, {table.TableData.Height}, {table.TableData.SurfaceImagePath}");
        updateTableDisplay();
    }

    private void updateTableDisplay()
    {
        Debug.Log("Reached updateTableDisplay");
        if (table == null)
        {
            Debug.LogError("Table is null when attempting to update table display");
            return;
        }
        resizeTable();
        loadSurfaceImage();
    }

    private void attachToTableContainer()
    {
        gameObject.transform.SetParent(ContainerReferences.Instance.TableContainer, false);
    }

    private void resizeTable()
    {
        Debug.Log("Reached resizeTable");
        Vector2 tableSize = new Vector2(
            table.TableData.Width,
            table.TableData.Height
        );
        gameObject.GetComponent<RectTransform>().sizeDelta = tableSize;
    }

    private void loadSurfaceImage()
    {
        Debug.Log("Reached loadSurfaceImage");
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

        if (string.IsNullOrEmpty(table.TableData.SurfaceImagePath))
        {
            Debug.LogWarning("Surface image path is empty or null");
        }

        // TODO implement loading images from the game template folder
        Sprite surfaceSprite = Resources.Load<Sprite>(table.TableData.SurfaceImagePath);
        if (surfaceSprite != null)
        {
            surfaceImage.sprite = surfaceSprite;
        }
        else
        {
            Debug.LogWarning($"Could not load table surface sprite in path: {table.TableData.SurfaceImagePath}");
            surfaceImage.color = Color.green;
        }
    }
}