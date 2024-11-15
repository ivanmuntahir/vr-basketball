using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenarioSetter : MonoBehaviour
{
    public static ScenarioSetter instance;

    private void Awake()
    {
        instance = this;
    }

    public void SetScenarioId(string key)
    {
        StaticData.nextScenarioId = key;
        Debug.Log("Scenario ID set to: " + key);
    }

    public void SetScenarioKey(string key)
    {
        StaticData.nextScenarioData = key;
        Debug.Log("Scenario Key set to: " + key);
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log("Loading scene: " + sceneName);
    }
}
