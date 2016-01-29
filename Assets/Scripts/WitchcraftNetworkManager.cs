using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class WitchcraftNetworkManager : NetworkManager
{
    public const int GOAL_PLAYER_COUNT = 2;
    public int CurrentPlayerCount;

    public int CurrentPlayerIndex;
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        CurrentPlayerCount++;
        base.OnServerAddPlayer(conn, playerControllerId);
        Debug.Log("Added player");

        if(CurrentPlayerCount == GOAL_PLAYER_COUNT) {
            StartGame();
        }
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        CurrentPlayerCount--;
        base.OnServerDisconnect(conn);
        Debug.Log("Player OnServerDisconnect");
    }

    public void StartGame()
    {
        foreach(var player in GameObject.FindObjectsOfType<PlayerScript>()) {
            player.IssueStartGameCommand();
        }
    }
}
