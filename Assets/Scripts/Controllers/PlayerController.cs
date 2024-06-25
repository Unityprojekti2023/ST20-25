using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References to other scripts")]
    private CharacterController controller;
    public OptionsMenu optionsMenu;
    private Transform cameraTransform; // Reference to the main camera's transform

    [Header("Other values")]
    public float moveSpeed = 5.0f;

    void Start()
    {
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
        float moveDirectionY = Input.GetAxis("Vertical") * moveSpeed;
        float moveDirectionX = Input.GetAxis("Horizontal") * moveSpeed;

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0; // Ensure vertical movement is zeroed out

        //Vector3 move = (forward.normalized * moveDirectionY + right.normalized * moveDirectionX).normalized;
        Vector3 move = forward.normalized * moveDirectionY + right.normalized * moveDirectionX;
        //Vector3 move = transform.right * moveDirectionX + transform.forward * moveDirectionY;

        // Apply movement to the character controller
        controller.Move(move * Time.fixedDeltaTime);
    }
}
