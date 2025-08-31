using System;
using System.Text;
using Mirror;

public class Stackable : CardCollection
{
    [SyncVar] private IStackableData stackableData;

    public IStackableData StackableData => stackableData;

    public virtual void Setup(IStackableData stackableData)
    {
        this.stackableData = stackableData;
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine(stackableData != null ? stackableData.ToString() : "null");
        stringBuilder.AppendLine(base.ToString());
        return stringBuilder.ToString();
    }
}
