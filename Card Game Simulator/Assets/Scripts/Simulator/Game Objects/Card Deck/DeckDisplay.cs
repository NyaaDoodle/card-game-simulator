public class DeckDisplay : StackableDisplay
{
    public Deck DeckState { get; private set; }

    public void Setup(Deck deckState)
    {
        base.Setup(deckState);
        DeckState = deckState;
    }
}