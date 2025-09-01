using Mirror;

public struct JoiningPlayerMessage : NetworkMessage
{
    public string playerId;
    public string playerName;
}
