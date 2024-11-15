using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stopwatch : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timetext;
    [SerializeField] private TextMeshProUGUI stoppedTimeText;

    private float elapsedTime;
    private bool isRunning = true; // Start running by default

    public float ElapsedTime => elapsedTime; // Expose elapsed time

    void Start() // Automatically starts when the game begins
    {
        elapsedTime = 0; // Reset the timer at the start
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimeText();
        }
    }

    void UpdateTimeText()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
        timetext.text = timeString;
    }

    public void StopStopwatch()
    {
        isRunning = false; // Stop the timer
        stoppedTimeText.text = timetext.text;
    }
}
