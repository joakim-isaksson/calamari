using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour {


	public LayerMask RaycastLayer;

	public CameraController CamController;


	private Camera mainCamera;
	private Animator handAnimator;
	//maybe not used
	private float stamina = 1;



	public enum HandState{
		Resting,
		Waving,
		WrongWaving
	}


	private HandState handState;

	void Awake(){
		handAnimator = GetComponent <Animator> ();
	}


	// Use this for initialization
	void Start () {
		mainCamera = Camera.main;
		handState = HandState.Resting;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	/// <summary>
	/// Player just clicks the mouse. It is gonna check if a enemy is clicked, play the animation and so on
	/// </summary>
	public void PlayerClick(){

		//Debug.Log ("Player clicked");

		if (handState != HandState.Resting || stamina <= 0)
			return;


		Ray mouseRay = mainCamera.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		Physics.Raycast(mouseRay,out hit,35,RaycastLayer);



		if (hit.collider != null) {
			Debug.Log ("Player hit something");
			EnemyController enemyController = hit.collider.GetComponent <EnemyController> ();
			if (enemyController != null) {
				//hit the enemy, wave back
				handState = HandState.Waving;
				StartCoroutine (PlayHandWaveAnimation(enemyController));

				return;
			}
		}

		//not hit the enemy
		//other animatinon
		handState = HandState.WrongWaving;
		StartCoroutine (PlayHandNoWaveAnimation());
	}


	/// <summary>
	/// Play the wave animation
	/// </summary>
	IEnumerator PlayHandWaveAnimation(EnemyController enemyController){
		//play animation
		//...
		AkSoundEngine.PostEvent ("Wave_Hit", gameObject);
		handAnimator.SetTrigger ("Wave");
		CamController.PauseFor (1.0f);
		yield return new WaitForSeconds (0.4f);
		enemyController.Greet ();
		yield return new WaitForSeconds (0.6f);

		handState = HandState.Resting;
	}

	/// <summary>
	/// Play the wrong wave wave animation
	/// </summary>
	IEnumerator PlayHandNoWaveAnimation(){
		//play animation
		//...
		AkSoundEngine.PostEvent ("Wave_Miss", gameObject);
		handAnimator.SetTrigger ("WrongWave");
		//CamController.PauseFor (0.7f);
		yield return new WaitForSeconds (.5f);

		handState = HandState.Resting;
	}


}
