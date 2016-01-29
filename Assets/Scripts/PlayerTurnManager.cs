using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerTurnManager : NetworkBehaviour
{
    public PlayerScript[] PlayerScripts;
    public PlayerScript CurrentPlayer;

    [SyncVar(hook = "SetTimeInterval")]
    public float TimerInterval;

    [SyncVar(hook = "SetTimerHook")]
    public float CurrentTimer;

    [SyncVar(hook = "SetIsActive")]
    public bool IsActive;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        OnUpdate();
	}

    public void OnUpdate()
    {
        CurrentTimer -= Time.deltaTime;
        if(CurrentTimer <= 0) {
            if (isServer) {
                OnTimerExpires();
            }
        }

        var scaleFactor = CurrentTimer / TimerInterval;
        transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }

    public void OnTimerExpires()
    {
        Debug.Log("Server timer expires");
        ResetTimer();
    }

    public void ResetTimer()
    {
        CurrentTimer = TimerInterval;
    }

    public void SetTimeInterval(float interval)
    {
        TimerInterval = interval;
    }

    public void SetTimerHook(float timer)
    {
        CurrentTimer = timer;
    }

    public void SetIsActive(bool isActive)
    {

    }
}
