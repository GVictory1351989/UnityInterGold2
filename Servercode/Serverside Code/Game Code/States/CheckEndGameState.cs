using System;
using System.Linq;

public class CheckEndGameState : IState
{
    public void Enter(GameRoom room)
    {
        bool allDecksEmpty = room.Players.All(p => p.Deck.Count == 0 && p.Hand.Count == 0);
        bool maxTurnsReached = room.currentTurnIndex >= 6; 
        if (allDecksEmpty || maxTurnsReached)
        {
            var players = room.Players.ToList();
            int highestScore = players.Max(p => p.Score);
            var winners = players.Where(p => p.Score == highestScore).ToList();
            if (winners.Count == 1)
            {
                room.Winner = winners[0];
                room.Broadcast("GameOver", room.Winner.ConnectUserId, room.Winner.Score);
            }
            else
            {
                room.Broadcast("GameOver", "Tie", highestScore);
            }
        }
        else
        {
            room.SwitchTurn(); 
            room.ChangeState(new PlayerTurnState(room.CurrentPlayer));
        }
    }

    public void Execute(GameRoom room)
    {
    }

    public void Exit(GameRoom room)
    {
    }
}
