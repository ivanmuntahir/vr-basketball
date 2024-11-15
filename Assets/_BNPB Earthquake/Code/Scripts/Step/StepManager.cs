using System;
using System.Collections;
using System.Collections.Generic;
/*using Oculus.Platform;*/
using UnityEngine;

[Serializable]
public class Step
{
    public string stepName;
    [TextArea(3, 10)]
    public string stepInstructions;
    public AudioClip stepVoiceOver;
    public StepHandler stepHandler;
    public bool isStepCompleted;
}

public class StepManager : MonoBehaviour
{
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioClip correctClip;
    public bool useCorrectClip;

    [Header("Step Data")]
    public int stepIndex; // Using stepIndex for step tracking
    public List<Step> stepList;

    void Start()
    {
        StartStep();
        PlayBGM();
    }

    public void StartStep()
    {
        if (stepIndex < stepList.Count)
        {
            Step currentStep = stepList[stepIndex];

            // If the current step is already completed, move to the next step
            if (currentStep.isStepCompleted)
            {
                stepIndex++;
                if (stepIndex < stepList.Count)
                {
                    currentStep = stepList[stepIndex];
                }
                else
                {
                    Debug.Log("[StepManager] All steps completed!");
                    return;
                }
            }

            // Start the sub-steps for the current step
            currentStep.stepHandler?.StartSubStep();
        }
        else
        {
            Debug.Log("[StepManager] Scenario is Done!");
            StopBGM();
        }
    }

    public void PlayCorrectClip()
    {
        if (!useCorrectClip) return;
        if (correctClip != null)
        {
            sfxSource.PlayOneShot(correctClip);
        }
    }

    // Function to play background music, with optional AudioClip parameter
    public void PlayBGM(AudioClip clip = null)
    {
        if (bgmSource != null)
        {
            if (clip != null)
            {
                bgmSource.clip = clip;
            }

            if (!bgmSource.isPlaying)
            {
                bgmSource.Play();
            }
        }
    }

    // Function to stop background music
    public void StopBGM()
    {
        if (bgmSource != null && bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }
    }

    // Function to play sound effect
    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    // Function to stop sound effect
    public void StopSFX()
    {
        if (sfxSource != null && sfxSource.isPlaying)
        {
            sfxSource.Stop();
        }
    }
}
