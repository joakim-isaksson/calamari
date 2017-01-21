using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DigitalRuby.Tween;
using System.Globalization;


public class ScoreTween : MonoBehaviour {


	public Text ScoreText;
	public float tweenTime;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//tween the text to the score
	public void TweenToScore(int score){

		// completion
		gameObject.Tween("MoveCircle", 0, score, tweenTime, TweenScaleFunctions.CubicEaseOut, (t3) =>
			{
				// progress
				ScoreText.text = string.Format("{0:n0}", (int)t3.CurrentValue);
			}, (t3) =>
			{
				// completion - nothing more to do!
			});
	}

}
