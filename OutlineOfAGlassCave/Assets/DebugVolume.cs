﻿using UnityEngine;
using System.Collections;

public class DebugVolume : MonoBehaviour {
	private SpriteRenderer renderer;
	// Use this for initialization
	void Start () {
		renderer = GetComponent<SpriteRenderer>();
		renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0.3f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Fire(float volume) {
		setVolume(volume);
		setOpacity(0.4f);
	}

	public void Unfire(float volume) {
		setVolume(volume);
		setOpacity(0.2f);
	}

	void setVolume(float volume) {
		var newVolume = Mathf.Pow(volume, 0.25f);
		var currentScale = renderer.transform.localScale;
		renderer.transform.localScale = new Vector3(currentScale.x, newVolume, currentScale.z);
	}

	void setOpacity(float opacity) {
		renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, opacity);
	}
}
