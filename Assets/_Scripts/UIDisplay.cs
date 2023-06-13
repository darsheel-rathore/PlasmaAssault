using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] TextMeshProUGUI scoreText;

    private void Start()
    {
        healthSlider.value = 1;
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
