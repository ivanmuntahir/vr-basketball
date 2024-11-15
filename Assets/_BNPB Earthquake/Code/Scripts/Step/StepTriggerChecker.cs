using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StepTriggerChecker : MonoBehaviour
{
    public string targetTag;
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;
    private Collider triggerCollider; // Public trigger collider

    private void Start()
    {
        triggerCollider = GetComponent<Collider>();
        // Ensure the triggerCollider is set as a trigger
        // if (triggerCollider != null)
        // {
        //     triggerCollider.isTrigger = true;
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            onTriggerEnter.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            onTriggerExit.Invoke();
        }
    }
}
