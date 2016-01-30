using UnityEngine;
using System.Collections;

public class HotseatManager : Manager<HotseatManager>
{
    public float CountdownTimer = 5;
    public float SuccessfullSpellDamageIncrease = 5;
    public float TurnTimerValue = 3;
    public HotseatPlayer[] HotseatPlayers;

    public HotSeatGameState CurrentGameState;
    public int CurrentPlayerIndex;
    public float CurrentTimerValue;
    public float CurrentDamagePool;
    public float CurrentVictoryValue;

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
            if(CurrentPlayerIndex == 0) {
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

        foreach(var player in HotseatPlayers) {
            player.ResetPlayer();
        }
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
        OtherPlayer.PlayerBoard.Disable();
        Debug.Log("Current Player index is " + CurrentPlayerIndex);
        CurrentTimerValue = TurnTimerValue;
        CurrentGameState = HotSeatGameState.Playing;
    }

    public void ChangePlayerTurn()
    {
        CurrentPlayer.PlayerBoard.Disable();
        RuneTouch.Instance.ClearRunes();
        CurrentTimerValue = TurnTimerValue;
        ToggleCurrentPlayer();
        Debug.Log("Current Player index is " + CurrentPlayerIndex);
        CurrentPlayer.PlayerBoard.Enable();
    }

    public void TakeDamage(float amount)
    {
        if(CurrentPlayerIndex == 0) {
            CurrentVictoryValue -= amount;
        } else {
            CurrentVictoryValue += amount;
        }
    }

    public void CastSpell(Spell spell)
    {
        if(CurrentSpell == null) {
            CurrentSpell = spell;
        } else {

            var result = SpellUtilties.GetResult(CurrentSpell, spell);

            if(result == SpellResult.Equal) {
                ResetCurrentDamagePool();
            }else if(result == SpellResult.Winning) {
                CurrentDamagePool += SuccessfullSpellDamageIncrease;
            }else if(result == SpellResult.Losing) {
                TakeDamage(CurrentDamagePool);
                ResetCurrentDamagePool();
            }
            CurrentSpell = spell;
        }
    }
}
