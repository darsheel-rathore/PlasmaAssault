using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private bool applyCameraShake;

    private CameraShake cameraShake;
    private AudioPlayer audioPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            // Do some damage
            TakeDamage(damageDealer.GetDamage());

            // Destroy the enemy/projectile
            damageDealer.Hit();
        }
    }

    private void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindAnyObjectByType<AudioPlayer>();
    }

    private void TakeDamage(int damage)
    {
        // Reduce health
        health -= damage;

        // Show some effect on damage
        PlayHitEffect();

        // Shake the camera
        ShakeCamera();

        // If health runs out
        if (health <= 0)
        {
            // Play Destroy audio clip
            audioPlayer.PlayDamageClip();

            // Destroy the game object 
            Destroy(gameObject);
        }
    }

    private void PlayHitEffect()
    {
        if(hitEffect != null)
        {
            // Instantiate the particle
            ParticleSystem particleEffect = Instantiate(hitEffect, transform.position, Quaternion.identity);
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
