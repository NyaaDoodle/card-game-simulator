public class DeckState : StackableState
{
    public DeckData DeckData { get; private set; }

    public DeckState(DeckData deckData) : base(deckData)
    {
        DeckData = deckData;
        base.AddCards(CardUtilities.CreateCards(DeckData.Cards));
    }
}
