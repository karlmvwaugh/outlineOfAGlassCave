using UnityEngine;
using System.Collections;

public class FuzzyLine : MonoBehaviour {


	private float minX = -8f;
	private float maxX = 8f;
	private float stepSize;
	private static System.Random rand = new System.Random();

	// Use this for initialization
	void Start () {
		stepSize = 0.007f * (float)rand.NextDouble();
		Color c = renderer.material.color;
		c.a = 0.1f;
		renderer.material.color = c; 

		var position = transform.position;
		transform.position = new Vector3((float)rand.NextDouble()*16f - 8f, position.y, position.z);
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

	private bool state = false;

	bool BurstRandom() {
		ChangeStateMaybe();
		if (state) {
			return HighState();
		} else {
			return LowState();
		}
	}

	void ChangeStateMaybe() {
		var threshold = 0.95;
		if (state) {
			threshold = 0.95;
		}

		if (rand.NextDouble() > threshold) {
			state = !state;
		}
	}

	bool HighState() {
		return (rand.NextDouble() > 0.8);
	}

	bool LowState() {
		return (rand.NextDouble() > 0.2);
	}

}
