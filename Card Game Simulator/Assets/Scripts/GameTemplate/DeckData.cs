using System.Collections.Generic;

public class DeckData
{
    public int Id { get; set; }
    
    // In-order card list
    public LinkedList<int> CardIds { get; set; }
}