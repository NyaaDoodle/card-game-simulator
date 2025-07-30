public class PlayerHandState : CardCollectionState
{
    public void AddCard(CardState card)
    {
        AddCardAtEnd(card);
    }

    public override void AddCard(CardState card, int index)
    {
        card.FlipFaceUp();
        base.AddCard(card, index);
    }
}