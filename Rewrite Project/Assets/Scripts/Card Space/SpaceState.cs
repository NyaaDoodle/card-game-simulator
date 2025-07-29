public class SpaceState : StackableState
{
    public SpaceData SpaceData { get; private set; }

    public SpaceState(SpaceData spaceData) : base(spaceData)
    {
        SpaceData = spaceData;
    }
}
