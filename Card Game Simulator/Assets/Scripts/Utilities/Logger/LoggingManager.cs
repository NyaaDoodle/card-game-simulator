using System.Collections.Generic;
using UnityEngine;

public sealed class LoggingManager : MonoBehaviour
{
    public static LoggingManager Instance { get; private set; }

    private readonly Dictionary<string, TraceLogger> loggers = new Dictionary<string, TraceLogger>();

    private void Awake()
    {
        initializeInstance();
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

    public TraceLogger GetLogger(string loggerName, bool enabledAtStart = false)
    {
        if (loggers.TryGetValue(loggerName, out TraceLogger logger))
        {
            return logger;
        }
        else
        {
            TraceLogger newLogger = new TraceLogger();
            newLogger.IsEnabled = enabledAtStart;
            loggers.Add(loggerName, newLogger);
            return newLogger;
        }
    }

    public void EnableLogger(string loggerName, bool enabled)
    {
        if (loggers.TryGetValue(loggerName, out TraceLogger logger))
        {
            logger.IsEnabled = enabled;
        }
    }
}