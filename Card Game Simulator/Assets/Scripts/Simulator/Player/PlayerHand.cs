public class PlayerHand : CardCollection
{
    public override void OnStartClient()
    {
        base.OnStartClient();
        attachToPlayerHandContainer();
    }

    private void attachToPlayerHandContainer()
    {
        gameObject.transform.SetParent(ContainerReferences.Instance.PlayerHandContainer, false);
    }
}