using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class GameTemplateFetchClient : MonoBehaviour
{
    [SerializeField] private string serverName = "localhost";
    [SerializeField] private int port = 8080;
    [SerializeField] private string fetchTemplatesUri = "/templates";
    private string currentRequestUrl = "";
    private DummyTemplateData currentFetchedGameTemplateData = null;

    public string ServerName => serverName;
    public int Port => port;
    public string FetchTemplatesUri => fetchTemplatesUri;
    public string LastRequestUrl => currentRequestUrl;

    public event Action<DummyTemplateData> GameTemplateFetched;

    public void GetGameTemplateData(int templateId)
    {
        StartCoroutine(fetchGameTemplateData(templateId));
    }

    private IEnumerator fetchGameTemplateData(int templateId)
    {
        currentRequestUrl = $"http://{serverName}:{port}{fetchTemplatesUri}/{templateId}";
        using (UnityWebRequest request = UnityWebRequest.Get(currentRequestUrl))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                Debug.Log($"Successfully fetched: {jsonResponse}");
                currentFetchedGameTemplateData = processJsonData(jsonResponse);
                OnGameTemplateFetched();
            }
            else
            {
                Debug.Log($"Error attempting to fetch data from {currentRequestUrl}: {request.error}");
            }
        }
    }

    private DummyTemplateData processJsonData(string jsonData)
    {
        return JsonConvert.DeserializeObject<DummyTemplateData>(jsonData);
    }

    protected virtual void OnGameTemplateFetched()
    {
        GameTemplateFetched?.Invoke(currentFetchedGameTemplateData);
    }
}