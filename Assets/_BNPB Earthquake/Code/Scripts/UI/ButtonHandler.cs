using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonHandler : MonoBehaviour
{
    public UnityEvent onNextAction;
    public UnityEvent onPreviousAction;

    public void NextAction()
    {
        onNextAction.Invoke();
    }
    public void PreviousAction()
    {
        onPreviousAction.Invoke();
    }

}
