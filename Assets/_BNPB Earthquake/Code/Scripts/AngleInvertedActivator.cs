using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AngleInvertedActivator : MonoBehaviour
{
    public float tolerance = 0.5f;
    public AudioClip audioRotating;
    public UnityEvent whenFinished;

    AudioSource rotatingAudioSource;
    SteeringWheel valve;

    private void Awake()
    {
        rotatingAudioSource = gameObject.AddComponent<AudioSource>();
        valve = GetComponentInChildren<SteeringWheel>();
    }

    public void Activator()
    {
        if (Mathf.Abs(valve.AngleInverted - valve.MaxAngle) <= tolerance ||
            Mathf.Abs(valve.AngleInverted + valve.MaxAngle) <= tolerance)
        {
            whenFinished.Invoke();
        }
        else
        {
            rotatingAudioSource.PlayOneShot(audioRotating);
        }
    }
}
