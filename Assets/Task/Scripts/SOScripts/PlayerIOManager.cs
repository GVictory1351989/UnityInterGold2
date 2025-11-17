using UnityEngine;
using PlayerIOClient;
using System;
using System.Collections.Generic;

public class PlayerIOManager : MonoBehaviour
{
    private static PlayerIOManager _instance;
    public static PlayerIOManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("PlayerIOManager");
                _instance = go.AddComponent<PlayerIOManager>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    [Header("Server Settings")]
    [Tooltip("Tick this to use the local development server, uncheck to use live PlayerIO server.")]
    public bool useLocalServer = false;
    private static PlayerIOClient.Client client;
    private static Connection connection;

    private static string liveGameId = "cardgame-ataoqx0oluwklhuhkvlvqw";
    private static string playerId;

    // Event callbacks
    public static Action OnConnected;
    public static Action<Connection> OnRoomJoinedCallback;
    public static Action<PlayerIOError> OnErrorCallback;
    public static Action<string> OnServerMessageCallback;
    private static Dictionary<string, string> joinData = new();

    private void Awake()
    {
        // Ensure singleton persists
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Connect();
    }
    public static string PlayerName;
    public void Connect()
    {
        if (client != null)
        {
            Debug.LogWarning("Already connected!");
            return;
        }

        playerId = "player_" + Guid.NewGuid().ToString("N");

        // Authentication arguments
        Dictionary<string, string> authArgs = new Dictionary<string, string> { { "userId", playerId } };

        joinData["playerName"] = GetRandomPlayerName();
        joinData["playerId"] = playerId;
        PlayerName = joinData["playerName"];
    
        PlayerIO.Authenticate(
            liveGameId,
            "public",
            authArgs,
            null,
            (PlayerIOClient.Client c) =>
            {
                ConnectToLocal(c);
                client = c;
                UnityMainThreadDispatcher.Instance.Enqueue(() =>
                {
                    EventManagerUtils.RaiseEvent("ConnectionStatus", "Authenticated ");
                    EventManagerUtils.RaiseEvent("PlayerId", playerId);
                });
                OnConnected?.Invoke();

            },
            (PlayerIOError err) =>
            {
                UnityMainThreadDispatcher.Instance.Enqueue(() =>
                {
                    EventManagerUtils.RaiseEvent("ConnectionStatus","Authenticate issue "+ err.Message);
                });
                OnErrorCallback?.Invoke(err);
            }
        );
    }
    private static readonly string[] playerNames = new string[]
    {
        "Knight", "Wizard", "Archer", "Rogue", "Paladin", "Sorcerer", "Druid", "Monk"
    };

    private static string GetRandomPlayerName()
    {
        int index = UnityEngine.Random.Range(0, playerNames.Length);
        return playerNames[index];
    }
    private const string LOBBY_ROOM_ID = "MainLobby"; 

    public void ConnectToLocal(PlayerIOClient.Client client)
    {
        if(useLocalServer)
        client.Multiplayer.DevelopmentServer = new ServerEndpoint("localhost", 8184);
    }
    public static void JoinLobby()
    {
        if (connection != null) return; 
        client.Multiplayer.CreateJoinRoom(
            LOBBY_ROOM_ID,  
            "LobbyRoom", 
            true,           
            null,           
            joinData,     
            (Connection conn) =>
            {
                if (connection != null)
                {
                    connection.OnMessage -= OnServerMessage;
                }
                connection = conn;

                connection.OnMessage += OnServerMessage;
                OnRoomJoinedCallback?.Invoke(conn);

                UnityMainThreadDispatcher.Instance.Enqueue(() =>
                {
                    EventManagerUtils.RaiseEvent("ConnectionStatus", $"Connected to {"LobbyRoom"}");
                    EventManagerUtils.RaiseEvent("ConnectToRoom", $"Connected to {"LobbyRoom"}");
                });
            },
            (PlayerIOError err) =>
            {
                Debug.LogError("Lobby join error: " + err.Message);
            }
        );
    }

    public static void JoinRoom(string roomType = "1v1Room", string roomId = null)
    {
        if (client == null)
        {
            Debug.LogWarning("PlayerIO client not connected yet!");
            return;
        }

        if (joinData == null)
            joinData = new Dictionary<string, string>();

        client.Multiplayer.CreateJoinRoom(
            roomId,
            roomType,
            true,       
            null,       
            joinData,   
            (Connection conn) =>
            {
                if (connection != null)
                {
                    connection.OnMessage -= OnServerMessage;
                }
                connection = conn;
              
                connection.OnMessage += OnServerMessage;
                OnRoomJoinedCallback?.Invoke(conn);
                Debug.Log($"Connected Room {roomType}");

                UnityMainThreadDispatcher.Instance.Enqueue(() =>
                {
                    EventManagerUtils.RaiseEvent("ConnectionStatus", $"Connected to {roomType}");
                    EventManagerUtils.RaiseEvent("ConnectToRoom", $"Connected to {roomType}");
                });
            },
            (PlayerIOError err) =>
            {
                Debug.LogError("Room join error: " + err.Message);
                UnityMainThreadDispatcher.Instance.Enqueue(() =>
                {
                    EventManagerUtils.RaiseEvent("ConnectionStatus", err.Message);
                });
                OnErrorCallback?.Invoke(err);
            }
        );
    }

    /// <summary>
    /// Handle server messages
    /// </summary>
    private static void OnServerMessage(object sender, Message m)
    {
        UnityMainThreadDispatcher.Instance.Enqueue(() =>
        {
            EventManagerUtils.RaiseEvent("OnMessage", m);
        });
    }

    /// <summary>
    /// Send message to server
    /// </summary>
    public static void SendMessage(string action, string json)
    {
        if (connection != null)
        {
            connection.Send(action, json);
        }
        else
        {
            Debug.LogWarning("Not connected to room yet.");
        }
    }

    /// <summary>
    /// Disconnect from room
    /// </summary>
    public static void Disconnect()
    {
        if (connection != null)
        {
            connection.Disconnect();
            connection = null;
        }

        if (client != null)
        {
            client = null;
        }
    }
    public static void DisconnectRoom()
    {
        if (connection != null)
        {
            connection.OnMessage -= OnServerMessage;
            connection.Disconnect();
            connection = null;
        }
    }
    /// <summary>
    /// Get Player ID
    /// </summary>
    public static string GetPlayerId()
    {
        return playerId;
    }
}
