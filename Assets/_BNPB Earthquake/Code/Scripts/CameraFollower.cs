using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public GameObject objectToFollow;  // The GameObject that should be placed in front of the camera
    public float distanceFromCamera = 1.5f;  // Distance in front of the camera

    private Transform cameraTransform;  // Reference to the camera's transform

    void Start()
    {
        // Get the main camera's transform
        cameraTransform = Camera.main.transform;

        // Place the object in front of the camera once
        PlaceObjectInFrontOfCamera();
    }

    void PlaceObjectInFrontOfCamera()
    {
        // Set the object's position in front of the camera at the specified distance
        objectToFollow.transform.position = cameraTransform.position + cameraTransform.forward * distanceFromCamera;

        // Optionally make the object face the same direction as the camera
        objectToFollow.transform.rotation = cameraTransform.rotation;
    }
}
