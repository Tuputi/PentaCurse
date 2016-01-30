using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerScript : NetworkBehaviour
{
    public static PlayerScript s_localInstance;
    public static PlayerScript s_otherInstance;

    public static PlayerScript LocalInstance
    {
        get
        {
            if (s_localInstance == null) {
                var players = GameObject.FindObjectsOfType<PlayerScript>();
                foreach (var player in players) {
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

    public GameObject EnemyRunePrefab;

    private TouchInput TouchInput;

    [SyncVar(hook = "SetCurrentSpellIndex")]
    public int CurrentSpellIndex;

    public Spell CurrentSpell;

    [SyncVar( hook = "SetCurrentHealth")]
    public float CurrentHealth = 100;

    void Start()
    {
    }

    void Update()
    {
        if (TouchInput == null) {
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
        for (int i = 0; i < SpellList.Instance.spells.Count; i++) {
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
        if (index == -1) {
            CurrentSpellIndex = index;
            CurrentSpell = SpellList.Instance.fallBack;
        } else {
            CurrentSpellIndex = index;
            CurrentSpell = SpellList.Instance.spells[index];
        }

        if (!isLocalPlayer) {
            Debug.Log("Enemy set their spell");

            foreach(var rune in GameObject.FindObjectsOfType<EnemyRune>()) {
                GameObject.Destroy(rune.gameObject);
            }

            var tmpObject = GameObject.Instantiate(EnemyRunePrefab, Vector3.zero, Quaternion.identity) as GameObject;
            tmpObject.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform, false);
            tmpObject.transform.localPosition = new Vector3(0, 900, 0);
            tmpObject.GetComponent<Image>().sprite = CurrentSpell.RuneSymbol;
        }
    }

    public void SetCurrentHealth(float health)
    {
        CurrentHealth = health;
        CmdIssueSetHealth(health);
    }

    [Command]
    public void CmdIssueSetHealth(float health)
    {
        CurrentHealth = health;
        UpdateHealth(health);

    }
    [Client]
    public void UpdateHealth(float health)
    {
        CurrentHealth = health;
    }
}
