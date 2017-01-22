using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIController : MonoBehaviour {
	public string GameSceneName;
	public string CreditSceneName;
	public GameObject SplashScreen;
	public float SplashFadeSecond = 1;
	public float SplashShowSecond = 1;
	private static bool  started = false;

	// Use this for initialization
	void Start () {
		SceneManager.sceneLoaded += delegate {
			ScreenFader.instance.FadeOut (Color.black,1, delegate {
			});
		};

		if (started == false) {
			started = true;
			SplashScreen.SetActive (true);
			StartCoroutine (PlaySplashScreen());
		}

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


	private IEnumerator PlaySplashScreen(){
		
		Image image = SplashScreen.GetComponent <Image> ();
		image.color = Color.black;
		float progress = 0;
		while (image.color.r < 0.99f)
		{
			progress += (Time.deltaTime / SplashFadeSecond);
			Debug.Log (progress);
			float t = Mathf.Lerp (0.0f, 1.0f, progress);
			image.color = new Color(t, t, t, 1);
			yield return null;

		}

		yield return new WaitForSeconds (SplashShowSecond);

		progress = 0;
		while (image.color.r >0.01f)
		{
			progress += (Time.deltaTime / SplashFadeSecond);
			Debug.Log (progress);
			float t = Mathf.Lerp (1.0f, 0.0f, progress);
			image.color = new Color(t, t, t, 1);
			yield return null;

		}

		image.color = new Color(1, 1, 1, 1.0f);
		SplashScreen.SetActive(false);
		ScreenFader.instance.FadeOut (Color.black, SplashFadeSecond, delegate {
			//AkSoundEngine.PostEvent ("Play_MenuAndCredits_Music", BgmController.Instance.gameObject);
		});
	}
}
