using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] private int score;

    public int GetScore() => score;

    public void ResetScore() => score = 0;

    public void ModifyScore(int value) 
    { 
        score += value;
        // Clamp the value so it not reaches below 0
        score = Mathf.Clamp(score, 0, int.MaxValue);
    }


}