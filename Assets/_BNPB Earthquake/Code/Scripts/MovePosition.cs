using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public GameObject objectToMove;

    public float x;
    public float y;
    public float z;
    public float yRotation;
    public float xRotation;
    public float zRotation;

    public void MoveToPosition()
    {
        if (objectToMove != null)
        {
            // Menentukan posisi baru
            Vector3 newPosition = new Vector3(x, y, z);
            objectToMove.transform.position = newPosition;

            // Menentukan rotasi baru
            Quaternion newRotation = Quaternion.Euler(xRotation, yRotation, zRotation);
            objectToMove.transform.rotation = newRotation;
        }
        else
        {
            Debug.LogWarning("Object to move is not assigned!");
        }
    }
}
