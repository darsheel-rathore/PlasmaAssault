using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifeTime = 5f;
    [SerializeField] float baseFiringRate = 0.2f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0;
    [SerializeField] float minFiringRate = 0.1f;

    private bool isFiring = false;
    private Coroutine firingCoroutine = null;
    
    private GameObject projectileGO;
    private AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = FindAnyObjectByType<AudioPlayer>();
    }

    private void Start()
    {
        if(useAI)
        {
            isFiring = true;
        }

        // Init fields
        projectileGO = GameObject.FindWithTag("Projectile");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Fire();
    }

    public void SetIsFiring(bool value) => isFiring = value;

    private void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinously());
        }
        else if(!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinously()
    {
        while (true)
        {
            // Instantiate Projectile Prefab
            GameObject instance = Instantiate(projectilePrefab, 
                                                transform.position, 
                                                Quaternion.identity, 
                                                projectileGO.transform);
            
            // Rigidbody Ref
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // Give velocity to the projectile
                rb.velocity = transform.up * projectileSpeed;
            }
            // Destroy that projectile prefab after certain time
            Destroy(instance, projectileLifeTime);
            
            // Calculating Slight Random Rate of Fire
            float timeToNextProjectile = Random.Range(baseFiringRate - firingRateVariance,
                                                        baseFiringRate + firingRateVariance);

            // Clamping that value
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minFiringRate, float.MaxValue);

            // Play shooting audio
            audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }

}
