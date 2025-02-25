using UnityEngine;

public class GameInstanceObjectsSpawner : MonoBehaviour
{
    [SerializeField]
    private GameTemplateDataContainer gameTemplateDataContainer;
    [SerializeField] 
    private GameObject table;
    [SerializeField] 
    private GameObject decks;
    [SerializeField] 
    private GameObject cardPlacementLocations;
    [SerializeField] 
    private GameObject playerHands;

    void Start()
    {
        spawnTable();
        spawnDecks();
        spawnCardPlacementLocations();
        spawnPlayerHands();
    }

    private void spawnTable()
    {
        table.SetActive(true);
        Transform tableTransform = table.transform;
        TableData tableData = gameTemplateDataContainer.TableData;
        // TODO this does not represent the size of the table, but its scale to the original sprite.
        Vector3 tableScaleVector = new Vector3(tableData.Width, tableData.Height, 1);
        tableTransform.localScale = tableScaleVector;
    }

    private void spawnDecks() {}
    private void spawnCardPlacementLocations() {}
    private void spawnPlayerHands() {}
}
