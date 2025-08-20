public class Space : Stackable
{
    public SpaceData SpaceData => (SpaceData)StackableData;
    public void Setup(SpaceData spaceData)
    {
        base.Setup(spaceData);
    }
}
