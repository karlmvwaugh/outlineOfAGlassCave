using UnityEngine;
using System.Collections;

public class Fireables : MonoBehaviour {

	public WordControl wordControl;
	public SoundControl soundControl;
	public DebugVolume debugVolume;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public void Fire(float rawVolume, float intensity, int ms) {
		wordControl.Fire(intensity);
		soundControl.Fire(ms);
		debugVolume.Fire(rawVolume);

	}

	public void Unfire(float rawVolume) {
		wordControl.Unfire();
		soundControl.Unfire();
		debugVolume.Unfire(rawVolume);
	}
}
