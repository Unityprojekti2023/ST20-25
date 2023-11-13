using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    public Camera playerCamera;
    public GameObject playerModel; // Add a reference to the player model
    public float moveSpeed = 160.0f;
    public float maxYRotation = 80.0f;

    private float rotationX = 0;

    public OptionsMenu optionsMenu;
    
    public GameObject cameraBackButton;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

        playerModel = GameObject.FindWithTag("PlayerModel");
        
        cameraBackButton.SetActive(false);
    }

    void Update()
    {
        if (!EscapeMenu.GameIsPaused && CameraController.Instance != null)
        {
            if (CameraController.Instance.isMainCamActive)
            {
                HandlePlayerInput();
                HideCursor();
            }
            else
            {
                ShowCursor();
            }
        }
    }

    void HandlePlayerInput()
    {
        float mouseX = Input.GetAxis("Mouse X") * optionsMenu.GetSensitivity();
        float mouseY = Input.GetAxis("Mouse Y") * optionsMenu.GetSensitivity();

        transform.Rotate(0, mouseX, 0);

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -maxYRotation, maxYRotation);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        float moveForward = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float moveSideways = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        Vector3 move = transform.forward * moveForward + transform.right * moveSideways;
        controller.Move(move);
    }

    void ShowCursor()
    {
        cameraBackButton.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void HideCursor()
    {
        cameraBackButton.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Method to show the player model
    public void ShowPlayerModel()
    {
        if (playerModel != null)
        {
            Renderer[] renderers = playerModel.GetComponentsInChildren<Renderer>(true);

            foreach (Renderer renderer in renderers)
            {
                renderer.enabled = true;
            }
        }
    }

    // Method to hide the player model
    public void HidePlayerModel()
    {
        if (playerModel != null)
        {
            Renderer[] renderers = playerModel.GetComponentsInChildren<Renderer>(true);

            foreach (Renderer renderer in renderers)
            {
                renderer.enabled = false;
            }
        }
    }
}
