﻿using UnityEngine;
using System.Collections;

public class SoundScript : Manager <SoundScript>{

	public AudioClip spellcast;
	public AudioClip selectsound;
	public AudioClip spellfail;
	public AudioClip boom;
	public AudioClip winspell;
	public AudioClip losespell;
	public AudioClip evillaugh;
	public AudioClip click;
	AudioSource audiosource;
	float selectsoundcount;

	// Use this for initialization
	void Start () {
		audiosource = GetComponent<AudioSource>();
		selectsoundcount = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlaySound(AudioClip clip){
		if (clip == selectsound) {
			selectsoundcount++;
			audiosource.pitch = (selectsoundcount / 20) + audiosource.pitch;
		} else {
			audiosource.pitch = 1;
		}
		audiosource.PlayOneShot(clip);
	}

	public void LetGo()
    {
        audiosource.pitch = 1;
        selectsoundcount = 0;
	}
}
