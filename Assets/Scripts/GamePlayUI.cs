using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePlayUI : MonoBehaviour {

	public static GamePlayUI gamePlayUI;


	public Sprite[] FaceSprites;
	public Image FaceImage;
	public GameObject EndGamePanel;
	//public Sprite goodWaveText;
	//public Sprite averageWaveText;
	public GameObject WaveTextUIPref;



	private Camera mainCamera;


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
		mainCamera = Camera.main;

		//test
		//ShowClickText (mainCamera.transform.position + Vector3.forward*10, WaveQualityText.Average);

		//ShowClickText (mainCamera.transform.position + Vector3.right*10 + Vector3.forward*10, WaveQualityText.Average);



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
		if (index >= 0 && index < FaceSprites.Length) {
			FaceImage.sprite = FaceSprites [index];
		}
	}

	/// <summary>
	/// Shows the click text for good wave, bad wave and neutral wave
	/// </summary>
	/// <param name="worldPosition">World position.</param>
	public void ShowClickText(Vector3 worldPosition, WaveQualityText textType){

		//Vector3 screenPosition = mainCamera.WorldToScreenPoint (worldPosition);
		GameObject waveText = GameObject.Instantiate (WaveTextUIPref,transform);


		Vector3 viewPointPos = mainCamera.WorldToViewportPoint (worldPosition);
		//first you need the RectTransform component of your canvas
		RectTransform canvasRect=GetComponent<RectTransform>();

		//then you calculate the position of the UI element
		Vector2 worldObjectScreenPosition=new Vector2(
			((viewPointPos.x*canvasRect.sizeDelta.x)-(canvasRect.sizeDelta.x*0.5f)),
			((viewPointPos.y*canvasRect.sizeDelta.y)-(canvasRect.sizeDelta.y*0.5f)));


		RectTransform trans = (RectTransform)waveText.transform;
		//now you can set the position of the ui element
		trans.anchoredPosition=worldObjectScreenPosition;

	}

	/// <summary>
	/// Shows the end game panel.(score panel)
	/// </summary>
	public void ShowEndGamePanel(){
		EndGamePanel.SetActive (true);
	}

	/// <summary>
	/// The retry is clicked. Restart the game
	/// </summary>
	public void OnClickRetry(){
	}


	/// <summary>
	/// When the quit button is clicked. Quit the game and back to menu
	/// </summary>
	public void OnClickQuit(){
		//SceneManager.LoadScene ("TestScene_Xiaoxiao");
		ScreenFader.instance.FadeIn (Color.black, 1, delegate {
			SceneManager.LoadScene ("MenuScene");
		});
		
	}

}
