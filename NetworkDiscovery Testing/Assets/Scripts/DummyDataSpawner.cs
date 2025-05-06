using System;
using System.Collections;
using Mirror;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class DummyDataSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject dummyDataObjectPrefab;
    [SerializeField] private BackendConnectionInputFieldsScript inputFieldsObject;
    private string serverName;
    private int port;
    private string fetchUri;
    private int id;

    public override void OnStartServer()
    {
        base.OnStartServer();
        getInputFieldsValues();
        StartCoroutine(FetchDataAndSpawnDataObject());
    }

    private void getInputFieldsValues()
    {
        serverName = inputFieldsObject.IpText;
        port = int.Parse(inputFieldsObject.PortText);
        fetchUri = inputFieldsObject.UriText;
        id = int.Parse(inputFieldsObject.IdText);
    }

    [Server]
    private IEnumerator FetchDataAndSpawnDataObject()
    {
        string currentRequestUrl = $"http://{serverName}:{port}{fetchUri}/{id}";
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