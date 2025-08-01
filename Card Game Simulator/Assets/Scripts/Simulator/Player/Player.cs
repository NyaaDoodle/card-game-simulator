public class Player
{
    public PlayerHand PlayerHand { get; private set; }

    public Player(PlayerHand playerHand)
    {
        PlayerHand = playerHand;
    }
}
