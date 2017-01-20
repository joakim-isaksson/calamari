using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float MovementSpeed;
    public float GreetDelay;
    public float GreetingScoreMultiplier;

    public Animator Anim;

    GameObject Player;

    float greetTime;
    bool greeted;

    void Awake()
	{
        Player = GameObject.FindGameObjectWithTag("Player");
    }

	void Start()
	{
        greetTime = GreetDelay;
    }
	
	void Update()
	{

        greetTime -= Time.deltaTime;
        if (!greeted && greetTime < 0)
        {
            greeted = true;
            Anim.SetTrigger("Greet");
        }

        transform.Translate(Vector3.forward * Time.deltaTime * MovementSpeed);

        // Destroy when passed player
        if (transform.position.z < Player.transform.position.z)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Greet the enemy.
    /// </summary>
    /// <returns>
    /// Greeting score.
    /// </returns>
    public int Greet()
    {
        if (!greeted)
        {
            Anim.SetTrigger("Greet");
            int score = (int)Mathf.Round(greetTime * GreetingScoreMultiplier);

            Debug.Log("Score: " + score);

            return score;
        }

        return 0;
    }
}