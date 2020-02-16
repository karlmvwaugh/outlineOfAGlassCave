using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class WordDef {
	public WordDef(string w, float s) {
		Word = w;
		Size = s;
	}

	public string Word; 
	public float Size;
}

public class WordControl : MonoBehaviour {
	public GameObject rawWord;
	public float sizeFactor = 0f;
	public int maxNumberOfWords = 8;
	

	private int smallestFontSize() {
		return Mathf.FloorToInt(20f + sizeFactor * 75f);
	}
	private float fontSizeFactor() {
		return 40f + sizeFactor * 150f;
	}

	public string[] TheBook = new string[]{ "hello", "world" };
	private static System.Random rand = new System.Random();

	private List<WordDef> wordDefs = new List<WordDef>();
	
	private List<GameObject> gameObjects = new List<GameObject>();

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Fire(float intensity) {
		Launch (nextWord(), intensity);
		sortOutCleanUpOfObjects();
	}

	public void Unfire() {
		//flush();
	}

	void sortOutCleanUpOfObjects() {
		if (gameObjects.Count > maxNumberOfWords) {
			removeDeadObjects();
			triggerDyingObjects();
		}
	}
	
	void removeDeadObjects() {
		var newList = new List<GameObject>();
		foreach(var ob in gameObjects) {
			if (ob != null) {
				newList.Add(ob);
			}
		}
		gameObjects = newList;
	}
	
	void triggerDyingObjects() {
		if (gameObjects.Count > maxNumberOfWords) {
			var numberToEvict = gameObjects.Count - maxNumberOfWords;
			
			for (var i = 0; i < numberToEvict; i++) {
				var wordOb = gameObjects[i].GetComponent<Word>();
				wordOb.StartFadeOut();
			}
		}
	}



	void flush() {
		foreach(var def in wordDefs) {
			Launch (def.Word, def.Size);
		}
		
		wordDefs.Clear();
	}
	
	string nextWord() {
		var index = rand.Next(0, TheBook.Length);

		return TheBook[index];
	}

	void Launch(string nextWord, float size){
		var ob = (GameObject)Instantiate(rawWord);
		var wordOb = ob.GetComponent<Word>();

		var fadeIn = (float)rand.Next(50, 250);
		var fadeOut = (float)rand.Next(1500, 30000);

		wordOb.Init(nextWord, getFontSize(size), fadeIn, fadeOut);

		gameObjects.Add(ob); 
	}

	
	int getFontSize(float size){
		if (size < 0.5){ 
			size = size*size;} 
		else {
			size = 1f - (1f- size)*(1f - size);
		}
		
		return smallestFontSize() + Mathf.RoundToInt(size*fontSizeFactor());
	}

}
