using UnityEngine;
using System.Collections;

public class rotator : MonoBehaviour {

	public float rotation; 
	public float opacity;
	
	// Use this for initialization
	void Start () {
		Color c = renderer.material.color;
		c.a = opacity ;
		renderer.material.color = c; 
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.forward * rotation); //rotate at steady speed. 
	}
}
