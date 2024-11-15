using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public Transform camTransform;
    public float shakeAmount;
    public bool isShaking;
    Vector3 originalPos;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (isShaking) camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
        else camTransform.localPosition = originalPos;
    }

    public void SetShakingState(bool cond)
    {
        isShaking = cond;
    }
}