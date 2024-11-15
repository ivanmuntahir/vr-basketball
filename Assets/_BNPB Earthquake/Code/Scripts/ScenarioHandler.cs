using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ScenarioData
{
    public string scenarioKey;
    public GameObject scenarioObject;
}

public class ScenarioHandler : MonoBehaviour
{
    public List<ScenarioData> scenarios;

    void Start()
    {
        for (int i = 0; i < scenarios.Count; i++)
        {
            if (StaticData.nextScenarioData == scenarios[i].scenarioKey)
            {
                scenarios[i].scenarioObject.SetActive(true);
                break;
            }
        }
    }
}
