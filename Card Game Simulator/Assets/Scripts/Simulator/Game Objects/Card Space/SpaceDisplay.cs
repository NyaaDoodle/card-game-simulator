using UnityEngine;

public class SpaceDisplay : StackableDisplay
{
    public Space Space => (Space)CardCollection;

    public virtual void Setup(Space space)
    {
        base.Setup(space);
    }
}