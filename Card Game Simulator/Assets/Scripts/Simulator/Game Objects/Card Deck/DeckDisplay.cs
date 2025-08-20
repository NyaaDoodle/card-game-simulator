public class DeckDisplay : StackableDisplay
{
    public Deck Deck => (Deck)CardCollection;

    public virtual void Setup(Deck deck)
    {
        base.Setup(deck);
    }
}