using UnityEngine;
using System.Collections;

public class HotseatManager : Manager<HotseatManager>
{
    public GameObject SpellCardPRefab;

    public float CountdownTimer = 5;
    public float SuccessfullSpellDamageIncrease = 5;
    public float TurnTimerValue = 3;
    public HotseatPlayer[] HotseatPlayers;

    public HotSeatGameState CurrentGameState;
    public int CurrentPlayerIndex;
    public float CurrentTimerValue;
    public float CurrentDamagePool;
    public float CurrentVictoryValue;

    public SpellCard CurrentTopCard;

    public Spell CurrentSpell;

    public HotseatPlayer CurrentPlayer
    {
        get
        {
            return HotseatPlayers[CurrentPlayerIndex];
        }
    }

    public HotseatPlayer OtherPlayer
    {
        get
        {
            if(CurrentPlayerIndex == 1) {
                return HotseatPlayers[1];
            } else {
                return HotseatPlayers[0];
            }
        }
    }

    // Use this for initialization
    void Start ()
    {
        HotseatPlayers = GameObject.FindObjectsOfType<HotseatPlayer>();
        ResetBoard();
	}

    public HotseatPlayer ToggleCurrentPlayer()
    {
        if(CurrentPlayerIndex == 0) {
            CurrentPlayerIndex = 1;
        } else {
            CurrentPlayerIndex = 0;
        }

        return HotseatPlayers[CurrentPlayerIndex];
    }

    public void ResetCurrentDamagePool()
    {
        CurrentDamagePool = 0;
    }


	// Update is called once per frame
	void Update ()
    {
        if (CurrentGameState == HotSeatGameState.Initial) {
            return;
        } else if (CurrentGameState == HotSeatGameState.Countdown) {
            CurrentTimerValue -= Time.deltaTime;
            if (CurrentTimerValue <= 0) {
                StartGame();
            }
        } else if (CurrentGameState == HotSeatGameState.Playing) {
            CurrentTimerValue -= Time.deltaTime;
            if (CurrentTimerValue <= 0) {
                CastSpell(SpellList.Instance.fallBack);
                ChangePlayerTurn();
            }
        }
	}

    public void ResetBoard()
    {
        foreach(var player in HotseatPlayers) {
            player.PlayerBoard.Disable();
        }

        CurrentGameState = HotSeatGameState.Initial;
        CurrentTimerValue = CountdownTimer;
        CurrentVictoryValue = 0;
        CurrentDamagePool = 0;
    }

    public void SetCountdownPhase()
    {
        Debug.Log("SetCountdownPhase");
        CurrentGameState = HotSeatGameState.Countdown;
    }

    public void StartGame()
    {
        Debug.Log("StartGame");
        CurrentPlayerIndex = 0;
        CurrentPlayer.PlayerBoard.Enable();
        Debug.Log("Current Player index is " + CurrentPlayerIndex);
        CurrentTimerValue = TurnTimerValue;
        CurrentGameState = HotSeatGameState.Playing;
    }

    public void ChangePlayerTurn()
    {
        GameManager.Instance.ClearCurrentSpell();
        CurrentPlayer.PlayerBoard.Disable();
        RuneTouch.Instance.ClearRunes();
        CurrentTimerValue = TurnTimerValue;
        ToggleCurrentPlayer();
        Debug.Log("Current Player index is " + CurrentPlayerIndex);
        CurrentPlayer.PlayerBoard.Enable();
    }

    public void TakeDamage()
    {
        if(CurrentPlayerIndex == 1) {
            CurrentVictoryValue -= SuccessfullSpellDamageIncrease;
        } else {
            CurrentVictoryValue += SuccessfullSpellDamageIncrease;
        }

        Debug.Log("CurrentVictoryValue: " + CurrentVictoryValue);
    }

    public void CastSpell(Spell spell)
    {
        if(spell == null) {
            spell = SpellList.Instance.fallBack;
        }

        InstantiateSpellCard(spell);
        if(CurrentSpell == null) {
            CurrentSpell = spell;

        } else {

            var result = SpellUtilties.GetResult(CurrentSpell, spell);

            Debug.Log("Result = " + result);
            if(result == SpellResult.Equal) {
                ResetCurrentDamagePool();
                CurrentSpell = spell;
            } else if(result == SpellResult.Winning) {
                CurrentSpell = spell;
            } else if(result == SpellResult.Losing) {
                TakeDamage();
                CurrentSpell = null;
                GameObject.Destroy(CurrentTopCard.gameObject);
            }
        }
    }

    public SpellCard InstantiateSpellCard(Spell spell)
    {
        var spellCard = GameObject.Instantiate(SpellCardPRefab, Vector3.zero, Quaternion.identity) as GameObject;
        spellCard.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform);
        spellCard.transform.position = CurrentPlayer.PlayerBoard.transform.position;
        CurrentTopCard = spellCard.GetComponent<SpellCard>();
        CurrentTopCard.Init();
        CurrentTopCard.Image.sprite = spell.RuneSymbol;

        return CurrentTopCard;
    }
}
