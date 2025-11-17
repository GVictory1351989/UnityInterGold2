using PlayerIO.GameLibrary;

public class GameOverState : IState
{
    public void Enter(GameRoom room)
    {
        room.Broadcast("GameOver", room.Winner?.ConnectUserId ?? "");
    }

    public void Execute(GameRoom room) { }
    public void Exit(GameRoom room) { }
}
