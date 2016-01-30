using UnityEngine;
using System.Collections;

public class SinglePlayerTouch : MonoBehaviour {

    public TouchInput TouchInput;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(TouchInput == null) {
            TouchInput = GameObject.FindObjectOfType<TouchInput>();
        }

        TouchInput.UpdateInput(true);
	}
}
