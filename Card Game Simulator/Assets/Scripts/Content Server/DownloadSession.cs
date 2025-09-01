using System;

public class DownloadSession
{
    public string ServerIP { get; }
    public int ServerPort { get; }
    public Action OnSuccess { get; }
    public Action<string, string> OnError { get; }
    
    private int totalOperations;
    private int completedOperations;
    private bool hasError;

    public DownloadSession(string serverIP, int serverPort, Action onSuccess, Action<string, string> onError)
    {
        ServerIP = serverIP;
        ServerPort = serverPort;
        OnSuccess = onSuccess;
        OnError = onError;
    }

    public void IncreaseTotalOperations(int amount)
    {
        totalOperations += amount;
    }

    public void CompleteOneOperation()
    {
        completedOperations++;
        if (completedOperations >= totalOperations && !hasError)
        {
            OnSuccess?.Invoke();
        }
    }

    public void ReportError(string errorMessage, string gameTemplateId)
    {
        if (!hasError)
        {
            hasError = true;
            OnError?.Invoke(errorMessage, gameTemplateId);
        }
    }
}
