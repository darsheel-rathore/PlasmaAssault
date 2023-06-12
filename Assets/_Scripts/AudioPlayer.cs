using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f, 1f)] float shootingVolume = 0.15f;

    [Header("Damage")]
    [SerializeField] AudioClip damageClip;
    [SerializeField] [Range(0f, 1f)] float damageVolume = 0.3f;

    // Shooting Clip Method
    public void PlayShootingClip()
    {
        if(shootingClip != null)
        {
            PlayClip(shootingClip, shootingVolume);
        }
    }

    // Damage Clip Method
    public void PlayDamageClip()
    {
        if(damageClip != null)
        {
            PlayClip(damageClip, damageVolume);
        }
    }

    // Method will play the audio
    private void PlayClip(AudioClip clip, float volume)
    {
        if(clip != null)
        {
            Vector3 camPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, camPos, volume);
        }
    }
}
