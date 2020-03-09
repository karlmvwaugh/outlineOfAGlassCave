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
		white.maxY = 7;
		black.maxY = 7;
		
		white.maxX = 3;
		white.maxX = 3;

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
			"history in ink written known catalogued documentary solution for evidence mounting procedures pinpoint exact moments of CCTV cameras dates times precision timelines documenting typed up locations listed in print", 
			"all the possibilities probabilities paths and passages taken drifted through lived lives loves futures hopes and opportunities lost or made real realities impossible chances forever dreams dreamt daydreaming do", 
			"what we had bloomed blossoming flowers time we held eachother holding moments glowing beauty fields of loving delicate desire something to remember hold a memory loved known as best as could be ",
			"my path and place reasons reasoned excuses instinct belief in better lines drawn cases made mine acknowledged for all the patterns plans telling myself gut explanation rides reasoning of all",
			"limited only time list small wasted chances decay and loss unfulfilled unrealised incomplete abandoned byproduct spent it all working sleeping daydreaming never rotten eroded or composted",
			"he she lived their life with grace and planning choreographed pinpointed survey mapped accidents absorbed into the whole as architecture",
			"each mistake echoing rivers of spiral pond ripples bouncing off of chance meetings random encounters accidents reverberating endless butterfly explosions fractal reactions",
			"afterwards it makes a quiet kind of sense which we dare not question in a way it always would but who could say if they weren't there the logic is external and unknowable "
		}, 
		new string[] {
			"fact happened here there then interaction condusive arrangement codependency amounting to coitus time and place reasons for this that public record ",
			"labelling questions activity uncertainty folding particulars specifics over drifting particles patterns of behaviour unknown beyond unknowable boundaries forever in vague",
			"to love to embrace hold know kiss caress care becoming forever entangled sweetened softened butter purring cooing softly gently lovingly restored",
			"this moment this feeling this point of time heat pressure whirlwind tornado lights bliss now beating hearts close feeling love body sweat heat",
			"entwined flesh panting throbbing thrusting vicious viscose bodies fighting beasts animalistic needy collapse of desire",
			"they love adore care for eachother about beyond around always caring caressing flowering beauty for about around soft careful loving",
			"to arrive blinding luck in arms folding time and space and everything you know this becomes all we could be all we envelope this beating heat of forever is so improbable so unlikely so beautiful",
			"what is to fall in love ? to care what is why are feelings you search for meaning strive for something somewhere someone to outline the world for you for you"
		}, 
		new string[] {
			"the rule of the law the iron fist the gentle caring state imposed instructions serving atoning correctional facility industry complex industrial systematic institutionalised",
			"echoes repeating phasing reasons reverberation silencing alternatives saturated in what happens we see only reasons contaminated by cause and effect",
			"dew drops exploding effervescent orgasms relationships timelines impossibilities tracing maps of dynamic forevers folded around always",
			"what have I done what happened how did I let this become unfurl I failed this became something untenable unrelenting unforgivable",
			"malleable failure of a physical manifestation of my own spittle grip on muscle aching repercussions spasm tone grabbing thrust of denial fluid",
			"a burning mess of self-destructive automatic failure spiralling let themselves down car crash pile up disowned abandoned byproduct of waste",
			"what unfolds dissolves explodes in butterflies cries out loud bouncing from place to point what will be is always timeless Eris forever onwards as the universe",
			"whatever came and went has been part of the tapestry the weave the story was told how could it be anything but what was a tale as old as time as part of everything" 
		}, 
		new string[] {
			"she has a name a date of birth height width features family history places medical education etc. these things are known or knowable somehow",
			"everything she was exploded around her explaining or hiding from the silhouette of the wake of her movements her path an image unknown",
			"inside outside eternally amazing glowing beauty shining diamonds gems gilded frozen explosion sunshine infinite forever beyond surrounding glowing sunrise",
			"my pressures times rhythms rhythmic pulsations loves desires feelings what makes me creates dictates sensation of self of identity satisfaction and loyalty to oneself",
			"bleeding angry emotional repeating fantasies monthly irrational irrationality inferior daughter wife mother who else births feeds loves outside firey burning wanting",
			"she glows glides smooth sublime adorable strong stable sweet caring careful delicate brutal she floats free immaculate heart funny clever determined amazing",
			"another path unwound a trail trodden what leads her roads mistakes choices how did she decisions made bouncing histories intwined genetics desires patterns",
			"strong in the current rhythm pull of everyday life certain and loving kind and determined yet dragged along in eddies preordained tide of life's waters"
		},


	};
}
