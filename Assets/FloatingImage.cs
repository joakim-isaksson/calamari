using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingImage : MonoBehaviour {


	public float existingTime;
	public Vector2 floatingSpeed;

	public Text text;

	// Use this for initialization
	void Start () {
		GameObject.Destroy (gameObject,existingTime);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += floatingSpeed * Time.deltaTime;
	}

	/// <summary>
	/// Sets the text to show
	/// </summary>
	/// <param name="textToShow">Text to show.</param>
	public void SetText(string textToShow){
		text.text = textToShow;
	}



}
