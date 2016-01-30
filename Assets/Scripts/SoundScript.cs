using UnityEngine;
using System.Collections;

public class SoundScript : Manager <SoundScript>{

	public AudioClip spellcast;
	public AudioClip selectsound;
	AudioSource audiosource;

	// Use this for initialization
	void Start () {
		audiosource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlaySound(AudioClip clip){
		audiosource.PlayOneShot(clip);
	}
}
