public class PlayerHand : CardCollection
{
    public override void OnStartClient()
    {
        base.OnStartClient();
        attachToPlayerHandContainer();
    }

    public override void AddCard(Card card, int index)
    {
        card = card.FaceUp();
        base.AddCard(card, index);
    }

    private void attachToPlayerHandContainer()
    {
        gameObject.transform.SetParent(ContainerReferences.Instance.PlayerHandContainer, false);
    }
}