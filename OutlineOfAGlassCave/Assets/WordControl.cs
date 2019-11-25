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

	private string[] TheBook;
	private int maxNumberOfWords = 4;
	private static System.Random rand = new System.Random();

	private List<WordDef> wordDefs = new List<WordDef>();
	
	private List<GameObject> gameObjects = new List<GameObject>();

	// Use this for initialization
	void Start () {
		TheBook = poem.Split(new string[]{" "}, new StringSplitOptions()); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Fire(float intensity) {
		//wordDefs.Add(new WordDef(nextWord(), sizeConversion(intensity)));


		Launch (nextWord(), sizeConversion(intensity));
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

	float sizeConversion(float intensity) {
		return intensity;
	}

	void Launch(string nextWord, float size){
		var ob = (GameObject)Instantiate(rawWord);
		var wordOb = ob.GetComponent<Word>();

		var fadeIn = (float)rand.Next(50, 250);
		var fadeOut = (float)rand.Next(1500, 30000);

		wordOb.Init(nextWord, size, fadeIn, fadeOut);

		gameObjects.Add(ob); 
	}

	private string poem = "A switch Blue swatches matched to each sq each pitch float against chronic misunderstanding leaks and a sold quarter "
		+ "weighed down pitched corrected every corner sliced diced mounted monuments stored in data in detail of monotonic decreasing everywhere so "
			+ "much for thinking tonight is the best things to women because of this email and delete delete it immediately point your fingers crossed "
			+ "for details again later but ok and that is not available application appreciate appointment with you the beauty of the other I since private "
			+ "message privileged confidential proprietary or legally protected information hence the corresponding services without the historical society "
			+ "baggage allowance is made up blinking lights of the other each song sounds like a lot of money an interview infant infallible inedible edited "
			+ "together to ISO certified company food grumby lush ambient temperature especially the octopus a bit wet watermark when liquid crystal display ";
}
