using PlayerIO.GameLibrary;
using System.Collections.Generic;

public class GameStartedState : IState
{
    private int START_HAND = 3;
    public void Enter(GameRoom room)
    {
    }
    public void Execute(GameRoom room)
    {
      
        room.currentTurnIndex = 0;
        room.ChangeState(new PlayerTurnState(GetPlayerByIndex(room, room.currentTurnIndex)));
    }
    public Player GetPlayerByIndex(GameRoom room, int index)
    {
        int i = 0;
        foreach (var p in room.Players)
        {
            if (i == index) return p;
            i++;
        }
        return null;
    }
    public void Exit(GameRoom room) { }
}
