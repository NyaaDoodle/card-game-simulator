public class GameTemplateLoader
{
    public GameTemplate LoadGameTemplate() {
        // PLACEHOLDER
        LoggingManager.Instance.GameTemplateLoaderLogger.LogMethod();
        return loadTestGameTemplate();
    }

    private GameTemplate loadTestGameTemplate()
    {
        LoggingManager.Instance.GameTemplateLoaderLogger.LogMethod();
        return new TestGameTemplateInitialization().TestGameTemplate;
    }
}
