using System;
using System.Text;
using Mirror;

public class Stackable : CardCollection
{
    [SyncVar] private IStackableData stackableData;

    public IStackableData StackableData => stackableData;

    public override void OnStartClient()
    {
        LoggerReferences.Instance.StackableLogger.LogMethod();
        base.OnStartClient();
        attachToTableObjectsContainer();
    }

    private void attachToTableObjectsContainer()
    {
        LoggerReferences.Instance.StackableLogger.LogMethod();
        gameObject.transform.SetParent(ContainerReferences.Instance.TableObjectsContainer, false);
    }

    public virtual void Setup(IStackableData stackableData)
    {
        LoggerReferences.Instance.StackableLogger.LogMethod();
        this.stackableData = stackableData;
    }

    public override void AddCard(Card card, int index)
    {
        LoggerReferences.Instance.StackableLogger.LogMethod();
        card = card.FaceDown();
        base.AddCard(card, index);
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine(stackableData != null ? stackableData.ToString() : "null");
        stringBuilder.AppendLine(base.ToString());
        return stringBuilder.ToString();
    }
}
