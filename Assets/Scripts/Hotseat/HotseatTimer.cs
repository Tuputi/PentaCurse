using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Slider))]
public class HotseatTimer : MonoBehaviour {

    public Slider Slider;
	// Use this for initialization
	void Start ()
    {
        Slider = GetComponent<Slider>();
	}

    // Update is called once per frame
    void Update()
    {
        var timerFactor = 1.0f;
         if (HotseatManager.Instance.CurrentGameState == HotSeatGameState.Countdown) {
            timerFactor = HotseatManager.Instance.CurrentTimerValue / HotseatManager.Instance.CountdownTimer;
        } else if (HotseatManager.Instance.CurrentGameState == HotSeatGameState.Playing) {
            timerFactor = HotseatManager.Instance.CurrentTimerValue / HotseatManager.Instance.CurrentResetTimerValue;
        }

        Slider.value = timerFactor;
	}
}
