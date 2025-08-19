using UnityEngine;

public class LoggingManager : MonoBehaviour
{
    public static LoggingManager Instance { get; private set; }
    
    public TraceLogger GameInstanceManagerLogger { get; private set; }
    public bool EnableGameInstanceManagerLogger;
    
    public TraceLogger GameTemplateLoaderLogger { get; private set; }
    public bool EnableGameTemplateLoaderLogger;
    
    public TraceLogger CardDisplayLogger { get; private set; }
    public bool EnableCardDisplayLogger;
    
    public TraceLogger CardCollectionLogger { get; private set; }
    public bool EnableCardCollectionLogger;
    
    public TraceLogger CardCollectionDisplayLogger { get; private set; }
    public bool EnableCardCollectionDisplayLogger;
    
    public TraceLogger DeckLogger { get; private set; }
    public bool EnableDeckLogger;
    
    public TraceLogger DeckDisplayLogger { get; private set; }
    public bool EnableDeckDisplayLogger;
    
    public TraceLogger SpaceLogger { get; private set; }
    public bool EnableSpaceLogger;
    
    public TraceLogger SpaceDisplayLogger { get; private set; }
    public bool EnableSpaceDisplayLogger;
    
    public TraceLogger StackableLogger { get; private set; }
    public bool EnableStackableLogger;
    
    public TraceLogger StackableDisplayLogger { get; private set; }
    public bool EnableStackableDisplayLogger;
    
    public TraceLogger TableLogger { get; private set; }
    public bool EnableTableLogger;
    
    public TraceLogger TableDisplayLogger { get; private set; }
    public bool EnableTableDisplayLogger;
    
    public TraceLogger SimulatorNetworkManagerLogger { get; private set; }
    public bool EnableSimulatorNetworkManagerLogger;
    
    public TraceLogger PlayerLogger { get; private set; }
    public bool EnablePlayerLogger;
    
    public TraceLogger PlayerHandLogger { get; private set; }
    public bool EnablePlayerHandLogger;
    
    public TraceLogger PlayerHandDisplayLogger { get; private set; }
    public bool EnablePlayerHandDisplayLogger;
    
    public TraceLogger PlayerManagerLogger { get; private set; }
    public bool EnablePlayerManagerLogger;
    
    public TraceLogger PrefabExtensionsLogger { get; private set; }
    public bool EnablePrefabExtensionsLogger;
    
    public TraceLogger SelectionManagerLogger { get; private set; }
    public bool EnableSelectionManagerLogger;
    
    public TraceLogger InteractionMenuManagerLogger { get; private set; }
    public bool EnableInteractionMenuManagerLogger;
    
    public TraceLogger InteractionMenuLogger { get; private set; }
    public bool EnableInteractionMenuLogger;
    
    public TraceLogger GameTemplateSaveLoadLogger { get; private set; }
    public bool EnableGameTemplateSaveLoadLogger;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            populateLoggerFields();
        }
    }

    private void Start()
    {
        setLoggersIsEnabled();
    }

    private void populateLoggerFields()
    {
        GameInstanceManagerLogger = new TraceLogger();
        GameTemplateLoaderLogger = new TraceLogger();
        CardDisplayLogger = new TraceLogger();
        CardCollectionLogger = new TraceLogger();
        CardCollectionDisplayLogger = new TraceLogger();
        DeckLogger = new TraceLogger();
        DeckDisplayLogger = new TraceLogger();
        SpaceLogger = new TraceLogger();
        SpaceDisplayLogger = new TraceLogger();
        StackableLogger = new TraceLogger();
        StackableDisplayLogger = new TraceLogger();
        TableLogger = new TraceLogger();
        TableDisplayLogger = new TraceLogger();
        SimulatorNetworkManagerLogger = new TraceLogger();
        PlayerLogger = new TraceLogger();
        PlayerHandLogger = new TraceLogger();
        PlayerHandDisplayLogger = new TraceLogger();
        PlayerManagerLogger = new TraceLogger();
        PrefabExtensionsLogger = new TraceLogger();
        SelectionManagerLogger = new TraceLogger();
        InteractionMenuManagerLogger = new TraceLogger();
        InteractionMenuLogger = new TraceLogger();
        GameTemplateSaveLoadLogger = new TraceLogger();
    }

    private void setLoggersIsEnabled()
    {
        GameInstanceManagerLogger.IsEnabled = EnableGameInstanceManagerLogger;
        GameTemplateLoaderLogger.IsEnabled = EnableGameTemplateLoaderLogger;
        CardDisplayLogger.IsEnabled = EnableCardDisplayLogger;
        CardCollectionLogger.IsEnabled = EnableCardCollectionLogger;
        CardCollectionDisplayLogger.IsEnabled = EnableCardCollectionDisplayLogger;
        DeckLogger.IsEnabled = EnableDeckLogger;
        DeckDisplayLogger.IsEnabled = EnableDeckDisplayLogger;
        SpaceLogger.IsEnabled = EnableSpaceLogger;
        SpaceDisplayLogger.IsEnabled = EnableSpaceDisplayLogger;
        StackableLogger.IsEnabled = EnableStackableLogger;
        StackableDisplayLogger.IsEnabled = EnableStackableDisplayLogger;
        TableLogger.IsEnabled = EnableTableLogger;
        TableDisplayLogger.IsEnabled = EnableTableDisplayLogger;
        SimulatorNetworkManagerLogger.IsEnabled = EnableSimulatorNetworkManagerLogger;
        PlayerLogger.IsEnabled = EnablePlayerLogger;
        PlayerHandLogger.IsEnabled = EnablePlayerHandLogger;
        PlayerHandDisplayLogger.IsEnabled = EnablePlayerHandDisplayLogger;
        PlayerManagerLogger.IsEnabled = EnablePlayerManagerLogger;
        PrefabExtensionsLogger.IsEnabled = EnablePrefabExtensionsLogger;
        SelectionManagerLogger.IsEnabled = EnableSelectionManagerLogger;
        InteractionMenuManagerLogger.IsEnabled = EnableInteractionMenuManagerLogger;
        InteractionMenuLogger.IsEnabled = EnableInteractionMenuLogger;
        GameTemplateSaveLoadLogger.IsEnabled = EnableGameTemplateSaveLoadLogger;
    }
}