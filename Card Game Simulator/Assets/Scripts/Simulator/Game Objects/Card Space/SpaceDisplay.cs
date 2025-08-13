using UnityEngine;

[RequireComponent(typeof(Space))]
public class SpaceDisplay : StackableDisplay
{
    protected Space space;

    protected override void SetCardCollection()
    {
        space = GetComponent<Space>();
        stackable = space;
        cardCollection = space;
    }
}