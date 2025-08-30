using System;

public class DownloadSession
{
    public int TotalOperations { get; set; }
    public int CompletedOperations { get; set; }
    public bool HasError { get; set; }
    public Action OnSuccess { get; set; }
    public Action<string, string> OnError { get; set; }

    public void CompleteOneOperation()
    {
        CompletedOperations++;
        if (CompletedOperations >= TotalOperations && !HasError)
        {
            OnSuccess?.Invoke();
        }
    }

    public void ReportError(string errorMessage, string gameTemplateId)
    {
        if (!HasError)
        {
            HasError = true;
            OnError?.Invoke(errorMessage, gameTemplateId);
        }
    }
}
