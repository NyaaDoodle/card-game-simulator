using UnityEngine;

public class SpaceDisplay : StackableDisplay
{
    public Space Space => (Space)CardCollection;

    public virtual void Setup(Space space)
    {
        LoggerReferences.Instance.SpaceDisplayLogger.LogMethod();
        base.Setup(space);
    }
}