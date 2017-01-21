using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DigitalRuby.Tween;
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
				ScoreText.text = ((int)t3.CurrentValue).ToString();
			}, (t3) =>
			{
				// completion - nothing more to do!
			});
	}

}
