public class GameTemplateLoader
{
    public GameTemplate LoadGameTemplate() {
        // PLACEHOLDER
        LoggerReferences.Instance.GameTemplateLoaderLogger.LogMethod();
        return loadTestGameTemplate();
    }

    private GameTemplate loadTestGameTemplate()
    {
        LoggerReferences.Instance.GameTemplateLoaderLogger.LogMethod();
        return new TestGameTemplateInitialization().TestGameTemplate;
    }
}
