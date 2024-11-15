using UnityEngine;
using UnityEngine.Events;

public class AnimationEventTrigger : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string animationName; // Nama animasi yang ingin dipantau
    [SerializeField] private UnityEvent onAnimationStopped; // UnityEvent yang dipanggil

    private Animator animator;
    private bool isAnimationPlaying;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found!");
        }
    }

    void Update()
    {
        if (animator != null)
        {
            // Cek apakah animasi sedang diputar
            bool currentlyPlaying = IsAnimationPlaying(animationName);

            if (isAnimationPlaying && !currentlyPlaying)
            {
                // Animasi berhenti
                onAnimationStopped.Invoke();
            }

            isAnimationPlaying = currentlyPlaying;
        }
    }

    private bool IsAnimationPlaying(string animName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(animName) && stateInfo.normalizedTime < 1.0f;
    }
}
