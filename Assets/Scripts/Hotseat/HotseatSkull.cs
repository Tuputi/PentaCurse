using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HotseatSkull : MonoBehaviour {

    private Slider Slider;
	// Use this for initialization
	void Start ()
    {
        Slider = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Slider.value = Mathf.Lerp(Slider.value, HotseatManager.Instance.CurrentVictoryValue, 0.05f);
	}
}
