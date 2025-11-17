using PlayerIOClient;

public class TurnTimerState : GameState
{
    public override void HandleMessage(GamePlayPage game, Message message)
    {
        string playerid = message.GetString(0);
        int timer = message.GetInt(1);

        if (game.MainPlayer.playerId == playerid)
        {
            game.OpponentPlayer.TurnText.text = "0";
            game.MainPlayer.TurnText.text = timer.ToString();
        }
        else
        {
            game.MainPlayer.TurnText.text = "0";
            game.OpponentPlayer.TurnText.text = timer.ToString();
        }
    }
}
