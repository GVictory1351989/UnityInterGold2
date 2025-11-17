using PlayerIO.GameLibrary;
public class WaitingForPlayersState : IState
{
    public void Enter(GameRoom room)
    {
        room.Broadcast("Status", "Waiting for players...");
    }

    public void Execute(GameRoom room)
    {
        if (room.CountPlayers() >= 2)
        {
            foreach (var player in room.Players)
            {
                player.CardDB = new CardDB(player.ConnectUserId);
                player.CreateDeck();
                player.DrawCards(3);
                room.SendHandToPlayer(player, true);
            }
            room.ChangeState(new GameStartedState());
        }
    }

    public void Exit(GameRoom room)
    {
        room.Broadcast("Status", "Game is starting!");
    }
}
