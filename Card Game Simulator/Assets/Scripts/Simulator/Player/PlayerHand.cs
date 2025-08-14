public class PlayerHand : CardCollection
{
    public override void OnStartClient()
    {
        LoggerReferences.Instance.PlayerHandLogger.LogMethod();
        base.OnStartClient();
        attachToPlayerHandContainer();
    }

    private void attachToPlayerHandContainer()
    {
        LoggerReferences.Instance.PlayerHandLogger.LogMethod();
        gameObject.transform.SetParent(ContainerReferences.Instance.PlayerHandContainer, false);
    }
    
    public override void AddCard(Card card, int index)
    {
        LoggerReferences.Instance.PlayerHandLogger.LogMethod();
        card = card.FaceUp();
        base.AddCard(card, index);
    }
}