using UnityEngine;

public enum StepType
{
    PopUp, Interact
}

[CreateAssetMenu(fileName = "StepData", menuName = "StepData", order = 1)]
public class StepScriptableObject : ScriptableObject
{
    [TextArea] public string taskNameId;
    [TextArea] public string taskNameEn;
    [Space]
    [TextArea] public string activityNameId;
    [TextArea] public string activityNameEn;
}