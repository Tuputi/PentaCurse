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
        NetworkManager.singleton.StartClient();
        Debug.Log("Press Client");
        CurrentState = NetworkState.IsClient;
    }

    public void PressStart()
    {
        ClientScene.Ready(NetworkManager.singleton.client.connection);

        //NetworkClient.Start
        Debug.Log("Press Start");
    }

    public void PressBack()
    {
        NetworkManager.singleton.StartHost();
        CurrentState = NetworkState.None;
    }
}
