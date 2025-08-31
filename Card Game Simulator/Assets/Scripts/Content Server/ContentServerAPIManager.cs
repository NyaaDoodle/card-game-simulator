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

    private string cloudBackendServerIp;
    private int cloudBackendServerPort;
    private string serverURL;

    public string CloudBackendServerIp
    {
        get
        {
            return cloudBackendServerIp;
        }
        set
        {
            cloudBackendServerIp = value;
            SetServerToCloudBackendServer();
        }
    }

    public int CloudBackendServerPort
    {
        get
        {
            return cloudBackendServerPort;
        }
        set
        {
            cloudBackendServerPort = value;
            SetServerToCloudBackendServer();
        }
    }

    private void Awake()
    {
        initializeInstance();
    }

    private void Start()
    {
        initializeValues();
        onReady();
    }

    private void initializeValues()
    {
        CloudBackendServerIp = PlayerPrefsManager.Instance.CloudBackendIP;
        CloudBackendServerPort = PlayerPrefsManager.Instance.CloudBackendPort;
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

    public void SetServerToCloudBackendServer()
    {
        SetServerURL(CloudBackendServerIp, CloudBackendServerPort);
    }

    public void SetServerURL(string serverIP, int serverPort)
    {
        serverURL = $"http://{serverIP}:{serverPort}";
    }

    public void GetAvailableGameTemplates(Action<List<string>> onSuccessAction, Action<string> onErrorAction)
    {
        StartCoroutine(getAvailableGameTemplatesCoroutine(onSuccessAction, onErrorAction));
    }

    private IEnumerator getAvailableGameTemplatesCoroutine(
        Action<List<string>> onSuccessAction,
        Action<string> onErrorAction)
    {
        string requestURL = $"{serverURL}/templates";
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
        string templateId,
        Action<GameTemplate> onSuccessAction,
        Action<string> onErrorAction)
    {
        StartCoroutine(getGameTemplateDataCoroutine(templateId, onSuccessAction, onErrorAction));
    }

    private IEnumerator getGameTemplateDataCoroutine(
        string templateId,
        Action<GameTemplate> onSuccessAction,
        Action<string> onErrorAction)
    {
        string requestURL = $"{serverURL}/templates/{templateId}";
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
        string imageFilename,
        string templateId,
        Action<Texture2D> onSuccessAction,
        Action<string> onErrorAction)
    {
        string imagePath = $"images/{templateId}/{imageFilename}";
        StartCoroutine(getTextureCoroutine(imagePath, onSuccessAction, onErrorAction));
    }

    private IEnumerator getTextureCoroutine(
        string imagePath,
        Action<Texture2D> onSuccessAction,
        Action<string> onErrorAction)
    {
        string requestURL = $"{serverURL}/{imagePath}";
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
