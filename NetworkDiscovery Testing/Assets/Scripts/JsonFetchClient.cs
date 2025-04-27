using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class JsonFetchClient : MonoBehaviour
{
    [SerializeField] private string serverName = "localhost";
    [SerializeField] private int port = 8080;
    [SerializeField] private string fetchUri = "/templates";
    private string currentRequestUrl = "";
    private DummyData currentFetchedData = null;

    public string ServerName => serverName;
    public int Port => port;
    public string FetchUri => fetchUri;
    public string LastRequestUrl => currentRequestUrl;

    public event Action<DummyData> DataFetched;

    public void GetData(int id)
    {
        StartCoroutine(fetchData(id));
    }

    private IEnumerator fetchData(int id)
    {
        currentRequestUrl = $"http://{serverName}:{port}{fetchUri}/{id}";
        using (UnityWebRequest request = UnityWebRequest.Get(currentRequestUrl))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                Debug.Log($"Successfully fetched: {jsonResponse}");
                currentFetchedData = processJsonData<DummyData>(jsonResponse);
                OnDataFetched();
            }
            else
            {
                Debug.Log($"Error attempting to fetch data from {currentRequestUrl}: {request.error}");
            }
        }
    }

    private T processJsonData<T>(string jsonData)
    {
        return JsonConvert.DeserializeObject<T>(jsonData);
    }

    protected virtual void OnDataFetched()
    {
        DataFetched?.Invoke(currentFetchedData);
    }
}