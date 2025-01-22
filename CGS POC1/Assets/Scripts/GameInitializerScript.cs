using System.Collections.Generic;
using UnityEngine;

public class GameInitializerScript : MonoBehaviour
{
    [SerializeField] private GameTemplateObject gameTemplateObject;
    [SerializeField] private GameObject tablePrefab;
    [SerializeField] private GameObject deckPrefab;

    private void Start()
    {
        spawnTable();
        spawnDecks();
    }

    private void spawnTable()
    {
        GameObject table = Instantiate(tablePrefab, transform, true);
        int tableLength = gameTemplateObject.TableLength;
        int tableWidth = gameTemplateObject.TableWidth;
        table.transform.localScale = new Vector3(tableLength, tableWidth, 1);
    }

    private void spawnDecks()
    {
        List<DeckInfo> deckInfoList = gameTemplateObject.DeckInfoList;

        foreach (DeckInfo deckInfo in deckInfoList)
        {
            
        }
    }
}
