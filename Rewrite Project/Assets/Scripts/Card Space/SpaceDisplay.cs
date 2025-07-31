using UnityEngine;
using UnityEngine.EventSystems;

public class SpaceDisplay : StackableDisplay
{
    public Space SpaceState { get; private set; }

    public void Setup(Space spaceState)
    {
        base.Setup(spaceState);
        SpaceState = spaceState;
    }

    public override void OnPointerClick(PointerEventData pointerEventData)
    {
        if (SpaceState == null) return;
        Debug.Log($"Space {SpaceState.SpaceData.Id} clicked");
    }
}