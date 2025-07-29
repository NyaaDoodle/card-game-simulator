public class DeckState : StackableState
{
    public DeckData DeckData { get; private set; }

    public DeckState(DeckData deckData) : base(deckData)
    {
        DeckData = deckData;
        AddCards(CardUtilities.CreateCards(DeckData.Cards));
    }
}
