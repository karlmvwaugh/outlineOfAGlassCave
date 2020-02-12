using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class VolumeSampler : MonoBehaviour {
	public Recording recorder;
	public AudioSource fakeAudioSource;
	public int windowMs = 100;
	public float threshold = 0.1f; 
	public Fireables fireables;

	private int sampleDataLength = 1024;
	private int delay = 1024;
	private int maxAcceptableLag = 4000;
	private AudioClip clip; 
	private DateTime lastTime = DateTime.Now;
	private DateTime startTime;
	private float[] clipData;
	private Rect debugRect = new Rect(0f, 0f, 50f, 50f);
	private GUIStyle style = new GUIStyle ();

	// Use this for initialization
	void Start () {
		clipData = new float[sampleDataLength];
		recorder.StartRecording();
		clip = recorder.GetClip();


		startTime = DateTime.Now;
		lastTime = DateTime.Now;
		SetupChecks();

		style.fontSize = 10;
		style.normal.textColor = Color.black;
	}
	
	// Update is called once per frame
	void Update () {
		SetupChecks();
	

		if (IsRunVolume()) {
			RunVolumeCheck();
		}

		maxLagCheck();
	}

	void SetupChecks() {
		if (!fakeAudioSource.isPlaying && isTime(startTime, delay)) {
			fakeAudioSource.clip = clip; 
			fakeAudioSource.volume = 0f;
			fakeAudioSource.loop = true;
			fakeAudioSource.timeSamples = (Microphone.GetPosition(null) - delay);
			fakeAudioSource.Play();
		}
	}

	Boolean IsRunVolume() {
		return fakeAudioSource.isPlaying && isTime(lastTime, windowMs);
	}

	void maxLagCheck() {
		var lag = getLag(); 
		if (lag > maxAcceptableLag) {
			fakeAudioSource.timeSamples = (Microphone.GetPosition(null) - delay);
		}
	}

	void RunVolumeCheck() {
		// clip = recorder.GetClip();

		LoadDataIntoClipData();
		var volume = GetMaxVolume();

		if (volume > threshold) {
			FireWithIntensity(volume);
		} else {
			fireables.Unfire(volume);
		}
	}

	void FireWithIntensity(float volume) {
		var intensity = getIntensity(volume);
		fireables.Fire(volume, intensity, windowMs);
	}

	float getIntensity(float volume) {
		return (volume - threshold) / (1f - threshold);
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


	void OnGUI(){
		GUI.Label(debugRect, "LAG=" + getLag(), style);
	}

	int getLag() {
		return Microphone.GetPosition(null) - fakeAudioSource.timeSamples;
	}
}
