using UnityEngine;
using System.Collections;

public class DebugVolume : MonoBehaviour {
	private SpriteRenderer renderer;
	// Use this for initialization
	void Start () {
		renderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Fire(float volume) {
		var newVolume = Mathf.Pow(volume, 0.25f);
		var currentScale = renderer.transform.localScale;
		renderer.transform.localScale = new Vector3(currentScale.x, newVolume, currentScale.z);
	}
}
