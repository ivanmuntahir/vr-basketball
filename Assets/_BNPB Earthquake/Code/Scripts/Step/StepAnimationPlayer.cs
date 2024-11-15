using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class AnimationData
{
    public Animator animator;
    public string animationName;
    public UnityEvent onAnimationPlayed;
    public UnityEvent onAnimationFinished;
}

public class StepAnimationPlayer : MonoBehaviour
{
    public List<AnimationData> animations;

    public void PlayAllAnimations()
    {
        foreach (var animationData in animations)
        {
            StartCoroutine(PlayAnimation(animationData));
        }
    }

    private IEnumerator PlayAnimation(AnimationData animationData)
    {
        if (animationData.animator != null && !string.IsNullOrEmpty(animationData.animationName))
        {
            // Trigger the onAnimationPlayed event
            animationData.onAnimationPlayed.Invoke();

            // Play the specified animation
            animationData.animator.Play(animationData.animationName);

            // Wait for the animation to finish
            yield return new WaitForSeconds(animationData.animator.GetCurrentAnimatorStateInfo(0).length);

            // Trigger the onAnimationFinished event
            animationData.onAnimationFinished.Invoke();
        }
    }
}
