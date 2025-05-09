using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class TableSpawnTest : MonoBehaviour
{
    public GameObject TablePrefab;
    public float Width;
    public float Height;
    public Sprite SurfaceSprite;
    private GameObject tableObject = null;
    private TableBehaviour tableBehaviour = null;

    void Start()
    {
        tableObject = Instantiate(TablePrefab);
        tableBehaviour = tableObject.GetComponent<TableBehaviour>();
        changeTableProperties();
    }

    void OnValidate()
    {
        if (tableObject != null && tableBehaviour != null)
        {
            changeTableProperties();
        }
    }

    private void changeTableProperties()
    {
        tableBehaviour.SetTableSize(Width, Height);
        tableBehaviour.SetSurfaceSprite(SurfaceSprite);
    }
}
