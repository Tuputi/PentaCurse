using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class PlayerScript : NetworkBehaviour
{
    public static PlayerScript s_localInstance;
    public static PlayerScript s_otherInstance;

    public static PlayerScript LocalInstance
    {
        get
        {
            if(s_localInstance == null) {
                var players = GameObject.FindObjectsOfType<PlayerScript>();
                foreach(var player in players) {
                    if (player.isLocalPlayer) {
                        s_localInstance = player;
                    }
                }
            }

            return s_localInstance;
        }
    }

    public static PlayerScript OtherInstance
    {
        get
        {
            if (s_otherInstance == null) {
                var players = GameObject.FindObjectsOfType<PlayerScript>();
                foreach (var player in players) {
                    if (!player.isLocalPlayer) {
                        s_otherInstance = player;
                    }
                }
            }

            return s_otherInstance;
        }
    }

    private TouchInput TouchInput;

    [SyncVar(hook = "SetCurrentSpellIndex")]
    public int CurrentSpellIndex;

    public Spell CurrentSpell;
    public float CurrentHealth = 100;

    void Start()
    {
    }

    void Update()
    {
        if(TouchInput == null) {
            TouchInput = AquireInput();
        } else {
            TouchInput.UpdateInput(isLocalPlayer);
        }
    }

    public TouchInput AquireInput()
    {
        return GameObject.FindObjectOfType<TouchInput>();
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

    public void SetCurrentSpell(Spell spell)
    {
        var index = -1;
        for(int i = 0; i < SpellList.Instance.spells.Count; i++) {
            if (SpellList.Instance.spells[i] == spell) {
                index = i;
            }
        }

        CurrentSpellIndex = index;
        CmdSetCurrentSpellIndex(index);
    }

    [Command]
    public void CmdSetCurrentSpellIndex(int index)
    {
        SetCurrentSpellIndex(index);
    }

    [Client]
    public void SetCurrentSpellIndex(int index)
    {
        Debug.Log(index);
        if (index == -1) {
            CurrentSpellIndex = index;
            CurrentSpell = SpellList.Instance.fallBack;
        } else {
            CurrentSpellIndex = index;
            CurrentSpell = SpellList.Instance.spells[index];
        }
        Debug.Log(CurrentSpell.SpellName);
    }

    public void ChangeCurrentHealth(float health)
    {
        if (hasAuthority) {
            CurrentHealth += health;
            CmdSetCurrentHealth(CurrentHealth);
        }
    }

    [Command]
    public void CmdSetCurrentHealth(float health)
    {
        ClientSetHealt(health);
    }

    [Client]
    public void ClientSetHealt(float health)
    {
        CurrentHealth = health;
    }
}
