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
	public GameObject WaveTextUIPref;

	public GameObject ScorePanelsGood;
	public GameObject ScorePanelsBad;
	public GameObject ScorePanelsAverage;
	public GameObject TotalScorePanel;

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
		//ShowEndGamePanel ();
		ChangeFaceImage (0);
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

		switch (textType) {
		case WaveQualityText.Average:
			waveText.GetComponent <FloatingImage> ().SetText ("OK");
			waveText.GetComponent <FloatingImage> ().SetColor (Color.yellow);
			break;
		case WaveQualityText.Bad:
			waveText.GetComponent <FloatingImage>().SetText ("Bad");
			waveText.GetComponent <FloatingImage> ().SetColor (Color.red);
			break;
		case WaveQualityText.Great:
			waveText.GetComponent <FloatingImage>().SetText ("Great");
			waveText.GetComponent <FloatingImage> ().SetColor (Color.green);
			break;
		}


	}

	/// <summary>
	/// Shows the end game panel.(score panel)
	/// </summary>
	public void ShowEndGamePanel(){
		StartCoroutine (ShowScoreCoroutine());
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

	/// <summary>
	/// Shows the score coroutine. For now, just activate the difference panels in sequence
	/// Maybe will use tween later
	/// </summary>
	/// <returns>The score coroutine.</returns>
	private IEnumerator ShowScoreCoroutine(){
		//TODO add the scores text from game controller
		EndGamePanel.SetActive (true);
		ScorePanelsGood.SetActive (false);
		ScorePanelsAverage.SetActive (false);
		ScorePanelsBad.SetActive (false);
		TotalScorePanel.SetActive (false);
		yield return new WaitForSeconds (1);
		ScorePanelsGood.SetActive (true);
		yield return new WaitForSeconds (1);
		ScorePanelsAverage.SetActive (true);
		yield return new WaitForSeconds (1);
		ScorePanelsBad.SetActive (true);
		yield return new WaitForSeconds (1);
		TotalScorePanel.SetActive (true);
	}

}
