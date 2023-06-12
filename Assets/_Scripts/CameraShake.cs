using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float shakeDuration = 1f;
    [SerializeField] float shakeMagnitude = 0.2f;

    private Vector3 initialPos = Vector3.zero;

    void Start() => initialPos = transform.position;

    public void Play() => StartCoroutine(ShakeCam());

    IEnumerator ShakeCam()
    {
        float elapsedTime = 0;

        while (elapsedTime < shakeDuration)
        {
            // Shake the camera
            transform.position = initialPos + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            // Allow other processes to update
            yield return new WaitForEndOfFrame();
            // Increment the time
            elapsedTime += Time.deltaTime;
        }

        // set the cam to its initial value
        transform.position = initialPos;
    }
}
