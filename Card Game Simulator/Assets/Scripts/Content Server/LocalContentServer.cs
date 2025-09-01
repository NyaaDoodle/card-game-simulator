using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class LocalContentServer : MonoBehaviour
{
    public static LocalContentServer Instance { get; private set; }
    public bool IsReady { get; private set; }

    private HttpListener httpListener;
    private Task serverTask;
    private GameTemplate currentGameTemplate;

    private void Awake()
    {
        initializeInstance();
        onReady();
    }

    private void onReady()
    {
        IsReady = true;
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

    public void StartServer()
    {
        if (httpListener != null && httpListener.IsListening)
        {
            Debug.LogWarning("Local content server is running");
            return;
        }

        if (SimulatorGlobalData.Instance.CurrentPlayingGameTemplate != null)
        {
            currentGameTemplate = SimulatorGlobalData.Instance.CurrentPlayingGameTemplate.Value;
        }
        else
        {
            Debug.LogError("Local content server started with null current playing game template!");
            return;
        }

        prepareHttpListener();
    }

    public void StopServer()
    {
        if (httpListener == null || !httpListener.IsListening) return;
        Debug.Log("Stopping local content server");
        httpListener.Stop();
        httpListener.Close();
        httpListener = null;
    }

    private void prepareHttpListener()
    {
        int port = PlayerPrefsManager.Instance.LocalContentServerPort;
        httpListener = new HttpListener();
        httpListener.Prefixes.Add($"http://*:{port}/");
        Debug.Log($"Starting local content server on port {port} for game template {currentGameTemplate.Id}");
        httpListener.Start();
        serverTask = Task.Run(listenLoop);
    }

    private async Task listenLoop()
    {
        while (httpListener != null && httpListener.IsListening)
        {
            try
            {
                HttpListenerContext context = await httpListener.GetContextAsync();
                processRequest(context);
            }
            catch (HttpListenerException)
            {
                // Expected to be thrown when httpListener is stopped.
                break;
            }
            catch (ObjectDisposedException)
            {
                // Expected to be thrown when GetContextAsync is waiting but httpListener has been closed.
                break;
            }
            catch (Exception e)
            {
                Debug.LogError($"Local content server error: {e.Message} {e.GetType()}");
            }
        }
    }

    private void processRequest(HttpListenerContext context)
    {
        HttpListenerRequest request = context.Request;
        HttpListenerResponse response = context.Response;
        
        string urlAbsolutePath = request.Url.AbsolutePath;
        Debug.Log($"Local content server received request at {urlAbsolutePath}");
        char[] urlDelimiters = new char[] { '/' };
        string[] urlParts = urlAbsolutePath.Split(urlDelimiters, StringSplitOptions.RemoveEmptyEntries);
        if (urlParts.Length <= 1)
        {
            sendErrorResponse(response, 400, "Invalid URL structure");
            return;
        }
        
        string requestTemplateId = urlParts[1];
        if (requestTemplateId != currentGameTemplate.Id)
        {
            sendErrorResponse(response, 403, "Invalid or not correct current game template ID");
            return;
        }
        
        string requestDirectory = urlParts[0];
        string filePath = "";
        if (requestDirectory == DataDirectoryManager.TemplatesDirectoryName)
        {
            filePath = Path.Combine(
                DataDirectoryManager.Instance.TemplatesDirectoryPath,
                requestTemplateId,
                DataDirectoryManager.TemplateDataFilename);
            
        }
        else
        {
            filePath = Path.Combine(DataDirectoryManager.Instance.DataDirectoryPath, urlAbsolutePath.Substring(1));
        }
        
        if (File.Exists(filePath))
        {
            sendFileResponse(response, filePath);
        }
        else
        {
            Debug.LogWarning($"Local content server could not find requested file at {filePath}");
            sendErrorResponse(response, 404, "Requested file not found");
        }
    }

    private void sendFileResponse(HttpListenerResponse response, string filePath)
    {
        try
        {
            byte[] bytes = File.ReadAllBytes(filePath);
            response.StatusCode = (int)HttpStatusCode.OK;
            response.ContentType = getMimeType(Path.GetExtension(filePath));
            response.ContentLength64 = bytes.Length;
            response.OutputStream.Write(bytes, 0, bytes.Length);
            response.OutputStream.Close();
        }
        catch (Exception e)
        {
            Debug.LogError($"Local content server error sending file {filePath}: {e.Message}");
            sendErrorResponse(response, 500, "Internal server error");
        }
    }
    
    private string getMimeType(string extension)
    {
        switch (extension.ToLower())
        {
            case ".json": return "application/json";
            case ".png": return "image/png";
            case ".jpg":
            case ".jpeg": return "image/jpeg";
            default: return "application/octet-stream";
        }
    }

    private void sendErrorResponse(HttpListenerResponse response, int statusCode, string errorMessage)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(errorMessage);
        response.StatusCode = statusCode;
        response.StatusDescription = errorMessage;
        response.ContentLength64 = bytes.Length;
        response.OutputStream.Write(bytes, 0, bytes.Length);
        response.OutputStream.Close();
    }

    private void OnApplicationQuit()
    {
        StopServer();
    }
}
