using PlayerIO.GameLibrary;
using System;
using System.Collections.Generic;

[RoomType("LobbyRoom")]
public class LobbyRoom : Game<Player>
{
    private Queue<Player> matchmakingQueue = new Queue<Player>();

    public override void GameStarted()
    {
        Console.WriteLine("Lobby started: " + RoomId);
    }

    public override void UserJoined(Player player)
    {
        Console.WriteLine($"{player.ConnectUserId} joined lobby");
        if (player.JoinData.ContainsKey("playerName"))
        {
            player.Name = player.JoinData["playerName"].ToString();
        }
        else
        {
            player.Name = "Player_" + player.ConnectUserId.Substring(0, 4);
        }
        player.InQueue = true;
        matchmakingQueue.Enqueue(player);

        TryStartMatch();
    }

    public override void UserLeft(Player player)
    {
        Console.WriteLine($"{player.ConnectUserId} left lobby");
        // Remove from queue if present
        if (player.InQueue)
        {
            var tempQueue = new Queue<Player>();
            while (matchmakingQueue.Count > 0)
            {
                var p = matchmakingQueue.Dequeue();
                if (p != player) tempQueue.Enqueue(p);
            }
            matchmakingQueue = tempQueue;
        }
    }

    private void TryStartMatch()
    {
        while (matchmakingQueue.Count >= 2)
        {
            var p1 = matchmakingQueue.Dequeue();
            var p2 = matchmakingQueue.Dequeue();

            p1.InQueue = false;
            p2.InQueue = false;

            string roomId = Guid.NewGuid().ToString();
            p1.Send("connecttogameplay", roomId, p2.Name, p2.ConnectUserId);
            p2.Send("connecttogameplay", roomId, p1.Name, p1.ConnectUserId);
            Console.WriteLine($"1v1 Room {roomId} ready for {p1.ConnectUserId} and {p2.ConnectUserId}");
        }
    }
}
