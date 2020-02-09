using UnityEngine;
using System.Collections;
using System; 



public class PitchOscillator : MonoBehaviour {
	private AudioSource source;

	private float initialPitch;
	private bool started = false; 
	private DateTime startTime; 
	private float speed;
	private float width;
	
	private static System.Random rand = new System.Random();

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource>();
		width = 0.10f + 1.9f*(float)rand.NextDouble();
	}
	
	// Update is called once per frame
	void Update () {
		if (started) {
			UpdatePitch();
		}
	}

	public void Init(float pitch) {
		initialPitch = pitch;
		startTime = DateTime.Now;
		speed = 0.001f + (float)rand.NextDouble()*0.399f;
		started = true;

	}

	void UpdatePitch() {
		var oscillation = GetOscillation(); // -1, 1

		source.pitch = initialPitch + oscillation*width*initialPitch;

	}

	float GetOscillation() {
		var now = DateTime.Now;
		var dif = (float)(now - startTime).TotalMilliseconds;

		return Mathf.Sin(speed * 3.14f * dif / 1000f);
	}
}
