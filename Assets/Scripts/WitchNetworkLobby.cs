using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(NetworkLobbyManager))]
public class WitchNetworkLobby : MonoBehaviour
{
    public enum NetworkState
    {
        None,
        IsHost,
        IsClient
    }

    public NetworkState CurrentState = NetworkState.None;
    void Start()
    {
    }

    public void PressHost()
    {
        NetworkManager.singleton.StartHost();
        Debug.Log("Press Host");
        CurrentState = NetworkState.IsHost;
    }

    public void PressClient()
    {
        var client = NetworkManager.singleton.StartClient();
        client.Connect("localhost", 7777);
        Debug.Log("Press Client");
        CurrentState = NetworkState.IsClient;
    }

    public void PressStart()
    {
        if (PlayerLobbyCharacter.LocalInstance != null) {
            PlayerLobbyCharacter.LocalInstance.SendReadyToBeginMessage();
            Debug.Log("Press Start");
        } else {
            ClientScene.Ready(NetworkManager.singleton.client.connection);
        }
    }

    public void PressBack()
    {
        NetworkManager.singleton.StartHost();
        CurrentState = NetworkState.None;
    }
}
