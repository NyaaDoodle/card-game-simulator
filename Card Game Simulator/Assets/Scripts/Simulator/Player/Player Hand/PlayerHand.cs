public class PlayerHand : CardCollection
{
    public void AddCard(Card card)
    {
        AddCardAtEnd(card);
    }

    public override void AddCard(Card card, int index)
    {
        card.FlipFaceUp();
        base.AddCard(card, index);
    }
}