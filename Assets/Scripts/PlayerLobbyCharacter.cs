using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerLobbyCharacter : NetworkLobbyPlayer {

    public static PlayerLobbyCharacter s_localInstance;
    public static PlayerLobbyCharacter s_otherInstance;

    public static PlayerLobbyCharacter LocalInstance
    {
        get
        {
            if (s_localInstance == null) {
                var players = GameObject.FindObjectsOfType<PlayerLobbyCharacter>();
                foreach (var player in players) {
                    if (player.isLocalPlayer) {
                        s_localInstance = player;
                    }
                }
            }

            return s_localInstance;
        }
    }

    public static PlayerLobbyCharacter OtherInstance
    {
        get
        {
            if (s_otherInstance == null) {
                var players = GameObject.FindObjectsOfType<PlayerLobbyCharacter>();
                foreach (var player in players) {
                    if (!player.isLocalPlayer) {
                        s_otherInstance = player;
                    }
                }
            }

            return s_otherInstance;
        }
    }
}
