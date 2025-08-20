public class PlayerHand : CardCollection
{
    public override void AddCard(Card card, int index)
    {
        card = card.FaceUp();
        base.AddCard(card, index);
    }
}