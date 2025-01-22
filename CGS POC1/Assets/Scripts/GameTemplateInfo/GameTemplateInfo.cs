using System;
using System.Collections.Generic;

public class GameTemplateInfo
{
    public TableInfo TableInfo { get; set; } = new TableInfo();

    public CardBank CardBank { get; set; } = new CardBank();

    public List<DeckInfo> DeckInfoList { get; set; } = new List<DeckInfo>();
}
