using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingToCamera : MonoBehaviour
{
    public Camera mainCamera;

    void LateUpdate()
    {
        if (mainCamera == null) return;
        Vector3 directionToCamera = mainCamera.transform.position - transform.position;
        directionToCamera.x = directionToCamera.z = 0.0f; // Only rotate around the Y-axis

        transform.LookAt(mainCamera.transform.position - directionToCamera);
        transform.Rotate(0, 180, 0); // Adjust if needed to ensure correct facing direction
    }
}
