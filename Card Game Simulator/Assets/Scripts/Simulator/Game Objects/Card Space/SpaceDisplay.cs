using UnityEngine;

[RequireComponent(typeof(Space))]
public class SpaceDisplay : StackableDisplay
{
    public Space Space => (Space)CardCollection;

    protected override void SetCardCollection()
    {
        LoggerReferences.Instance.SpaceDisplayLogger.LogMethod();
        CardCollection = GetComponent<Space>();
    }
}