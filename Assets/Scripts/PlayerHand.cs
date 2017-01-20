using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour {


	public string enemyTag;
	public LayerMask raycastLayer;


	Camera mainCamera;

	//maybe not used
	float stamina = 1;



	public enum HandState{
		Resting,
		Waving
	}


	HandState handState;

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

		if (handState == HandState.Resting || stamina <= 0)
			return;

		Vector3 point = mainCamera.ScreenToWorldPoint (Input.mousePosition);
		Collider2D clickOnCol = Physics2D.OverlapPoint (point,raycastLayer);

		StartCoroutine (PlayHandAnimation());

		if (clickOnCol != null) {
			if (clickOnCol.gameObject.CompareTag (enemyTag)) {
				//TODO call the enemy stuff;
			}
		}

	}


	/// <summary>
	/// Play the wave animation
	/// </summary>
	IEnumerator PlayHandAnimation(){
		handState = HandState.Waving;

		//play animation
		//...

		yield return new WaitForSeconds (1.0f);

		handState = HandState.Resting;
	}




}
