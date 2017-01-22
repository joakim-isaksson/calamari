using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{

    // TODO fix references to use the correct MonoBehaviour type
    // when the controllers are implemented
    public GameObject WorldControllerReference;
    public GameObject CameraControllerReference;

    public int LevelLength;
    public int EnemyCount;
    

    void Awake()
    {
        WorldControllerReference.GetComponent<WorldController>().GenerateLevel(LevelLength, EnemyCount);
    }

	void Start() {
		CameraControllerReference.GetComponent <CameraController>().PauseFor (2);
		AkSoundEngine.PostEvent ("Narrator_Ready", gameObject);
		Invoke ("PlayGoSound", 2);
		//AkSoundEngine.PostEvent ("Play_StartLevel_Music", gameObject);
		//Invoke ("PlayIngameMusic", 2.5f);
	}


	void Update() {
		
	}


	private void PlayGoSound(){
		AkSoundEngine.PostEvent ("Narrator_Go", gameObject);
	}

	private void PlayIngameMusic(){
		AkSoundEngine.PostEvent ("Play_InGame_Music", gameObject);
	}
}
