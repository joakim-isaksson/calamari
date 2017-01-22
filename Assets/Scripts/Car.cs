using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float MovementSpeed;
    [HideInInspector]
    public int Direction; // -1 or 1

    public float DespawnDistance;

    private bool activated;

    private GameObject playerRef;

	// Use this for initialization
	void Start () {
		playerRef = GameObject.Find("CameraController");
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var dt = Time.deltaTime;

        transform.position += new Vector3(0, 0, Direction * MovementSpeed * dt);

	    if (activated)
	    {
	        if (Vector3.Distance(transform.position, playerRef.transform.position) > DespawnDistance)
	        {
	            Destroy(gameObject);
	        }
	    }
	}

    /// <summary>
    /// When activated, moves the car behind the player if the car should travel in the same direction
    /// </summary>
    public void Activate(float playerZ)
    {
        if (activated)
        {
            return;
        }
        activated = true;

        if (Direction == 1)
        {
            transform.rotation *= Quaternion.Euler(new Vector3(0f, 180f, 0f));

            var curZ = transform.position.z;
            var differenceZ = curZ - playerZ;
            transform.position = new Vector3(transform.position.x, 
                transform.position.y, 
                playerZ - differenceZ);
        }

		AkSoundEngine.PostEvent ("Car_Play", gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LevelEndTrigger")
        {
            Destroy(gameObject);
        }
    }
}
