using System.Collections.Generic;

public class GameInstance
{
    public GameTemplate GameTemplate { get; }
    public Table Table { get; set; }
    public List<Deck> Decks { get; set; } = new List<Deck>();
    public List<Space> Spaces { get; set; } = new List<Space>();
    public List<Player> Players { get; set; } = new List<Player>();

    public GameInstance(GameTemplate gameTemplate)
    {
        GameTemplate = gameTemplate;
    }
}
