public class SpaceDisplay : StackableDisplay
{
    public Space SpaceState { get; private set; }

    public void Setup(Space spaceState)
    {
        base.Setup(spaceState);
        SpaceState = spaceState;
    }
}