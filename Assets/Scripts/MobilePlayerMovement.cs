using UnityEngine;

public class MobilePlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    public Joystick joystick;   // Reference to the FixedJoystick
    public Transform cameraTransform; // Reference to the main camera's transform
    private Vector3 movement;
    private Rigidbody rb; // Reference to the Rigidbody
    private Animator animator; // Reference to the Animator

    private float initialCameraY; // Fixed Y position for the camera

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
        animator = GetComponent<Animator>(); // Get the Animator component

        if (cameraTransform != null)
        {
            initialCameraY = cameraTransform.position.y; // Save the camera's initial height
        }
    }

    void Update()
    {
        // Stabilize the camera's position on the Y-axis and follow the player
        if (cameraTransform != null)
        {
            cameraTransform.position = new Vector3(transform.position.x, initialCameraY, transform.position.z);
            // Do not modify rotation here; let CameraFollow handle it.
        }
    }

    void LateUpdate()
    {
        // Get joystick input
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        // Create the direction vector from joystick input
        Vector3 inputDirection = new Vector3(horizontal, 0, vertical).normalized;

        // Transform input direction relative to the camera
        if (cameraTransform != null)
        {
            Vector3 forward = cameraTransform.forward; // Camera's forward vector
            Vector3 right = cameraTransform.right;     // Camera's right vector

            // Ignore the y-component (make the movement on a flat plane)
            forward.y = 0f;
            right.y = 0f;

            // Normalize the vectors to ensure consistent speed
            forward.Normalize();
            right.Normalize();

            // Calculate the movement direction relative to the camera
            movement = forward * inputDirection.z + right * inputDirection.x;
        }
        else
        {
            // Fallback if no camera is assigned
            movement = inputDirection;
        }

        // If there is movement, rotate the character to face the movement direction
        if (movement.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);
        }

        // Update the Animator's Speed parameter
        animator.SetFloat("Speed", movement.magnitude * moveSpeed);
    }

    void FixedUpdate()
    {
        // Move the player using Rigidbody.MovePosition
        Vector3 newPosition = rb.position + movement * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }
}
