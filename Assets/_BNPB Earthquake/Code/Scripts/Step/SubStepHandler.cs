using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SubStepHandler : MonoBehaviour
{
    public bool isDone;
    public UnityEvent onSubStepStarted;
    public UnityEvent onSubStepCompleted;

    public void FinishStep()
    {
        GetComponentInParent<StepHandler>().SetConditionLastSubStep(this);
    }

    public void OnSubStepStarted()
    {
        onSubStepStarted?.Invoke();
    }

    public void OnSubStepCompleted()
    {
        onSubStepCompleted?.Invoke();
    }
}
