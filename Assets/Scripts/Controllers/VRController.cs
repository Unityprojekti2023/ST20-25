using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class VRController : MonoBehaviour
{
    public float moveSpeed = 2f; // Movement speed
    public InputActionProperty leftThumbstickInput; // Input action for the left thumbstick

    private CharacterController characterController;
    private XROrigin xrRig; // Reference to the XR Rig (which includes the camera and the controllers)
    //XRRig
    private Transform headTransform; // Reference to the VR headset (camera)

    void Start()
    {
        // Get the CharacterController component
        characterController = GetComponent<CharacterController>();

        // Get the XRRig component (make sure this script is attached to the XRRig or its parent)
        xrRig = GetComponent<XROrigin>();
        //XRRig

        // Get the headset's transform (the camera inside the XR Rig)
        headTransform = xrRig.Camera.transform;
        //cameraGameObject
    }

    void Update()
    {
        // Get the input from the left thumbstick (returns a Vector2 with x and y values)
        Vector2 inputAxis = leftThumbstickInput.action.ReadValue<Vector2>();

        // Move the player based on thumbstick input
        MovePlayer(inputAxis);
    }

    void MovePlayer(Vector2 inputAxis)
    {
        // Get the forward and right directions relative to where the head (VR headset) is looking
        Vector3 forward = new Vector3(headTransform.forward.x, 0, headTransform.forward.z).normalized;
        Vector3 right = new Vector3(headTransform.right.x, 0, headTransform.right.z).normalized;

        // Calculate movement direction based on the thumbstick input
        Vector3 moveDirection = forward * inputAxis.y + right * inputAxis.x;

        // Move the CharacterController
        characterController.Move(moveSpeed * Time.deltaTime * moveDirection);
    }
}
