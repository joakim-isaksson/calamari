using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditSceneUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Backs to mene.
	/// </summary>
	public void BackToMenu(){
		//SceneManager.LoadScene ("TestScene_Xiaoxiao");
		ScreenFader.instance.FadeIn (Color.black, 1, delegate {
			SceneManager.LoadScene ("MenuScene");
		});
	}
}
