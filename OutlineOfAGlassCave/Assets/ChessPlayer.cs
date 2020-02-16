using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ChessPlayer : MonoBehaviour {

	public int xPosition; //0-7
	public int yPosition;

	private static System.Random rand = new System.Random();

	private List<Moves> directions = new List<Moves>() {
		new Moves(1, 1),
		new Moves(-1, 1),
		new Moves(-1, -1),
		new Moves(1, -1)
	};

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void MovePieces() {
		var allPossibleMoves = GetAllPossibleMoves();
		ChooseAMove(allPossibleMoves);
	}

	void ChooseAMove(List<Moves> allPossibleMoves) {
		var index = rand.Next (0, allPossibleMoves.Count);
		var move = allPossibleMoves[index];
		xPosition = move.x;
		yPosition = move.y;
	}

	List<Moves> GetAllPossibleMoves() {
		var currentMove = CurrentMove();
		return directions.SelectMany(direction => GetAllInOneDirection(currentMove, direction)).ToList();
		//CurrentMove() 
	}

	List<Moves> GetAllInOneDirection(Moves currentMove, Moves direction) {
		var newList = new List<Moves>();
		var currentContender = AddMoves(currentMove, direction); 
		while (IsOnBoard(currentContender)) {
			newList.Add(currentContender);

			currentContender = AddMoves(currentContender, direction);
		}

		return newList;
	}

	Moves AddMoves(Moves one, Moves two) {
		return new Moves(one.x + two.x, one.y + two.y);
	}

	Boolean IsOnBoard(Moves move) {
		return (move.x >= 0 && move.x <= 7 && move.y >= 0 && move.y <= 7);
	}

	Moves CurrentMove() {
		return new Moves(xPosition, yPosition);
	}


	public class Moves {
		public int x;
		public int y;
		public Moves(int _x, int _y) {
			x = _x;
			y = _y;
		}

	}
}


