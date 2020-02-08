using UnityEngine;
using System;
using System.Collections;

public class Word : MonoBehaviour {
	public string theWord;
	public string actualWord;
	public float fadeInTime;
	public float fadeOutTime;
	public float typeStep = 123;
	public float typeTime;

	public Boolean fadeIn;
	public Boolean fadeOut; 
	
	public int fontSize;
	private GUIStyle style;
	private Rect rect;
	private static System.Random rand = new System.Random();
	private Color invisible;
	private Color mainColour;
	private DateTime startTime;
	private bool started = false;
	private bool fadingOut = false;
	private DateTime fadeOutStartTime;

	// Use this for initialization
	void Start () {

	}

	void initStyle() {
		style = new GUIStyle ();

		style.fontSize = fontSize;
		mainColour = getColour();
		invisible = new Color(mainColour.r, mainColour.g, mainColour.b, 0f);
		style.normal.textColor = fadeIn ? invisible : mainColour;
		// var r = rand.Next(5);
		// if (r == 0 || r == 1){
		// 	style.fontStyle = FontStyle.Italic;
		// } else if(r == 2) {
		// 	style.fontStyle = FontStyle.Bold;
		// } else {
			style.fontStyle = FontStyle.Normal; 

		// }
		var x = (float)rand.NextDouble()*0.9f*Screen.width; // - 0.5f*Screen.width;
		var y = (float)rand.NextDouble()*0.9f*Screen.height; // - 0.5f*Screen.height;
		rect = new Rect(x, y, 100, 100);
	}

	
	public void Init(string word, int size, float fadeIn, float fadeOut){
		fontSize = size;
		initStyle();
		theWord = "";
		actualWord = word;

		typeTime = (word.Length + 1) * typeStep;
		fadeOutTime = fadeIn;
		fadeOutTime = fadeOut;
		startTime = DateTime.Now;
		started = true;
	}

	
	public void StartFadeOut() {
		if (fadingOut) { return; }

		fadeOutStartTime = DateTime.Now;
		fadingOut = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (! started) return; 
		
		var now = DateTime.Now;
		var dif = (float)(now - startTime).TotalMilliseconds;

		var fadeDif = fadingOut ? (float)(now - fadeOutStartTime).TotalMilliseconds : float.PositiveInfinity;

		typeOut(dif);
		if (fadeIn && dif < fadeInTime){
			fadeInColour(dif);
		} else if (fadingOut && fadeDif > fadeOutTime){
			Destroy(this.gameObject);
		} else if (fadingOut && fadeOut) {
			fadeOutColour(fadeDif);
		}
	}

	void typeOut(float time) {
		if (time < typeTime) {
			var charactersCount = Mathf.FloorToInt(time / typeStep);

			theWord = actualWord.Substring(0, charactersCount);
		}
	}

	void fadeInColour(float dif) {
		var frac = dif / fadeInTime;
		style.normal.textColor = Color.Lerp(invisible, mainColour, frac);
	}

	void fadeOutColour(float dif) {
		var frac = (dif - fadeInTime)/fadeOutTime;
		style.normal.textColor = Color.Lerp(mainColour, invisible, frac);
	}
	
	void OnGUI(){
		GUI.Label(rect, theWord, style);
	}
	
	Color getColour(){
		var maxBrightness = 0.05f;
		var a = (float)(0.2 + 0.5*rand.NextDouble()); 
		var greyShadeR = (float)(maxBrightness*rand.NextDouble());
		var greyShadeG = (float)(maxBrightness*rand.NextDouble());
		var greyShadeB = (float)(maxBrightness*rand.NextDouble());

		
		return new Color(greyShadeR, greyShadeG, greyShadeB, a); 
	}
}
