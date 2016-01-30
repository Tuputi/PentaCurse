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

    public HotseatPlayer CurrentPlayer
    {
        get
        {
            return HotseatPlayers[CurrentPlayerIndex];
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
        Debug.Log("Current Player index is " + CurrentPlayerIndex);
        CurrentTimerValue = TurnTimerValue;
        CurrentGameState = HotSeatGameState.Playing;
    }

    public void ChangePlayerTurn()
    {
        CurrentTimerValue = TurnTimerValue;
        ToggleCurrentPlayer();
        Debug.Log("Current Player index is " + CurrentPlayerIndex);
    }
}
