using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIController : MonoBehaviour {
	public string GameSceneName;
	public string CreditSceneName;
	// Use this for initialization
	void Start () {
		SceneManager.sceneLoaded += delegate {
			ScreenFader.instance.FadeOut (Color.black,1, delegate {
			});
		};


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClickStartGame(){
		AkSoundEngine.PostEvent ("Menu_Start_Game", gameObject);
		ScreenFader.instance.FadeIn (Color.black, 1, delegate {
			SceneManager.LoadScene (GameSceneName);
		});
	}	

	public void OnClickCredits(){
		AkSoundEngine.PostEvent ("Menu_Click", gameObject);
		ScreenFader.instance.FadeIn (Color.black, 1, delegate {
			SceneManager.LoadScene (CreditSceneName);
		});
	}	


	public void PointerEnterSound(){
		AkSoundEngine.PostEvent ("Menu_Button_Hit", gameObject);
		Debug.Log ("button Enter Sound");
	}

	public void PointerExitSound(){
		AkSoundEngine.PostEvent ("Menu_Button_Release", gameObject);
		Debug.Log ("button Exit Sound");
	}
}
