using UnityEngine;

public class LoggingManager : MonoBehaviour
{
    [SerializeField] private bool isLogMethodEnabled;
    [SerializeField] private bool isLogVariableEnabled;
    [SerializeField] private bool isLogListEnabled;
    [SerializeField] private bool isLogDictionaryEnabled;
    [SerializeField] private bool isGeneralLogEnabled;

    void Start()
    {
        updateLoggerSettings();
    }

    void OnValidate()
    {
        updateLoggerSettings();
    }

    private void updateLoggerSettings()
    {
        TraceLogger.IsLogMethodEnabled = isLogMethodEnabled;
        TraceLogger.IsLogVariableEnabled = isLogVariableEnabled;
        TraceLogger.IsLogListEnabled = isLogListEnabled;
        TraceLogger.IsLogDictionaryEnabled = isLogDictionaryEnabled;
        TraceLogger.IsGeneralLogEnabled = isGeneralLogEnabled;
    }
}