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
		
	}


	void Update() {
		
	}
}
