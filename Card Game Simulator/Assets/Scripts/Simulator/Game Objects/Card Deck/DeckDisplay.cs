public class DeckDisplay : StackableDisplay
{
    public Deck Deck => (Deck)CardCollection;

    public virtual void Setup(Deck deck)
    {
        LoggerReferences.Instance.DeckDisplayLogger.LogMethod();
        base.Setup(deck);
    }
}