using PlayerIO.GameLibrary;
using System;
using System.Collections.Generic;

public class PlayerTurnState : IState
{
    private Player player;
    private DateTime turnStartTime;
    private const int TURN_DURATION = 30;
    private int timeRemaining;
    public PlayerTurnState(Player currentPlayer)
    {
        player = currentPlayer;
    }

    public void Enter(GameRoom room)
    {
        timeRemaining = TURN_DURATION;
        turnStartTime = DateTime.UtcNow;
        room.CurrentPlayer = player;
        room.CurrentPlayer.DrawCards(1);
        room.SendHandToPlayer(room.CurrentPlayer,true);
        // Increase energy (max 6)
        room.CurrentPlayer.Energy = Math.Min(player.Energy + 1, 6);
        room.Broadcast("TurnStart", player.ConnectUserId, player.Energy);
    }

    public void Execute(GameRoom room)
    {
        timeRemaining--;
        room.Broadcast("TurnTimer", room.CurrentPlayer.ConnectUserId, timeRemaining);
        if (timeRemaining <= 0)
        {
            room.ChangeState(new ResolveTurnState());
        }
    }

    public void Exit(GameRoom room) { }
}
