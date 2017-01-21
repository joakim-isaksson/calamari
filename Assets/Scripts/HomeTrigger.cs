using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeTrigger : MonoBehaviour
{

	// Use this for initialization
	void Start()
    {
		
	}
	
	// Update is called once per frame
	void Update()
    {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LevelEndTrigger")
        {
            GamePlayUI.gamePlayUI.ShowEndGamePanel();
            transform.parent.GetComponent<CameraController>().PauseFor(10000);
        }
    }
}
