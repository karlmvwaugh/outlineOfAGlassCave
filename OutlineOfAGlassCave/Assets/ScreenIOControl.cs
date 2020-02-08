using UnityEngine;
using System.Collections;

public class ScreenIOControl : MonoBehaviour {
	public VolumeSampler volumeSampler;
	public WordControl wordControl;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (IsTouch ()) {
			processXCoord(touchCoordinate.x);
			processYCoord(touchCoordinate.y);

		}
	}


	void processYCoord(float y) {
		volumeSampler.threshold = y;
	}

	void processXCoord(float x) {
		wordControl.sizeFactor = x;
	}

	void guiUpdates() {

	}



	private Vector2 touchCoordinate;
	
	private bool IsTouch(){
		var isMouse = isMouseTouch();
		if (isMouse) return isMouse; 
		
		return isPhoneTouch(); 
	}
	
	private bool isMouseTouch(){		
		if (Input.GetMouseButton(0)){
			touchCoordinate = Input.mousePosition;
			touchCoordinate.x /= Screen.width;
			touchCoordinate.y /= Screen.height;
			return true;
		}
		
		return false; 
	}
	
	private bool isPhoneTouch(){
		if (Input.touchCount > 0) {
			Touch tou = Input.GetTouch(0);
			touchCoordinate = tou.position;
			touchCoordinate.x /= Screen.width;
			touchCoordinate.y /= Screen.height;
			return true;
		}
		
		return false;
	}

}
