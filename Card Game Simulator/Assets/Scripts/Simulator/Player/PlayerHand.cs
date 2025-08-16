public class PlayerHand : CardCollection
{
    public override void AddCard(Card card, int index)
    {
        LoggerReferences.Instance.PlayerHandLogger.LogMethod();
        card = card.FaceUp();
        base.AddCard(card, index);
    }
}