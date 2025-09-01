using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class ContentServerAPIManager : MonoBehaviour
{
    public static ContentServerAPIManager Instance { get; private set; }
    public bool IsReady { get; private set; }

    private void Awake()
    {
        initializeInstance();
        onReady();
    }

    private void initializeInstance()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private string getServerURL(string serverIP, int serverPort)
    {
        return $"http://{serverIP}:{serverPort}";
    }

    public void GetAvailableGameTemplates(string serverIP, int serverPort, Action<List<string>> onSuccessAction, Action<string> onErrorAction)
    {
        StartCoroutine(getAvailableGameTemplatesCoroutine(serverIP, serverPort, onSuccessAction, onErrorAction));
    }

    private IEnumerator getAvailableGameTemplatesCoroutine(
        string serverIP,
        int serverPort,
        Action<List<string>> onSuccessAction,
        Action<string> onErrorAction)
    {
        string requestURL = $"{getServerURL(serverIP, serverPort)}/templates";
        Debug.Log($"Requesting available game templates from {requestURL}");
        using (UnityWebRequest webRequest = UnityWebRequest.Get(requestURL))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string responseJson = webRequest.downloadHandler.text;
                try
                {
                    List<string> templateIds = JsonConvert.DeserializeObject<List<string>>(responseJson);
                    Debug.Log($"Successfully fetched {templateIds.Count} available templates");
                    onSuccessAction?.Invoke(templateIds);
                }
                catch (Exception e)
                {
                    string errorMessage = $"Failed to parse available templates JSON: {e.Message}";
                    onErrorAction?.Invoke(errorMessage);
                }
            }
            else
            {
                string errorMessage = $"Error fetching game templates: {webRequest.error}";
                onErrorAction?.Invoke(errorMessage);
            }
        }
    }

    public void GetGameTemplateData(
        string serverIP,
        int serverPort,
        string templateId,
        Action<GameTemplate> onSuccessAction,
        Action<string> onErrorAction)
    {
        StartCoroutine(getGameTemplateDataCoroutine(serverIP, serverPort, templateId, onSuccessAction, onErrorAction));
    }
    
    private IEnumerator getGameTemplateDataCoroutine(
        string serverIP,
        int serverPort,
        string templateId,
        Action<GameTemplate> onSuccessAction,
        Action<string> onErrorAction)
    {
        string requestURL = $"{getServerURL(serverIP, serverPort)}/templates/{templateId}";
        Debug.Log($"Requesting game template {templateId} data file from {requestURL}");
        using (UnityWebRequest webRequest = UnityWebRequest.Get(requestURL))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string responseJson = webRequest.downloadHandler.text;
                try
                {
                    GameTemplate gameTemplate = JsonConvert.DeserializeObject<GameTemplate>(responseJson);
                    Debug.Log($"Successfully fetched and parsed game template {gameTemplate.Id} data file");
                    onSuccessAction?.Invoke(gameTemplate);
                }
                catch (Exception e)
                {
                    string errorMessage = $"Failed to parse game template data file: {e.Message}";
                    onErrorAction?.Invoke(errorMessage);
                }
                
            }
            else
            {
                string errorMessage = $"Failed fetching game template data file: {webRequest.error}";
                onErrorAction?.Invoke(errorMessage);
            }
        }
    }

    public void GetImageTexture(
        string serverIP,
        int serverPort,
        string imageFilename,
        string templateId,
        Action<Texture2D> onSuccessAction,
        Action<string> onErrorAction)
    {
        string imagePath = $"images/{templateId}/{imageFilename}";
        StartCoroutine(getTextureCoroutine(serverIP, serverPort, imagePath, onSuccessAction, onErrorAction));
    }

    private IEnumerator getTextureCoroutine(
        string serverIP,
        int serverPort,
        string imagePath,
        Action<Texture2D> onSuccessAction,
        Action<string> onErrorAction)
    {
        string requestURL = $"{getServerURL(serverIP, serverPort)}/{imagePath}";
        Debug.Log($"Requesting texture from {requestURL}");
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(requestURL))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(webRequest);
                Debug.Log($"Successfully downloaded texture from {requestURL}");
                onSuccessAction?.Invoke(texture);
            }
            else
            {
                string errorMessage = $"Error fetching texture: {webRequest.error}";
                onErrorAction?.Invoke(errorMessage);
            }
        }
    }

    private void onReady()
    {
        IsReady = true;
    }
}
