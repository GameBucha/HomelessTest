using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The target the camera should follow (player)
    public Vector3 offset; // Camera's offset relative to the player
    public float smoothSpeed = 0.125f; // Smoothing speed for camera movement

    void LateUpdate()
    {
        if (target == null) return;

        // Calculate desired position
        Vector3 desiredPosition = target.position + offset;

        // Smoothly move the camera to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Keep the camera rotation; look at the target
        transform.LookAt(target);
    }
}
