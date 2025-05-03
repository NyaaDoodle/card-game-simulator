using Mirror;

public class DummyData
{
    [SyncVar]
    private int id;

    [SyncVar]
    private string info;

    public int Id
    {
        get => id;
        set => id = value;
    }

    public string Info
    {
        get => info;
        set => info = value;
    }
}