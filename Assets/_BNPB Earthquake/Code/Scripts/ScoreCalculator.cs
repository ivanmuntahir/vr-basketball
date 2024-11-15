using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreCalculator : MonoBehaviour
{
    [SerializeField] private Stopwatch stopwatch;
    [SerializeField] private GameObject setActive;
    [SerializeField] private TextMeshProUGUI finalTimeText; 
    [SerializeField] private float timeThreshold = 1200.0f;


    public void CalculateScore()
    {
        int score = 100;
        float elapsedTime = stopwatch.ElapsedTime;
        stopwatch.StopStopwatch();

        if (elapsedTime > timeThreshold)
        {
            int extraMinutes = Mathf.FloorToInt((elapsedTime - timeThreshold) / 60);
            score = Mathf.Clamp(100 - extraMinutes, 0, 100);
            finalTimeText.text = score.ToString();
            setActive.SetActive(true);
        }
        else
        {
            finalTimeText.text = score.ToString();
            setActive.SetActive(true);
        }

        if (!string.IsNullOrEmpty(StaticData.token))
        {
            string jsonData = $"{{\"scenario_id\":\"{StaticData.nextScenarioId}\", \"nilai\":\"{score}\"}}"; ;
            StartCoroutine(APIManager.instance.PostDataWithTokenCoroutine(
                "api/insert-nilai", jsonData,
                res =>
                {
                    print(StaticData.token);
                    print(jsonData);
                    print(res); 
                }));
        }
    }
}
