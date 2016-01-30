using UnityEngine;
using System.Collections;

public class AmbienceScript : Manager <AmbienceScript>{

	public AudioSource ambience;

	// Use this for initialization
	void Start () {
		ambience = GetComponent<AudioSource>();
		ambience.Play ();
	}

	// Update is called once per frame
	void Update () {

	}
		
}

