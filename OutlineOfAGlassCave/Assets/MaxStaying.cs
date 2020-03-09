using UnityEngine;
using System;
using System.Collections;

public class MaxStaying : MonoBehaviour {
	public WordControl wordControl;
	public SoundControl soundControl;

	private DateTime lastTime;
	private int value = 5;
	// Use this for initialization
	void Start () {
		lastTime = DateTime.Now;
		SetValues();
	}
	
	// Update is called once per frame
	void Update () {
		if (IsTime(20000)) {
			GetNextValue();
			SetValues();

		}
	}

	void GetNextValue() {
		if (value < 13) {
			value++;
			return;
		}

		value = 5;
		return;
	}

	void SetValues() {
		wordControl.maxNumberOfWords = value;
		soundControl.maxNumberOfLoops = value;
	}


	Boolean IsTime(int window) {
		var now = DateTime.Now;
		var difference = (now - lastTime).TotalMilliseconds;
		if (difference > window) {
			lastTime = now;
			return true;
		}
		return false; 
	}

}
