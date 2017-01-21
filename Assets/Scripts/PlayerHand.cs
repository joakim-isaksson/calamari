using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour {

	public string EnemyTag;
	public LayerMask RaycastLayer;

	public CameraController CamController;




	private Camera mainCamera;
	private Animator handAnimator;
	//maybe not used
	private float stamina = 1;



	public enum HandState{
		Resting,
		Waving
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

		if (handState == HandState.Waving || stamina <= 0)
			return;


		Ray mouseRay = mainCamera.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		Physics.Raycast(mouseRay,out hit,100,RaycastLayer);


		handState = HandState.Waving;
		StartCoroutine (PlayHandAnimation());
		Debug.Log ("Wave");

		if (hit.collider != null) {
			Debug.Log ("Player hit something");
			if (hit.collider.gameObject.CompareTag (EnemyTag)) {
				//TODO call the enemy stuff;
				EnemyController enemyController = hit.collider.GetComponent <EnemyController> ();
				enemyController.Greet ();
			}
		}

	}


	/// <summary>
	/// Play the wave animation
	/// </summary>
	IEnumerator PlayHandAnimation(){
		//play animation
		//...
		handAnimator.SetTrigger ("Wave");
		CamController.PauseFor (0.7f);
		yield return new WaitForSeconds (1.0f);

		handState = HandState.Resting;
	}




}
