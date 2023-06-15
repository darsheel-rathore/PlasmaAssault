using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] private int score;

    public static ScoreKeeper instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        // Set to this instance
        instance = this;
        // Dont destroy on new scene
        DontDestroyOnLoad(gameObject);
    }

    public int GetScore() => score;

    public void ResetScore() => score = 0;

    public void ModifyScore(int value) 
    {
        Debug.Log("Score Keepre Modify Score called");
        score += value;
        // Clamp the value so it not reaches below 0
        score = Mathf.Clamp(score, 0, int.MaxValue);
    }


}
