using UnityEngine;
using TMPro;
using System;

public class WaitingPanel : MonoBehaviour
{
    public TMP_Text player1;
    public TMP_Text player2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        player1.text = PlayerIOManager.PlayerName;
        EventManagerUtils.Subscribe<GameEvent<OpponentData>>("otherplayerconnected",OtherPlayerConnected);
    }

    private void OtherPlayerConnected(GameEvent<OpponentData> obj)
    {
        player2.text = obj.Data.playername;
    }

    // Update is called once per frame
    void OnDisable()
    {
        EventManagerUtils.Unsubscribe<GameEvent<OpponentData>>("otherplayerconnected",OtherPlayerConnected);

    }

}
