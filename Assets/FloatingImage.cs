using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingImage : MonoBehaviour {


	public float ExistingTime;
	public float FloatingSpeed;

	public Text Text;

	// Use this for initialization
	void Start () {
		GameObject.Destroy (gameObject, ExistingTime);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += Vector3.up * FloatingSpeed * Time.deltaTime;
	}

	/// <summary>
	/// Sets the text to show
	/// </summary>
	/// <param name="textToShow">Text to show.</param>
	public void SetText(string textToShow){
		Text.text = textToShow;
	}



}
