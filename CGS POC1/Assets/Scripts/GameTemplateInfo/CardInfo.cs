using UnityEngine;

public class CardInfo
{
    public string Id { get; set; }

    public string FrontSideFileName { get; set; } = null;

    public string BackSideFileName { get; set; } = null;

    public CardInfo(string id) : this(id, null, null) {}

    public CardInfo(string id, string frontSideFileName, string backSideFileName)
    {
        this.Id = id;
        this.FrontSideFileName = frontSideFileName;
        this.BackSideFileName = backSideFileName;
    }
}
