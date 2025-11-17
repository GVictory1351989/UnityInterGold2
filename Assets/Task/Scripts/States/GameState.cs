using PlayerIOClient;

public abstract class GameState
{
    public abstract void HandleMessage(GamePlayPage game, Message message);
}
