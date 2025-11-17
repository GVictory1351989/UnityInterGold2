using PlayerIOClient;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginPage : TargetBaseViewer<LoginPage>
{
    [SerializeField] Button PlayButton;
    [SerializeField] TMP_Text ConnectionStatus,PlayerID,PlayerName;
    [SerializeField] WaitingPanel Panel;
    protected override void Show(LoginPage tTarget)
    {
        base.Show(this);
    }
    private void Start()
    {
        PlayButton.onClick.AddListener(PlayOperation);
        Panel.gameObject.SetActive(false);

    }
    private void OnEnable()
    {
        EventManagerUtils.Subscribe<string>("ConnectionStatus", ConnectionStatusCallback);
        EventManagerUtils.Subscribe<string>("ConnectToRoom", ConnectToRoomCallback);
        EventManagerUtils.Subscribe<string>("PlayerId", PlayerIDAssign);
        EventManagerUtils.Subscribe<PlayerIOClient.Message>("OnMessage", OnMessageReceived);
    }

    private void OnMessageReceived(Message obj)
    {
        switch(obj.Type)
        {
            case "connecttogameplay":
                StartCoroutine(HandleConnectToGameplay(obj));
                break;
        }
    }

    private IEnumerator HandleConnectToGameplay(Message obj)
    {
        PlayerIOManager.DisconnectRoom();
        yield return new WaitForSeconds(1f);
        string roomid = obj.GetString(0);
        string name = obj.GetString(1);
        string opponentplayerid = obj.GetString(2);
        Displayer.Instance.Display(Type.GetType("GamePlayPage"));
        PlayerIOManager.JoinRoom("1v1Room", roomid);
        yield return new WaitForSeconds(0.1f);
        var evt = new GameEvent<OpponentData>(new OpponentData(opponentplayerid, name),
            () => Debug.Log("Opponent data handled")
            );
        EventManagerUtils.RaiseEvent("otherplayerconnected", evt);

        base.Hide();
    }
    private void ConnectToRoomCallback(string obj)
    {
        PlayButton.gameObject.SetActive(false);
        Panel.gameObject.SetActive(true);
    }
    private void PlayOperation()
    {
        PlayerIOManager.JoinLobby();
    }
    private void PlayerIDAssign(string obj)
    {
        PlayerID.text = obj;
    }
    private void ConnectionStatusCallback(string obj)
    {
        ConnectionStatus.text = obj;
        PlayerName.text = PlayerIOManager.PlayerName;
    }
    protected override void Hide()
    {
        PlayButton.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        EventManagerUtils.Unsubscribe<string>("ConnectionStatus", ConnectionStatusCallback);
        EventManagerUtils.Unsubscribe<string>("PlayerId", ConnectionStatusCallback);
        EventManagerUtils.Unsubscribe<string>("ConnectToRoom", ConnectToRoomCallback);
        EventManagerUtils.Unsubscribe<PlayerIOClient.Message>("OnMessage", OnMessageReceived);
        Panel.gameObject.SetActive(false);
    }

}
