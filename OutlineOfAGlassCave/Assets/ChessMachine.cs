using UnityEngine;
using System;
using System.Linq;
using System.Collections;

public class ChessMachine : MonoBehaviour {
	public int timeToMoveMs;
	public ChessPlayer white;
	public ChessPlayer black;
	public WordControl wordControl;

	private DateTime lastTime;
	private bool whitePlayersMove = true;
	// Use this for initialization
	void Start () {
		lastTime = DateTime.Now;
		var text = CalculateText();
		UpdateWordControl(text);
	}
	
	// Update is called once per frame
	void Update () {
		if (IsTime(timeToMoveMs)) {
			CallAChessPlayer();
			var text = CalculateText();
			UpdateWordControl(text);
		}
	}

	string[] CalculateText() {
		var firstWords = GetWords(white.xPosition, white.yPosition);
		var secondWords = GetWords(black.xPosition, black.yPosition);

		return firstWords.Concat(secondWords).ToArray();
	}

	string[] GetWords(int x, int y) {
		return books[x][y].Split(new string[]{" "}, new StringSplitOptions());
	}

	void UpdateWordControl(string[] text) {
		wordControl.TheBook = text;
	}


	void CallAChessPlayer() {
		if (whitePlayersMove) {
			white.MovePieces();
		} else {
			black.MovePieces();
		}
		whitePlayersMove = !whitePlayersMove;
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


	private string[][] books = new string[][] {
		new string[] {
			"switch Blue swatches", 
			"matched to each sq", 
			"each pitch float", 
			"against chronic misunderstanding", 
			"eaks and a sold quarter", 
			"weighed down pitched corrected", 
			"every corner sliced diced", 
			"mounted monuments stored in data"
		}, 
		new string[] {
			"in detail of", 
			"monotonic decreasing everywhere so", 
			"much for thinking", 
			"tonight is the best", 
			"things to women because", 
			"of this email", 
			"delete delete it immediately", 
			"point your fingers crossed"
		}, 
		new string[] {
			"for details again", 
			"later but ok", 
			"not available application", 
			"appreciate appointment with you", 
			"beauty of the other", 
			"private property", 
			"beauty of the other", 
			"private property"
		}, 
		new string[] {
			"for details again", 
			"later but ok", 
			"not available application", 
			"appreciate appointment with you", 
			"beauty of the other", 
			"private property", 
			"beauty of the other", 
			"private property"
		}, 
		new string[] {
			"in detail of", 
			"monotonic decreasing everywhere so", 
			"much for thinking", 
			"tonight is the best", 
			"things to women because", 
			"of this email", 
			"delete delete it immediately", 
			"point your fingers crossed"
		}, 
		new string[] {
			"switch Blue swatches", 
			"matched to each sq", 
			"each pitch float", 
			"against chronic misunderstanding", 
			"eaks and a sold quarter", 
			"weighed down pitched corrected", 
			"every corner sliced diced", 
			"mounted monuments stored in data"
		}, 
		new string[] {
			"switch Blue swatches", 
			"matched to each sq", 
			"each pitch float", 
			"against chronic misunderstanding", 
			"eaks and a sold quarter", 
			"weighed down pitched corrected", 
			"every corner sliced diced", 
			"mounted monuments stored in data"
		}, 
		new string[] {
			"in detail of", 
			"monotonic decreasing everywhere so", 
			"much for thinking", 
			"tonight is the best", 
			"things to women because", 
			"of this email", 
			"delete delete it immediately", 
			"point your fingers crossed"
		} 


	};
}
