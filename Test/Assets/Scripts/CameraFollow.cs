using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The target object to follow
    public float distance = 5.0f; // The distance between the camera and the target object
    public float height = 3.0f; // The height of the camera relative to the target object
    public float followSpeed = 5.0f; // The speed at which the camera follows

    private Vector3 offset; // The initial offset between the camera and the target object

    void Start()
    {
        // Initially, calculate the offset between the camera and the target object
        if (target != null)
        {
            offset = transform.position - target.position;
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Calculate the position where the camera should be, based on the target object's current position and the initial offset
        Vector3 targetCameraPosition = target.position + offset;

        // Smoothly move the camera to the calculated position
        transform.position = Vector3.Lerp(transform.position, targetCameraPosition, followSpeed * Time.deltaTime);

        // The camera always faces the target object
        transform.LookAt(target.position);
    }
}
