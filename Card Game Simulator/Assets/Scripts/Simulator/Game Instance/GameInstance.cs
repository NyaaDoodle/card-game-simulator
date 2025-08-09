using System.Collections.Generic;

public class GameInstance
{
    public Table Table { get; private set; }
    public List<Deck> Decks { get; } = new List<Deck>();
    public List<Space> Spaces { get; } = new List<Space>();
    public List<Player> Players { get; } = new List<Player>();
}
