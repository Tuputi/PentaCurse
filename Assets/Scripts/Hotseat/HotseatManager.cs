using UnityEngine;
using System.Collections;

public class HotseatManager : Manager<HotseatManager>
{
    public GameObject SpellCardPRefab;
    public GameObject RestartButtonPrefab;
    public GameObject SkullPrefab;

    //cloud + damage
    public GameObject cloud;
    public GameObject damage1;
    public GameObject damage2;

    public float VictoryScore = 50;
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
        //FallbackClouds(false);
        DamageBlood(false, 1);
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
        ClearSkulls();
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
        
        if (CurrentGameState != HotSeatGameState.Victory) {
            CurrentPlayer.PlayerBoard.Enable();
        }
    }

    public void TakeDamage()
    {
        if(CurrentPlayerIndex == 1) {
            CurrentVictoryValue -= SuccessfullSpellDamageIncrease;
            DamageBlood(true, 1);
        } else {
            CurrentVictoryValue += SuccessfullSpellDamageIncrease;
            DamageBlood(true, 2);
        }

        if(Mathf.Abs(CurrentVictoryValue) == VictoryScore) {
            SetVictory();
        }
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

            var result = spell.GetResultForSpell(CurrentSpell);

            Debug.Log("Result = " + result);
            if(result == SpellResult.Equal) {
                ResetCurrentDamagePool();
                CurrentSpell = spell;
            } else if(result == SpellResult.Winning) {
                CurrentSpell = spell;
               // CurrentTopCard.MoveTowardPlayer();
				SoundScript.Instance.PlaySound(SoundScript.Instance.winspell);
            } else if(result == SpellResult.Losing) {
				SoundScript.Instance.PlaySound(SoundScript.Instance.losespell);
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
        spellCard.transform.localScale = new Vector3(3, 3, 3);
        CurrentTopCard = spellCard.GetComponent<SpellCard>();
        CurrentTopCard.Init();
        CurrentTopCard.Image.sprite = spell.RuneSymbol;

        if (spell.SpellName.Equals("Fallback"))
        {
            FallbackClouds(true);
        }

        return CurrentTopCard;
    }

    public void SetVictory()
    {
        CurrentGameState = HotSeatGameState.Victory;

        foreach(var player in HotseatPlayers) {
            player.PlayerBoard.Disable();
        }

        InstantiateRetstartButton();
        InstantiateSkull();
		SoundScript.Instance.PlaySound(SoundScript.Instance.evillaugh);
    }

    public void InstantiateRetstartButton()
    {
        var restartButton = GameObject.Instantiate(RestartButtonPrefab) as GameObject;
        restartButton.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform);
        restartButton.transform.localPosition = Vector3.zero;
        restartButton.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
    }

    public void InstantiateSkull()
    {
        var skull = GameObject.Instantiate(SkullPrefab) as GameObject;
        skull.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform);
        skull.transform.position = CurrentPlayer.PlayerBoard.transform.position;
		if (CurrentPlayerIndex == 1) {
			skull.transform.localScale = new Vector3 (3, 3, 3);
		} else {
			skull.transform.localScale = new Vector3 (3, -3, 3);
		}
    }

    public void ClearSkulls()
    {
        foreach(var skull in GameObject.FindObjectsOfType<DeathSkull>()) {
            GameObject.Destroy(skull.gameObject);
        }
    }

    public void FallbackClouds(bool active)
    {
        cloud.SetActive(active);
    }

    public void DamageBlood(bool active, int activePLayerIndex)
    {
        if (!active)
        {
            damage1.SetActive(false);
            damage2.SetActive(false);
        }

        if (activePLayerIndex == 1)
        {
            damage1.SetActive(active);
        }
        else
        {
            damage2.SetActive(active);
        }
    }
}
