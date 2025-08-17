public class Space : Stackable
{
    public SpaceData SpaceData => (SpaceData)StackableData;
    public void Setup(SpaceData spaceData)
    {
        LoggingManager.Instance.SpaceLogger.LogMethod();
        base.Setup(spaceData);
    }
}
