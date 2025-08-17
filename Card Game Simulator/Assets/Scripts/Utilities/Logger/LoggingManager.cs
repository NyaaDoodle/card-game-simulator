using System;
using UnityEngine;

public class LoggingManager : MonoBehaviour
{
    public static LoggingManager Instance { get; private set; }

    public TraceLogger GameInstanceManagerLogger = new TraceLogger();

    public TraceLogger GameTemplateLoaderLogger = new TraceLogger();

    public TraceLogger CardDisplayLogger = new TraceLogger();

    public TraceLogger CardCollectionLogger = new TraceLogger();

    public TraceLogger CardCollectionDisplayLogger = new TraceLogger();

    public TraceLogger DeckLogger = new TraceLogger();

    public TraceLogger DeckDisplayLogger = new TraceLogger();

    public TraceLogger SpaceLogger = new TraceLogger();

    public TraceLogger SpaceDisplayLogger = new TraceLogger();

    public TraceLogger StackableLogger = new TraceLogger();

    public TraceLogger StackableDisplayLogger = new TraceLogger();

    public TraceLogger TableLogger = new TraceLogger();

    public TraceLogger TableDisplayLogger = new TraceLogger();

    public TraceLogger SimulatorNetworkManagerLogger = new TraceLogger();

    public TraceLogger PlayerLogger = new TraceLogger();

    public TraceLogger PlayerHandLogger = new TraceLogger();

    public TraceLogger PlayerHandDisplayLogger = new TraceLogger();
    public TraceLogger PlayerManagerLogger = new TraceLogger();

    public TraceLogger PrefabExtensionsLogger = new TraceLogger();

    public TraceLogger SelectionManagerLogger = new TraceLogger();

    public TraceLogger InteractionMenuManagerLogger = new TraceLogger();

    public TraceLogger InteractionMenuLogger = new TraceLogger();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}