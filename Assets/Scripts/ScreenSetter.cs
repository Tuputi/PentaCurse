using UnityEngine;
using System.Collections;

public class ScreenSetter : MonoBehaviour {

	// Use this for initialization
	void Start () {

        #if (UNITY_STANDALONE)
        Screen.SetResolution(576, 1024, false);
        #endif
    }

    // Update is called once per frame
    void Update () {
	
	}
}
