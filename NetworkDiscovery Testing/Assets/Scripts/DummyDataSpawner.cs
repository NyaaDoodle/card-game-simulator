using System;
using System.Collections;
using Mirror;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class DummyDataSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject dummyDataObjectPrefab;

    [SerializeField] private string serverName = "localhost";
    [SerializeField] private int port = 8080;
    [SerializeField] private string fetchUri = "/templates";
    [SerializeField] private int id = 1;
    private string currentRequestUrl = "";

    public override void OnStartServer()
    {
        base.OnStartServer();
        StartCoroutine(FetchDataAndSpawnDataObject());
    }

    [Server]
    private IEnumerator FetchDataAndSpawnDataObject()
    {
        currentRequestUrl = $"http://{serverName}:{port}{fetchUri}/{id}";
        using (UnityWebRequest request = UnityWebRequest.Get(currentRequestUrl))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                Debug.Log($"Successfully fetched: {jsonResponse}");
                DummyData fetchedDummyData = parseJsonData<DummyData>(jsonResponse);
                spawnDummyDataOnNetwork(fetchedDummyData);
            }
            else
            {
                Debug.Log($"Error attempting to fetch data from {currentRequestUrl}: {request.error}");
            }
        }
    }

    private T parseJsonData<T>(string jsonData)
    {
        return JsonConvert.DeserializeObject<T>(jsonData);
    }

    [Server]
    private void spawnDummyDataOnNetwork(DummyData fetchedDummyData)
    {
        GameObject dummyDataGameObject = Instantiate(dummyDataObjectPrefab);
        dummyDataGameObject.GetComponent<DummyDataObject>().setDummyData(fetchedDummyData);
        NetworkServer.Spawn(dummyDataGameObject);
    }
}