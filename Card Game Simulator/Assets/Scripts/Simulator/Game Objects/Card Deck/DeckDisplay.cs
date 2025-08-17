public class DeckDisplay : StackableDisplay
{
    public Deck Deck => (Deck)CardCollection;

    public virtual void Setup(Deck deck)
    {
        LoggingManager.Instance.DeckDisplayLogger.LogMethod();
        base.Setup(deck);
    }
}