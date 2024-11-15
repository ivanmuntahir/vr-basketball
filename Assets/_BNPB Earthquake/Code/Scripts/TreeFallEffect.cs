using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFallEffect : MonoBehaviour
{
    public List<GameObject> trees;
    public float duration = 2.0f;
    private bool stopFalling = false;

    public void StartTreeFallSequence()
    {
        stopFalling = false; // Reset the stop flag when starting the sequence
        StartCoroutine(TreeFallSequence());
    }

    public void StopTreeFallSequence()
    {
        stopFalling = true; // Set the flag to stop the sequence
    }

    private IEnumerator TreeFallSequence()
    {
        foreach (GameObject tree in trees)
        {
            if (stopFalling) yield break; // Stop the sequence if stopFalling is true

            PlayTreeFallSound(tree);
            StartCoroutine(FallTree(tree));
            yield return new WaitForSeconds(duration);
        }
    }

    private IEnumerator FallTree(GameObject tree)
    {
        float randomXRotation = 0f;
        float randomZRotation = 0f;

        if (Random.value > 0.5f)
        {
            randomXRotation = Random.value > 0.5f ? -78f : 78f;
            randomZRotation = Random.Range(-78f, 78f);
        }
        else
        {
            randomZRotation = Random.value > 0.5f ? -78f : 78f;
            randomXRotation = Random.Range(-78f, 78f);
        }

        Quaternion initialRotation = tree.transform.rotation;
        Quaternion targetRotation = initialRotation * Quaternion.Euler(randomXRotation, 0, randomZRotation);

        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            if (stopFalling) yield break; // Stop falling immediately if stopFalling is true

            tree.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        tree.transform.rotation = targetRotation;
    }

    private void PlayTreeFallSound(GameObject tree)
    {
        AudioSource audioSource = tree.GetComponent<AudioSource>();

        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No AudioSource found on tree: " + tree.name);
        }
    }
}
