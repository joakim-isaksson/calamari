using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public float ComboMultiplier = 1.0f;

    [HideInInspector]
    public static ScoreManager instance = null;

    [HideInInspector]
    public static int Combo;

    [HideInInspector]
    public static int TotalScore;

	[HideInInspector]
	public int MaxCombo;

	[HideInInspector]
	public int BadWaveScores;

	[HideInInspector]
	public int GoodWaveScores;


	public int PerfectRatingScore;
	public int GoodRatingScore;
	public int BadRatingScore;
	public int TerribleRatingScore;



	public enum ScoreRating
	{
		Perfect,
		Good,
		Bad,
		Terrible
	}


    public static void DestroySingleton()
    {
        Destroy(instance.gameObject);
        instance = null;
    }

    void Awake()
    {
        // Singleton
        if (instance == null) instance = this;
        else if (!instance.Equals(this)) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start()
	{
		Reset ();
	}
	
	void Update()
	{
		
	}

	public void Reset(){
		TotalScore = 0;
		Combo = 0;
		MaxCombo = 0;
		BadWaveScores = 0;
		GoodWaveScores = 0;
	}

    public void Add(int amount)
    {
        Debug.Log("add" + amount);

        if (amount > 0) Combo++;
        else Combo = 1;

		if (Combo > MaxCombo && amount > 0) {
			MaxCombo = Combo;
		}



		int actualAmout = (int)Mathf.Round(Combo * ComboMultiplier * amount + amount);
		if (amount < 0) {
			actualAmout = amount;
		}
		TotalScore += actualAmout;
		if (actualAmout > 0) {
			GoodWaveScores += actualAmout;
		} else {
			BadWaveScores += actualAmout;
		}

		GamePlayUI.gamePlayUI.ChangeFaceBasedOnScore (TotalScore);
    }


	/// <summary>
	/// Gets the score rating.
	/// </summary>
	/// <returns>The score rating.</returns>
	public ScoreRating GetScoreRating(){
		if (TotalScore >= PerfectRatingScore) {
			return ScoreRating.Perfect;
		} else if (TotalScore >= GoodRatingScore) {
			return ScoreRating.Good;
		} else if (TotalScore >= BadRatingScore) {
			return ScoreRating.Bad;
		} else {
			return ScoreRating.Terrible;
		}
	}
}