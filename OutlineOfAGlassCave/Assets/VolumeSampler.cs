using UnityEngine;
using System;
using System.Collections;

public class VolumeSampler : MonoBehaviour {
	public Recording recorder;
	public AudioSource fakeAudioSource;
	public int windowMs = 100;
	public float threshold = 0.1f; 
	public string debugOutput = "";

	private int sampleDataLength = 1024;
	private AudioClip clip; 
	private DateTime lastTime = DateTime.Now;
	private DateTime startTime;
	private float[] clipData;

	// Use this for initialization
	void Start () {
		clipData = new float[sampleDataLength];
		recorder.StartRecording();
		clip = recorder.GetClip();


		startTime = DateTime.Now;
		lastTime = DateTime.Now;
	}
	
	// Update is called once per frame
	void Update () {
		SetupChecks();
	

		if (IsRunVolume()) {
			RunVolumeCheck();
		}
	}

	void SetupChecks() {
		if (!fakeAudioSource.isPlaying && isTime(startTime, sampleDataLength)) {
			fakeAudioSource.clip = clip; 
			fakeAudioSource.volume = 1f;
			fakeAudioSource.loop = true;
			fakeAudioSource.Play();
		}
	}

	Boolean IsRunVolume() {
		return fakeAudioSource.isPlaying && isTime(lastTime, windowMs);
	}

	void RunVolumeCheck() {
		// clip = recorder.GetClip();

		LoadDataIntoClipData();
		var volume = GetMaxVolume();

		if (volume > threshold) {
			debugOutput = "fire " + volume;
		} else {
			debugOutput = "";
		}
	}


	float GetMaxVolume() {
		var runningVolume = 0f;
		foreach(var sample in clipData) {
			runningVolume += Mathf.Abs(sample);
		}
		return runningVolume / sampleDataLength;
	}

	void LoadDataIntoClipData() {
		var sampleAt = Math.Max(fakeAudioSource.timeSamples - sampleDataLength, 0);
		clip.GetData(clipData, sampleAt);
	}

	Boolean isTime(DateTime baseTime, int window) {
		var now = DateTime.Now;
		var difference = (now - baseTime).TotalMilliseconds;
		if (difference > window) {
			lastTime = now;
			return true;
		}
		return false; 
	}
}
