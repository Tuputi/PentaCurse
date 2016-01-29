using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class PlayerScript : NetworkBehaviour
{
    public bool GameStarted;
    
    void Start()
    {
        GameStarted = false;
    }

    public void IssueStartGameCommand()
    {
        if (isServer) {
            CmdStartGame();
        }
    }

    [Command]
    public void CmdStartGame()
    {
        Debug.Log("CmdStartGame");
        StartGame();
    }

    [Client]
    public void StartGame()
    {
        DebugText.Instance.UpdateText("StartGame");
        Debug.Log("StartGame");
    }
}
