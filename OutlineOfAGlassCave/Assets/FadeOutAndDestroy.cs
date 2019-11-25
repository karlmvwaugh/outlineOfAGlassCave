using UnityEngine;
using System;
using System.Collections;

public class FadeOutAndDestroy : MonoBehaviour {
	private DateTime startTime;
	private float fadeOutTime;
	private float initialVolume;
	private bool started;
	private AudioSource source;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (started) {
			var now = DateTime.Now;
			var dif = (now - startTime).TotalMilliseconds;
			var fraction = (float)dif / fadeOutTime;
			var newVolume = Math.Max(initialVolume * (1f - fraction), 0f);

			source.volume = newVolume;

			if (newVolume == 0f) {
				Destroy (this.gameObject);
			}
		}
	}

	public void Init(float time) {
		if (started) { return; }
		source = GetComponent<AudioSource>();
		initialVolume = source.volume;
		startTime = DateTime.Now;
		fadeOutTime = time;
		started = true;

	}
}
