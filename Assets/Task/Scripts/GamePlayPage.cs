using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using PlayerIOClient;
using System;
using PlayerIOClient;

public class GamePlayPage : TargetBaseViewer<GamePlayPage>
{
    public PlayerUI MainPlayer;
    public PlayerUI OpponentPlayer;
    public Transform BattleZone;
    public GameObject CardPrefab;
    private Dictionary<int, CardUnit> cardDatabase = new Dictionary<int, CardUnit>();
    private Dictionary<string, GameState> stateMap = new Dictionary<string, GameState>();
    public CardDatabase CardDatabase;
    public TMP_Text ErrorText;

    private void Awake()
    {
        stateMap["Hand"] = new HandState();
        stateMap["dealcards"] = new DealCardsState();
        stateMap["TurnTimer"] = new TurnTimerState();
        stateMap["Error"] = new ErrorState();
    }
    protected override void Show(GamePlayPage tTarget)
    {
        base.Show(this);
        MainPlayer.playerId = PlayerIOManager.GetPlayerId();
        MainPlayer.PlayerName.text = PlayerIOManager.PlayerName;
        MainPlayer.PlayerID.text = PlayerIOManager.GetPlayerId();
    }

    private void OnEnable()
    {
        EventManagerUtils.Subscribe<Message>("OnMessage", OnServerMessage);
        EventManagerUtils.Subscribe<GameEvent<OpponentData>>("otherplayerconnected", OnOpponentConnected);
        
    }
    private void OnDisable()
    {
        EventManagerUtils.Unsubscribe<Message>("OnMessage", OnServerMessage);
        EventManagerUtils.Unsubscribe<GameEvent<OpponentData>>("otherplayerconnected", OnOpponentConnected);
    }


    private void Start()
    {
        MainPlayer.PlayerName.text = PlayerIOManager.PlayerName;
        MainPlayer.PlayerID.text = PlayerIOManager.GetPlayerId();
        
    }

    private void OnOpponentConnected(GameEvent<OpponentData> obj)
    {
        OpponentPlayer.PlayerName.text = obj.Data.playername;
        OpponentPlayer.PlayerID.text = obj.Data.playerid;
        OpponentPlayer.playerId = obj.Data.playerid;
    }

    private void OnServerMessage(Message m)
    {
        if (stateMap.TryGetValue(m.Type, out GameState state))
        {
            state.HandleMessage(this, m);
        }
    }

    private void PlayCard(CardUnit card, GameObject cardGO)
    {
        
    }

    private void HandleOpponentCardPlayed(Message m)
    {
       
    }

    private void UpdateGameState(Message m)
    {
    }
    public void LoadCardDatabase(CardDataList dataList)
    {
        cardDatabase.Clear();
      
    }
}
