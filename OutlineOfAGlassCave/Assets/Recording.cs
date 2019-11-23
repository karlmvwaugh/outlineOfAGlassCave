using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Recording : MonoBehaviour {
	public bool startedRecording = false;
	private AudioClip clip = null;
	// Use this for initialization
	void Start () {
	}

	void Update () {

	}

	public void StartRecording() {
		if (! startedRecording) {
			BeginRecording();
		} 
	}

	public AudioClip GetClip() {
		return clip; 
	}

	void BeginRecording() {
		clip = Microphone.Start(null, true, 30, 44100);
		startedRecording = true;
	}

}


public class AudioTransfer {
	public float frequency;
	public float volume; 


}
