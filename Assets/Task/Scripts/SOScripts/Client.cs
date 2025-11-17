using UnityEngine;

public class Client : MonoBehaviour
{
    void Start()
    {
        var server = PlayerIOManager.Instance;
        var dispatcher = UnityMainThreadDispatcher.Instance;
        PlayerIOManager.OnConnected += () => Debug.Log("Connected!");
        PlayerIOManager.OnRoomJoinedCallback += (conn) => Debug.Log("Room joined: " );
        PlayerIOManager.OnServerMessageCallback += (json) => Debug.Log("Server: " + json);
        PlayerIOManager.OnErrorCallback += (err) => Debug.LogError("Error: " + err.Message);
    }

    // Send a playCards message
    void PlayCards(int[] cardIds)
    {
        var message = new { action = "playCards", cardIds = cardIds, playerId = PlayerIOManager.GetPlayerId() };
        string json = JsonUtility.ToJson(message);
        PlayerIOManager.SendMessage("gameAction", json);
    }

}
