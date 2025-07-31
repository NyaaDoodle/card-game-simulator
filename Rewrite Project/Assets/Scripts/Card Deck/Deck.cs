public class Deck : Stackable
{
    public DeckData DeckData { get; private set; }

    public Deck(DeckData deckData) : base(deckData)
    {
        DeckData = deckData;
        base.AddCards(CardUtilities.CreateCards(DeckData.Cards));
    }
}
