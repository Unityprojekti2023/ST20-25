using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("References to other scripts")]
    public OptionsMenu optionsMenu; // Reference to the options menu
    public Transform target; // Reference to the player
    public EscapeMenu escapeMenu; // Reference to the escape menu

    [Header("References to Gameobjects")]
    public Camera playerCamera; // Reference to the player's camera

    [Header("Camera Settings")]
    public Vector3 offset; // Offset of the camera from the player
    public float smoothSpeed = 0.125f; // Speed of the camera following the player
    private float pitch = 0.0f; // Pitch of the camera

    void Start()
    {
        if (escapeMenu == null)
        {
            Debug.LogError("EscapeMenu reference not set in CameraFollow!");
            return;
        }

        Cursor.lockState = CursorLockMode.Locked;

        //Hide camera switch buttons from UI
    }
    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogError("Target not set in CameraFollow!");
            return;
        }

        if (!escapeMenu.isGamePaused)
        {
            // Check if main camera is active
            if (CameraController.Instance.isMainCamActive)
            {
                // Calculate the desired position of the camera
                Vector3 desiredPosition = target.position + offset;

                // Smoothly move the camera to the desired position
                Vector3 smoothedPosition = Vector3.Lerp(playerCamera.transform.position, desiredPosition, smoothSpeed);
                playerCamera.transform.position = smoothedPosition;

                // Handle camera rotation separately
                RotateCamera();
            }
            else
                return;

        }
        else
            return;
    }

    void RotateCamera()
    {
        // Get the mouse input
        float mouseX = Input.GetAxis("Mouse X") * optionsMenu.mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * optionsMenu.mouseSensitivity;

        // Calculate the pitch of the camera
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -90.0f, 90.0f);

        // Rotate the camera
        playerCamera.transform.localRotation = Quaternion.Euler(pitch, 0.0f, 0.0f);
        target.Rotate(Vector3.up * mouseX);
    }
}
