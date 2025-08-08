public class Space : Stackable
{
    public SpaceData SpaceData { get; private set; }

    public void Setup(SpaceData spaceData)
    {
        SpaceData = spaceData;
        base.Setup(spaceData);
    }
}
