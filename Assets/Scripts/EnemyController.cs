using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Walking and running speeds
    public float WalkingSpeed = -2.0f;
    public float RunningSpeed = -5.0f;

    // Only friendly enemies are going to wave
    public bool Friendly = true;

    // Wave delay
    public float WaveMinDelay = 0.0f;
    public float WaveMaxDelay = 5.0f;

    // Maximum time for the player to respond to the wave
    public float MaxResponseTime = 2.0f;

    // Scoring
    public int ScoreNotGreetedFriendly = -1000;
    public int ScoreNotGreetedHostile = 1000;
    public int ScoreGreetHostile = -10000;
    public int ScoreGreetFriendlyEarly = -2000;
    public int ScoreGreetFriendlyOnTime = 10000;
    public int ScoreGreetFriendlyLate = -500;

    public Animator Anim;

	public string NegativeWwiseSfx;
	public string PositiveWwiseSfx;
	public string VeryNegativeWwiseSfx;

    GameObject Player;
    ScoreManager Score;

    bool wavingTriggered;
    float wavingStartedTime = -1.0f;
    bool greeted;
    bool running;

    void Awake()
	{
        Player = GameObject.FindGameObjectWithTag("Player");

	    Friendly = Random.value > 0.3;
	}

	void Start(){
		Score = ScoreManager.instance;
	}

    void Update()
	{
        // Move character
       if (running) transform.Translate(Vector3.forward * Time.deltaTime * RunningSpeed);
       else transform.Translate(Vector3.forward * Time.deltaTime * WalkingSpeed);

        // Destroy character when passing the player
        if (transform.position.z < Player.transform.position.z)
        {
            // Handle scoring when not greeted
            if (!greeted)
            {
                if (Friendly)
                {
                    Score.Add(ScoreNotGreetedFriendly);

                }
                else
                {
                    Score.Add(ScoreNotGreetedHostile);
					//AkSoundEngine.PostEvent (NegativeWwiseSfx, this.gameObject);
                }
            }

            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (Friendly && !wavingTriggered && collision.tag == "Player")
        {
            wavingTriggered = true;
            StartCoroutine(Wave());
        }
    }

    IEnumerator Wave()
    {
        yield return new WaitForSeconds(Random.Range(WaveMinDelay, WaveMaxDelay));
        if (!greeted)
        {
            wavingStartedTime = Time.realtimeSinceStartup;
            Anim.SetTrigger("Waving");
            Debug.Log("Waving");
			StartCoroutine (AfterGreeting());
        }
    }

    /// <summary>
    /// Greet the enemy.
    /// Get or lose scores.
    /// </summary>
    /// <returns>
    /// </returns>
    public void Greet()
    {
        greeted = true;

        // Greeting an unfriendly character (really bad)
        if (!Friendly)
        {
            Score.Add(ScoreGreetHostile);
            Anim.SetTrigger("Angry");
			AkSoundEngine.PostEvent (NegativeWwiseSfx, this.gameObject);
			Invoke ("GreetNegativeSound", 0.9f);
			//GamePlayUI.gamePlayUI.ShowClickText (transform.position + Vector3.up * 2.5f, GamePlayUI.WaveQualityText.Bad);
        }

        // Greeting a friendly character too early (bad)
        else if (wavingStartedTime < 0)
        {
            Score.Add(ScoreGreetHostile);
            Anim.SetTrigger("Angry");
			AkSoundEngine.PostEvent ("Narrator_Negative", this.gameObject);
			Invoke ("GreetNegativeSound", 0.9f);
			//GamePlayUI.gamePlayUI.ShowClickText (transform.position + Vector3.up * 2.5f, GamePlayUI.WaveQualityText.Average);
        }

        // Responding to a friendly characters greeting in time (good)
		else if (Time.realtimeSinceStartup - wavingStartedTime <= MaxResponseTime)
        {
            Score.Add(ScoreGreetFriendlyOnTime);
            Anim.SetTrigger("Happy");
			AkSoundEngine.PostEvent ("Narrator_Positive", this.gameObject);
			Invoke ("GreetPositiveSound", 0.9f);
			//GamePlayUI.gamePlayUI.ShowClickText (transform.position + Vector3.up * 2.5f, GamePlayUI.WaveQualityText.Great);
        }

        // Greeting a friendly character too late (bad)
        else
        {
            Score.Add(ScoreGreetFriendlyLate);
            Anim.SetTrigger("Angry");
			AkSoundEngine.PostEvent ("Narrator_Neutral", this.gameObject);
			Invoke ("GreetNegativeSound", 0.9f);
			//GamePlayUI.gamePlayUI.ShowClickText (transform.position + Vector3.up * 2.5f, GamePlayUI.WaveQualityText.Average);
        }
    }

	private void GreetNegativeSound(){
		AkSoundEngine.PostEvent (NegativeWwiseSfx, this.gameObject);
	}
	private void GreetPositiveSound(){
		AkSoundEngine.PostEvent (PositiveWwiseSfx, this.gameObject);
	}
	private void GreetVeryNegativeSound(){
		AkSoundEngine.PostEvent (VeryNegativeWwiseSfx, this.gameObject);
		Debug.Log ("Very negative played on" + gameObject.name);
	}

	private IEnumerator AfterGreeting(){
		yield return new WaitForSeconds (MaxResponseTime + 1);

		if (!greeted) {
			AkSoundEngine.PostEvent ("Narrator_Neutral", this.gameObject);
			Invoke ("GreetVeryNegativeSound", 0.9f);
		}

	}

}