using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float MovementSpeed;

    private float pauseTimer;
    private float maxPauseTimer;

    public float CameraBobbingSpeed;
    public float CameraBobbingScaleX;
    public float CameraBobbingScaleY;

    private float cameraBobTimer;
    private Vector3 cameraBobAnchor;

    void Start()
    {
        cameraBobAnchor = transform.position;
    }

	void Update()
	{
	    float dt = Time.deltaTime;

	    pauseTimer += dt;
        
        // If the timer hasn't reached the maximum pause time, do not move
	    if (pauseTimer < maxPauseTimer)
	    {
	        return;
	    }

        // Camera bobbing
        cameraBobTimer += CameraBobbingSpeed * dt;

        var cameraBobX = Mathf.Cos(cameraBobTimer);
	    var cameraBobY = Mathf.Abs(Mathf.Sin(cameraBobTimer));

        transform.position = new Vector3(cameraBobAnchor.x + cameraBobX * CameraBobbingScaleX,
            cameraBobAnchor.y + cameraBobY * CameraBobbingScaleY, 
            transform.position.z);

        // Camera movement
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
