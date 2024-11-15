using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepHandler : MonoBehaviour
{
    public List<SubStepHandler> subStep;
    private StepManager stepManager;
    private int subStepIndex = 0; // Tracks the current active sub-step

    private void Start()
    {
        stepManager = FindObjectOfType<StepManager>();
    }

    public void StartSubStep()
    {
        if (subStepIndex < subStep.Count)
        {
            // Activate the sub-step at the current index if it is not done
            SubStepHandler currentSubStep = subStep[subStepIndex];
            if (!currentSubStep.isDone)
            {
                currentSubStep.gameObject.SetActive(true);
                currentSubStep.OnSubStepStarted();
            }
        }
    }

    public void SetConditionLastSubStep(SubStepHandler subStep)
    {
        stepManager.PlayCorrectClip();

        if (subStepIndex < this.subStep.Count && this.subStep[subStepIndex] == subStep && !subStep.isDone)
        {
            subStep.OnSubStepCompleted();
            subStep.isDone = true;

            // Move to the next sub-step
            subStepIndex++;

            // Check if there are more sub-steps to process
            if (subStepIndex < this.subStep.Count && this.subStep[subStepIndex].isDone == false)
            {
                StartSubStep();
            }
            else
            {
                // All sub-steps are done, mark the corresponding step as completed
                MarkStepAsCompleted();
                stepManager.StartStep();
            }
        }
    }

    private void MarkStepAsCompleted()
    {
        // Find the step in the stepManager's stepList that corresponds to this StepHandler
        if (stepManager != null)
        {
            Step step = stepManager.stepList.Find(s => s.stepHandler == this);
            if (step != null)
            {
                step.isStepCompleted = true;
                Debug.Log($"[StepHandler] Step '{step.stepName}' is completed.");
            }
        }
    }
}
