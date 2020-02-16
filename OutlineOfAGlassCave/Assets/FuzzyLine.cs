using UnityEngine;
using System.Collections;

public class FuzzyLine : MonoBehaviour {


	private float minX = -8f;
	private float maxX = 8f;
	private float stepSize;
	private static System.Random rand = new System.Random();
	private double stateChangeThreshold = 0.95;
	private double highState = 0.8;
	private double lowState = 0.2;
	private bool state = false;


	// Use this for initialization
	void Start () {
		Color c = renderer.material.color;
		c.a = 0.1f;
		renderer.material.color = c; 

		var position = transform.position;
		transform.position = new Vector3((float)rand.NextDouble()*16f - 8f, position.y, position.z);


		stepSize = 0.008f * (float)rand.NextDouble();
		state = rand.NextDouble() > 0.5;
		stateChangeThreshold = 0.97 + 0.02999*rand.NextDouble();
		highState = 0.6 + 0.3*rand.NextDouble();
		lowState = 1.0 - highState;

	}
	
	// Update is called once per frame
	void Update () {
		var position = transform.position;
		var newX = position.x; 

		if (BurstRandom()) {
			newX = modXValue(newX + stepSize*(float)rand.NextDouble());
		} else {
			newX = modXValue(newX - stepSize*(float)rand.NextDouble());
		}

		transform.position = new Vector3(newX, position.y, position.z);
	}

	float modXValue(float xValue) {
		if (xValue > maxX) {
			return modXValue(xValue - (maxX - minX));
		}

		if (xValue < minX) {
			return modXValue(xValue + (maxX - minX));
		}
		return xValue;
	}


	bool BurstRandom() {
		ChangeStateMaybe();
		if (state) {
			return HighState();
		} else {
			return LowState();
		}
	}

	void ChangeStateMaybe() {
		if (rand.NextDouble() > stateChangeThreshold) {
			state = !state;
		}
	}

	bool HighState() {
		return (rand.NextDouble() > highState);
	}

	bool LowState() {
		return (rand.NextDouble() > lowState);
	}

}
