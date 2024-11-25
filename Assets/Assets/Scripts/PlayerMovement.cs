using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    private Vector3 movement;

    // Reference to the camera
    public Camera mainCamera;

    void Update()
    {
        // Read input data from the virtual joystick
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Create a movement vector
        movement = new Vector3(horizontal, 0, vertical).normalized;

        // If there is movement, rotate the character towards its direction
        if (movement.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);
        }
    }

    void FixedUpdate()
    {
        // Move the character
        transform.Translate(movement * moveSpeed * Time.fixedDeltaTime, Space.World);
    }
}
