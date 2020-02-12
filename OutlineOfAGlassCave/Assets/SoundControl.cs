using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class SoundControl : MonoBehaviour {
	public AudioSource theRecorder;
	public GameObject player;
	public string debug = "";

	private int maxNumberOfLoops = 4;
	private DateTime startTime; 
	private DateTime endTime;
	private static System.Random rand = new System.Random();
	private List<GameObject> gameObjects = new List<GameObject>();

	private Boolean ready = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Fire(int msOffSet) {
		if (ready) { return; }
		startTime = DateTime.Now.AddMilliseconds(-1.0 * msOffSet);
		ready = true;
	}

	public void Unfire() {
		if (! ready) { return; }
		endTime = DateTime.Now;
		orchestrateAudio();
		sortOutCleanUpOfObjects();
		ready = false;

	}

	void sortOutCleanUpOfObjects() {
		if (gameObjects.Count > maxNumberOfLoops) {
			removeDeadObjects();
			triggerDyingObjects();
		}
	}

	void removeDeadObjects() {
		var newList = new List<GameObject>();
		foreach(var ob in gameObjects) {
			if (ob != null) {
				newList.Add(ob);
			}
		}
		gameObjects = newList;
	}

	void triggerDyingObjects() {
		if (gameObjects.Count > maxNumberOfLoops) {
			var numberToEvict = gameObjects.Count - maxNumberOfLoops;

			for (var i = 0; i < numberToEvict; i++) {
				var fadeOutAndDestroy = gameObjects[i].GetComponent<FadeOutAndDestroy>();
				fadeOutAndDestroy.Init(2000f + 8000f*(float)rand.NextDouble());
			}
		}
	}


	void orchestrateAudio() {
		var newClip = createAudioClip();
		var ob = createAudioSource(newClip);
		gameObjects.Add(ob); 
	}


	GameObject createAudioSource(AudioClip newClip) {
		var ob = (GameObject)Instantiate(player);
		var newSource = ob.GetComponent<AudioSource>();
		// kick off 
		newSource.clip = newClip;
		newSource.loop = true;
		newSource.pitch = 0.01f + (float)rand.NextDouble()*0.14f;
		newSource.volume = 0.4f + (float)rand.NextDouble()*0.2f;
		newSource.Play();


		var pitchOsc = ob.GetComponent<PitchOscillator>();
		pitchOsc.Init(newSource.pitch); 

		return ob;
	}

	AudioClip createAudioClip() {
		//debug = "stripping from " + startTime + " to " + endTime; 
		var recordingClip = theRecorder.clip;

		var lengthOfClip = (endTime - startTime).TotalMilliseconds;
		var samplesOfNewClip = Mathf.RoundToInt((float)lengthOfClip * 44.1f);
		var currentPosition = theRecorder.timeSamples;
		var offSet = currentPosition - samplesOfNewClip;


		var soundData = new float[recordingClip.samples * recordingClip.channels];
		recordingClip.GetData (soundData, 0);

		var newData = new float[samplesOfNewClip * recordingClip.channels];
		
		//Copy the used samples to a new array
		for (int i = 0; i < newData.Length; i++) {
			newData[i] = modDataPoint(soundData, i + offSet);
		}

		var newClip = AudioClip.Create (recordingClip.name,
		                                samplesOfNewClip,
		                                recordingClip.channels,
		                                recordingClip.frequency,
		                                false,
		                                false);
		
		newClip.SetData (newData, 0);   
		return newClip; 

	}

	float modDataPoint(float[] soundData, int index) {
		if (index < 0) {
			return modDataPoint(soundData, index + soundData.Length);
		} 
		if (index >= soundData.Length) {
			return modDataPoint(soundData, index - soundData.Length);
		}

		return soundData[index];
	}
}
