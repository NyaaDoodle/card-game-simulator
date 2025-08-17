public class PlayerHand : CardCollection
{
    public override void AddCard(Card card, int index)
    {
        LoggingManager.Instance.PlayerHandLogger.LogMethod();
        card = card.FaceUp();
        base.AddCard(card, index);
    }
}