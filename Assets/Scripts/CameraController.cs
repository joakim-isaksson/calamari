using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float MovementSpeed;

    private float pauseTimer;
    private float maxPauseTimer;

	void Update()
	{
	    float dt = Time.deltaTime;

	    pauseTimer += dt;
        
        // If the timer hasn't reached the maximum pause time, do not move
	    if (pauseTimer < maxPauseTimer)
	    {
	        return;
	    }

        transform.position += new Vector3(0, 0, 1) * MovementSpeed * dt;
	}

    /// <summary>
    /// Pauses the player for the given number of seconds.
    /// </summary>
    /// <param name="seconds"></param>
    public void PauseFor(float seconds)
    {
        pauseTimer = 0;
        maxPauseTimer = seconds;
    }
}
