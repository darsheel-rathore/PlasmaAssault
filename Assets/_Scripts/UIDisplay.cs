using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] TextMeshProUGUI scoreText;

    private ScoreKeeper scoreKeeper;
    private Health playerHealth;

    private void Awake()
    {
        scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
        playerHealth = FindAnyObjectByType<PlayerMovement>().GetComponent<Health>();
    }

    private void Start()
    {
        // Init values
        healthSlider.value = playerHealth.GetHealth();
        scoreText.text = scoreKeeper.GetScore().ToString("0000000");
    }

    public void UpdateScore(int value)
    {
        scoreText.text = value.ToString("0000000");
    }

    public void UpdateHealth(int value)
    {
        float newHealth = (float)value / 100;
        healthSlider.value = newHealth;
    }
}
