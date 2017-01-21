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
		
	}
	
	void Update()
	{
		
	}

    public void Add(int amount)
    {
        if (amount > 0) Combo++;
        else Combo = 0;

        TotalScore += amount + (int)Mathf.Round(Combo * ComboMultiplier * amount);
    }
}