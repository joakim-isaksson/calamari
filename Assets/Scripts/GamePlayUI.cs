using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour {

	public static GamePlayUI gamePlayUI;


	public Sprite[] faceSprites;
	public Image faceImage;
	public Sprite goodWaveText;
	public Sprite averageWaveText;
	public GameObject waveTextUIPref;



	public enum WaveQualityText{
		Great,
		Average,
		Bad
	}





	void Awake(){
		gamePlayUI = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Changes the face image. Current use index  for dfferent faces.
	/// </summary>
	/// <param name="index">Index.</param>
	public void ChangeFaceImage(int index){
		//change the image based on the index
		if (index >= 0 && index < faceSprites.Length) {
			faceImage.sprite = faceSprites [index];
		}
	}

	/// <summary>
	/// Shows the click text for good wave, bad wave and neutral wave
	/// </summary>
	/// <param name="worldPosition">World position.</param>
	public void ShowClickText(Vector3 worldPosition, WaveQualityText textType){



	}




}
