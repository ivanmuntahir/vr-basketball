using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Countdown
{
    public float countdownTime = 10f;
    public UnityEvent onCountdownStarted;
    public UnityEvent onCountdownCompleted;
}

public class StepCountDown : MonoBehaviour
{
    public List<Countdown> countdowns;

    public void StartAllCountdowns()
    {
        // Stop any ongoing countdowns before starting new ones
        StopAllCoroutines();

        // Start all countdowns simultaneously
        foreach (var countdown in countdowns)
        {
            StartCoroutine(CountdownCoroutine(countdown));
        }
    }

    private IEnumerator CountdownCoroutine(Countdown countdown)
    {
        // Invoke the countdown start event
        countdown.onCountdownStarted.Invoke();

        float remainingTime = countdown.countdownTime;

        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            yield return null;
        }

        // Invoke the countdown completed event
        countdown.onCountdownCompleted.Invoke();
    }
}
