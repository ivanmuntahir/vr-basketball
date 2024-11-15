using UnityEngine;

public class TimerScript : MonoBehaviour
{
    private float timeElapsed = 0f; // Menghitung waktu yang telah berlalu
    private bool isTimerActive = false;

    void Update()
    {
        if (isTimerActive)
        {
            timeElapsed += Time.deltaTime; // Update waktu yang telah berlalu
        }
    }

    public void StartTimer()
    {
        isTimerActive = true;
    }

    public void StopTimer()
    {
        isTimerActive = false;
    }

    public void ResetTimer()
    {
        isTimerActive = false;
        timeElapsed = 0f; // Reset waktu ke 0
    }

    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public float GetTimeInSeconds()
    {
        return timeElapsed;
    }
}
