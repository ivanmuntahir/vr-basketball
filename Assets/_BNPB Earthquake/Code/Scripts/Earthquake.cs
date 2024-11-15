using System.Collections;
using UnityEngine;

public class Earthquake : MonoBehaviour
{
    public Transform cameraTransform; // Assign your camera transform here
    public float intensity = 0.5f;    // Intensity of the shake
    public float duration = 2.0f;     // Duration of the shake

    private Vector3 originalPosition;

    void Start()
    {
        // Set originalPosition only if cameraTransform is assigned
        if (cameraTransform != null)
        {
            originalPosition = cameraTransform.localPosition;
        }
        else
        {
            Debug.LogError("Camera Transform is not assigned.");
        }
    }

    public void StartEarthquake()
    {
        if (cameraTransform != null)
        {
            StartCoroutine(Shake());
        }
        else
        {
            Debug.LogWarning("Cannot start earthquake, cameraTransform is not assigned.");
        }
    }

    private IEnumerator Shake()
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * intensity;
            float y = Random.Range(-1f, 1f) * intensity;
            float z = Random.Range(-1f, 1f) * intensity;

            cameraTransform.localPosition = originalPosition + new Vector3(x, y, z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        cameraTransform.localPosition = originalPosition;  // Reset to original position
    }
}
