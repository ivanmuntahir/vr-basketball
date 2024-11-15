using UnityEngine;
using UnityEngine.Events;

public class TriggerEventOnNamedTag : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string requiredTag; 
    [SerializeField] private UnityEvent onTagTriggerEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(requiredTag))
        {
            onTagTriggerEvent.Invoke();
        }
    }
}
