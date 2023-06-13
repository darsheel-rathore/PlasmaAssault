using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] int score = 50;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private bool applyCameraShake;
    [SerializeField] private bool isPlayer;

    private CameraShake cameraShake;
    private AudioPlayer audioPlayer;
    private ScoreKeeper scoreKeeper;
    private GameObject temporaryObjects;
    private UIDisplay uiDisplay;

    private void Awake()
    {
        // Ref
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindAnyObjectByType<AudioPlayer>();
        scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
        temporaryObjects = GameObject.Find("TempGameObject");
        uiDisplay = FindAnyObjectByType<UIDisplay>();
    }

    public int GetHealth() => health;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            // Do some damage
            TakeDamage(damageDealer.GetDamage());

            // Show some effect on damage
            PlayHitEffect();
            
            // Play Destroy audio clip
            if (audioPlayer != null)
                audioPlayer.PlayDamageClip();

            // Shake the camera
            ShakeCamera();

            // Destroy the enemy/projectile
            damageDealer.Hit();
        }
    }

    private void TakeDamage(int damage)
    {
        // Reduce health
        health -= damage;

        // Update the UI
        if (isPlayer)
        {
            uiDisplay.UpdateHealth(health);
        }

        // If health runs out
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Increment score when enemy dies
        if (!isPlayer)
        {
            // Increment the score and UI
            if (scoreKeeper != null)
            {
                // Update the score
                scoreKeeper.ModifyScore(score);

                // Update the UI
                uiDisplay.UpdateScore(scoreKeeper.GetScore());
            }
        }

        // Destroy the game object 
        Destroy(gameObject);
    }

    private void PlayHitEffect()
    {
        if(hitEffect != null)
        {
            // Check if particle effect holding area is not null
            if (temporaryObjects == null)
                return;

            // Instantiate the particle
            ParticleSystem particleEffect = Instantiate(hitEffect, transform.position, Quaternion.identity, temporaryObjects.transform);

            // Destroy the particle after the time given in the prefab setting
            Destroy(particleEffect, 
                particleEffect.main.duration + particleEffect.main.startLifetime.constantMax);
        }
    }

    private void ShakeCamera()
    {
        // Only shake the camera if apply camera shake is enabled
        if(cameraShake != null && applyCameraShake)
        {
            // Shake the camera
            cameraShake.Play();
        }
    }
}
