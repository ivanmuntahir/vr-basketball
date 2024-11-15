using UnityEngine;
using System.Collections;

public class StepAudioWithTimer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    private const float FadeDuration = 3f;
    private float originalVolume;

    public void PlayAudio(float timerDuration)
    {
        audioSource.clip = audioClip;
        originalVolume = audioSource.volume;
        audioSource.Play();
        StartCoroutine(AudioTimerCoroutine(timerDuration));
    }

    private IEnumerator AudioTimerCoroutine(float timerDuration)
    {
        yield return new WaitForSeconds(timerDuration - FadeDuration);

        float fadeOutTime = 0;

        while (fadeOutTime < FadeDuration)
        {
            fadeOutTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(originalVolume, 0, fadeOutTime / FadeDuration);
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = originalVolume;
    }
}
