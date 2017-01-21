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

		AkSoundEngine.PostEvent ("Play_MenuAndCredits_Music", gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClickStartGame(){
		//SceneManager.LoadScene ("TestScene_Xiaoxiao");
		ScreenFader.instance.FadeIn (Color.black, 1, delegate {
			SceneManager.LoadScene (GameSceneName);
		});
	}	

	public void OnClickCredits(){
		//SceneManager.LoadScene ("TestScene_Xiaoxiao");
		ScreenFader.instance.FadeIn (Color.black, 1, delegate {
			SceneManager.LoadScene (CreditSceneName);
		});
	}	
}
