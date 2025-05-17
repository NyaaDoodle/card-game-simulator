using UnityEngine;

public class GameInstanceObjectsSpawner : MonoBehaviour
{
    //    [SerializeField] private GameTemplateDataContainer gameTemplateDataContainer;
    //    [SerializeField] private GameInstanceObjectsContainer gameInstanceObjectsContainer;
    //    [SerializeField] private GameObject tablePrefab;

    //    void Start()
    //    {
    //        spawnTable();
    //        spawnDecks();
    //        spawnCardPlacementLocations();
    //        spawnPlayerHands();
    //    }

    //    private void spawnTable()
    //    {
    //        GameObject table = Instantiate(tablePrefab, gameInstanceObjectsContainer.transform);
    //        gameInstanceObjectsContainer.Table = table;
    //        table.SetActive(true);
    //        Transform tableTransform = table.transform;
    //        TableData tableData = gameTemplateDataContainer.TableData;
    //        // TODO this does not represent the size of the table, but its scale to the original sprite.
    //        Vector3 tableScaleVector = new Vector3(tableData.Width, tableData.Height, 1);
    //        tableTransform.localScale = tableScaleVector;
    //    }

    //    private void spawnDecks()
    //    {

    //    }

    //    private void spawnCardPlacementLocations()
    //    {

    //    }

    //    private void spawnPlayerHands()
    //    {

    //    }
}
