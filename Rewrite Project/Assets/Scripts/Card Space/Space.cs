public class Space : Stackable
{
    public SpaceData SpaceData { get; private set; }

    public Space(SpaceData spaceData) : base(spaceData)
    {
        SpaceData = spaceData;
    }
}
