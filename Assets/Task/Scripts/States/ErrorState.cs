using PlayerIOClient;
using UnityEngine;

public class ErrorState : GameState
{
    public override void HandleMessage(GamePlayPage game, Message message)
    {
        game.ErrorText.text = message.GetString(0);
    }
}
