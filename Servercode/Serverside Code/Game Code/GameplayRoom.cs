using PlayerIO.GameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

[RoomType("1v1Room")]
public class GameRoom : Game<Player>
{
    private const int START_HAND = 3;
    private const int TURN_DURATION = 30; // seconds per turn
    private const int MIN_PLAYERS = 2;
    public int currentTurnIndex = 0;
    private int turnTimeRemaining = TURN_DURATION;
    private DateTime turnStartTime;
    private Random rng = new Random();
    public bool gameHasStarted = false;
    private Timer turnTimer;
    public Player CurrentPlayer { get; set; }
    public Player Winner { get; set; }
    public bool IsStartedTimer = false;
    public override void UserJoined(Player player)
    {
        Console.WriteLine("Joined: " + player.ConnectUserId);
        player.CurrentRoomId = RoomId;
        if (currentState == null)
        {
            ChangeState(new WaitingForPlayersState());
        }
        if (!IsStartedTimer)
        {
            IsStartedTimer = true;
            turnTimer = AddTimer(GameLoop, 1000);
        }
        LogBroadcast($"User Joined "+player.ConnectUserId);
      }

    public Player GetOpponent(Player player)
    {
        foreach (var p in Players)
        {
            if (p.ConnectUserId != player.ConnectUserId) return p;
        }
        return null;
    }

    private IState currentState;
    
    public void ChangeState(IState newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState.Enter(this);
    }

    public void GameLoop()
    {
        currentState?.Execute(this);
    }
    public void StartGame()
    {
        if (!gameHasStarted)
        {
            gameHasStarted = true;
            Console.WriteLine("Game started: " + RoomId);
            
        }
    }
    public override void GotMessage(Player player, Message message)
    {
        switch (message.Type)
        {
            case "selectedcard":
                HandlePlayCard(player, message);
                break;
            case "EndTurn":
                HandleEndTurn(player);
                break;
            case "DrawCard":
                HandleDrawCard(player);
                break;
            default:
                player.Send("Error", "Unknown message type");
                break;
        }
    }

    private void HandlePlayCard(Player player, Message msg)
    {
        Console.WriteLine($"HandlePlayCard called for player: {player.ConnectUserId}");

        if (!IsPlayerTurn(player))
        {
            Console.WriteLine($"Not player's turn: {player.ConnectUserId}");
            player.Send("Error", "Not your turn");
            return;
        }

        int handIndex =int.Parse( msg.GetString(0));
        Console.WriteLine($"Received handIndex: {handIndex}");

        if (handIndex < 0 || handIndex >= player.Hand.Count)
        {
            Console.WriteLine($"Invalid card index: {handIndex}. Hand count: {player.Hand.Count}");
            player.Send("Error", "Invalid card index");
            return;
        }

        int cardId = player.Hand[handIndex];
        Console.WriteLine($"Card selected to play: {cardId}");

        if (!player.CardDB.Cards.TryGetValue(cardId, out CardData card))
        {
            Console.WriteLine($"Card does not exist in CardDB: {cardId}");
            player.Send("Error", "Card does not exist");
            return;
        }

        Console.WriteLine($"Card cost: {card.Cost}, Player energy: {player.Energy}");

        if (player.Energy < card.Cost)
        {
            Console.WriteLine($"Not enough energy to play card {cardId}");
            player.Send("Error", "Not enough energy");
            return;
        }

        player.Energy -= card.Cost;
        player.Hand.RemoveAt(handIndex);
        player.DiscardPile.Add(cardId);
        player.PlayedThisTurn.Add(cardId);

        Console.WriteLine($"Card {cardId} played. Energy left: {player.Energy}");
        Console.WriteLine($"Hand now: {string.Join(", ", player.Hand)}");
        Console.WriteLine($"Discard pile: {string.Join(", ", player.DiscardPile)}");
        Console.WriteLine($"PlayedThisTurn: {string.Join(", ", player.PlayedThisTurn)}");

        Broadcast("CardPlayed", player.ConnectUserId, cardId.ToString());
        SendHandToPlayer(player, true);
        SendEnergy(player);

        if (player.Hand.Count == 0 || player.Energy == 0)
        {
            Console.WriteLine("Hand empty or energy 0, changing state to ResolveTurnState");
            ChangeState(new ResolveTurnState());
        }
    }

    public void SendHandToPlayer(Player player,bool isBroadcast=false)
    {
        var message = Message.Create("Hand");

        foreach (var cardId in player.Hand)
        {
            if (player.CardDB.Cards.TryGetValue(cardId, out CardData card))
            {
                message.Add(
                    player.ConnectUserId,
                    "" + cardId, 
                    card.BaseCard.GetID(),
                    ""+card.Cost,
                    ""+card.Health,
                    ""+card.Ability,
                    ""+card.EnergyValue, 
                    ""+card.FinalScore);                
               
            }
        }
        if (!isBroadcast)
            player.Send(message);
        else
        {
            BroadcastData(message);
        }
    }
    public void BroadcastData(Message msg)
    {
        Broadcast(msg);
    }

    public void HandleEndTurn(Player player)
    {
        if (!IsPlayerTurn(player))
        {
            player.Send("Error", "Not your turn");
            return;
        }

        SwitchTurn();
        turnTimeRemaining = TURN_DURATION; // reset timer
    }
    private void HandleDrawCard(Player player)
    {
        if (!IsPlayerTurn(player))
        {
            player.Send("Error", "Not your turn");
            return;
        }
        player.DrawCards(1);
        SendHandToPlayer(player,true);
    }
  
    private bool IsPlayerTurn(Player p)
    {
        return GetPlayerByIndex(currentTurnIndex) == p;
    }
    public void SwitchTurn()
    {
        currentTurnIndex = (currentTurnIndex + 1) % CountPlayers();
        var p = GetPlayerByIndex(currentTurnIndex);
        p.RefillEnergy();
        SendEnergy(p);
        Broadcast("TurnStart", p.ConnectUserId);
        CurrentPlayer = p;
    }

    public int CountPlayers()
    {
        int count = 0;
        foreach (var p in Players) count++;
        return count;
    }

    public Player GetPlayerByIndex(int index)
    {
        var playerList = Players.ToList(); // ensure order
        if (index < 0 || index >= playerList.Count)
            return null;
        return playerList[index];
    }
 
    public void LogBroadcast(string obj)
    {
        foreach (var p in Players)
        {
            p.Send("log", obj);
        }
    }

    private void SendEnergy(Player p)
    {
        p.Send("Energy", ""+p.Energy);
    }
}
