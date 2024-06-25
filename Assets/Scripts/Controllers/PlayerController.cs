using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References to other scripts")]
    private CharacterController controller; // Reference to the character controller
    private Transform cameraTransform; // Reference to the main camera's transform

    [Header("Other values")]
    public float moveSpeed = 5.0f; // Speed of the player

    void Start()
    {
        // Get the character controller component
        controller = GetComponentInChildren<CharacterController>();

        // Assuming the main camera is tagged as "MainCamera"
        cameraTransform = Camera.main.transform;
    }

    void FixedUpdate()
    {
            HandlePlayerMovement();
    }

    void HandlePlayerMovement()
    {
        // Get the movement input from the player
        float moveDirectionY = Input.GetAxis("Vertical") * moveSpeed;
        float moveDirectionX = Input.GetAxis("Horizontal") * moveSpeed;

        // Calculate the movement direction based on the camera's forward and right vectors
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0; // Ensure vertical movement is zeroed out

        // Normalize the vectors to prevent faster diagonal movement
        Vector3 move = forward.normalized * moveDirectionY + right.normalized * moveDirectionX;

        // Apply movement to the character controller
        controller.Move(move * Time.fixedDeltaTime);
    }
}
