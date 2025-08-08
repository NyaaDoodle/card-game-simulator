public class Deck : Stackable
{
    public DeckData DeckData { get; private set; }

    public void Setup(DeckData deckData)
    {
        DeckData = deckData;
        base.Setup(deckData);
    }

    private void addStartingCards()
    {
        if (DeckData != null) return;
        base.AddCards(DeckData.StartingCards);
    }
}