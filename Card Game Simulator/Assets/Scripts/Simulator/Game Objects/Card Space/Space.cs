public class Space : Stackable
{
    public SpaceData SpaceData => (SpaceData)StackableData;
    public void Setup(SpaceData spaceData)
    {
        LoggerReferences.Instance.SpaceLogger.LogMethod();
        base.Setup(spaceData);
    }
}
