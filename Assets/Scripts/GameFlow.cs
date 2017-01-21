using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{

    // TODO fix references to use the correct MonoBehaviour type
    // when the controllers are implemented
    public GameObject WorldControllerReference;
    public GameObject CameraControllerReference;
    

    void Awake()
    {
        WorldControllerReference.GetComponent<WorldController>().GenerateLevel(10);
    }

	void Start() {
		
	}


	void Update() {
		
	}
}
