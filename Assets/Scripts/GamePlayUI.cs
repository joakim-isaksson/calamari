using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePlayUI : MonoBehaviour {

	public static GamePlayUI gamePlayUI;


	public Sprite[] FaceSprites;
	public int[] faceScoreThresholds;

	public Image FaceImage;
	public GameObject EndGamePanel;
	public GameObject WaveTextUIPref;

	public GameObject ScorePanelsGood;
	public GameObject ScorePanelsBad;
	public GameObject ScorePanelsCombo;
	public GameObject ScorePanelsTotal;
	public GameObject RatingBadge;
	public PlayerInput HandInput;

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

		ChangeFaceImage (2);
	}
	
	// Update is called once per frame
	void Update () {
	}



	public void ChangeFaceBasedOnScore(int currentScore){
		if (currentScore < faceScoreThresholds [0]) {
			ChangeFaceImage (0);
		} else if (currentScore < faceScoreThresholds [1]) {
			ChangeFaceImage (1);
		} else if (currentScore < faceScoreThresholds [2]) {
			ChangeFaceImage (2);
		} else if (currentScore < faceScoreThresholds [3]) {
			ChangeFaceImage (3);
		} else {
			ChangeFaceImage (4);
		}
	}


	/// <summary>
	/// Changes the face image. Current use index  for dfferent faces.
	/// </summary>
	/// <param name="index">Index.</param>
	private void ChangeFaceImage(int index){
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
		HandInput.enabled = false;
	}

	/// <summary>
	/// The retry is clicked. Restart the game
	/// </summary>
	public void OnClickRetry(){
		AkSoundEngine.PostEvent ("Menu_Start_Game", gameObject);
		SceneManager.LoadScene ("Main");
	}


	/// <summary>
	/// When the quit button is clicked. Quit the game and back to menu
	/// </summary>
	public void OnClickQuit(){
		AkSoundEngine.PostEvent ("Menu_Click", gameObject);
		ScreenFader.instance.FadeIn (Color.black, 1, delegate {
			SceneManager.LoadScene ("CreditsScene");
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
		ScorePanelsTotal.SetActive (false);
		ScorePanelsCombo.SetActive (false);
		ScorePanelsBad.SetActive (false);
		RatingBadge.SetActive (false);
		yield return new WaitForSeconds (0.4f);
		ScorePanelsGood.SetActive (true);
		ScorePanelsGood.GetComponent <ScoreTween>().TweenToScore (ScoreManager.instance.GoodWaveScores);

		yield return new WaitForSeconds (1.3f);
		ScorePanelsBad.SetActive (true);
		ScorePanelsBad.GetComponent <ScoreTween>().TweenToScore (ScoreManager.instance.BadWaveScores);

		yield return new WaitForSeconds (1.3f);
		ScorePanelsCombo.SetActive (true);
		ScorePanelsCombo.GetComponent <ScoreTween>().TweenToScore (ScoreManager.instance.MaxCombo);

		yield return new WaitForSeconds (1.3f);
		ScorePanelsTotal.SetActive (true);
		ScorePanelsTotal.GetComponent <ScoreTween>().TweenToScore (ScoreManager.TotalScore);

		yield return new WaitForSeconds (1.5f);

		ScoreManager.ScoreRating rating = ScoreManager.instance.GetScoreRating ();

		switch (rating) {
		case ScoreManager.ScoreRating.Bad:
			RatingBadge.GetComponentInChildren <Text>().text = "B";
			break;
		case ScoreManager.ScoreRating.Good:
			RatingBadge.GetComponentInChildren <Text>().text = "A";
			break;
		case ScoreManager.ScoreRating.Perfect:
			RatingBadge.GetComponentInChildren <Text>().text = "A+";
			break;
		case ScoreManager.ScoreRating.Terrible:
			RatingBadge.GetComponentInChildren <Text>().text = "F";
			break;
		}
		RatingBadge.SetActive (true);

	}

}
