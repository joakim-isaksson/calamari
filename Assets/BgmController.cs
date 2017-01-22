using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BgmController : MonoBehaviour {

	private string currentSceneName = "";

	public static BgmController Instance;

	void Awake(){
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		SceneManager.sceneLoaded += CheckMusicToPlay;
		//AkSoundEngine.PostEvent ("Play_MenuAndCredits_Music", gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void CheckMusicToPlay(Scene sc, LoadSceneMode mode){
		
		if (sc.name.Equals ("Main")) {
			PlayGamePlayMusic ();
		} else if (currentSceneName.Equals ("Main") && (sc.name.Equals ("MenuScene") || sc.name.Equals ("CreditsScene"))) {
			PlayMenuAndCreditMusic ();
		}

		currentSceneName = sc.name;
	}

	private void PlayMenuAndCreditMusic(){
		AkSoundEngine.PostEvent ("Play_MenuAndCredits_Music", gameObject);
	}

	private void PlayGamePlayMusic(){
		AkSoundEngine.PostEvent ("Play_StartLevel_Music", gameObject);
		Invoke ("PlayIngameMusic", 2.5f);
	}

	private void PlayIngameMusic(){
		AkSoundEngine.PostEvent ("Play_InGame_Music", gameObject);
	}

}
