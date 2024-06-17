using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References to other scripts")]
    CharacterController controller;
    public OptionsMenu optionsMenu;

    [Header("References to Gameobjects")]
    public Camera playerCamera;    
    public GameObject playerModel;
    public GameObject mainCameraButton;
    public GameObject controlPanelCamerakButton;
    public GameObject insideCamerakButton;
    public GameObject helpCameraButton;

    [Header("Other values")]
    public float moveSpeed = 200.0f;
    public float maxYRotation = 80.0f;
    private float rotationX = 0;
    private bool canMove = true;



    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

        playerModel = GameObject.FindWithTag("PlayerModel");

        //Hide camera switch buttons from UI
        mainCameraButton.SetActive(false);
        controlPanelCamerakButton.SetActive(false);
        insideCamerakButton.SetActive(false);
        helpCameraButton.SetActive(false);

        Debug.Log("Player can move now: " + canMove);
    }

    void Update()
    {
        if (!EscapeMenu.GameIsPaused && CameraController.Instance != null)
        {
            if (CameraController.Instance.isMainCamActive)
            {
                if (canMove)
                {
                    HandlePlayerInput();
                    HideCursor();
                }
                else
                {
                    ShowCursor();
                }
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
        mainCameraButton.SetActive(true);
        controlPanelCamerakButton.SetActive(true);
        insideCamerakButton.SetActive(true);
        helpCameraButton.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void HideCursor()
    {
        mainCameraButton.SetActive(false);
        controlPanelCamerakButton.SetActive(false);
        insideCamerakButton.SetActive(false);
        helpCameraButton.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ToggleMovement(bool canMove)
    {
        this.canMove = canMove;
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
