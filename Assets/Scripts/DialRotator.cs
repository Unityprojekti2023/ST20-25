using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialRotator : MonoBehaviour
{
    public ControlPanelInteractable controlPanelInteractable;


    private bool isRotating = false;
    private bool rotationHandled = false;
    private float initialAngle;
    private float initialDialAngle;
    private Vector3 initialMousePosition;
    private Quaternion originalRotation;
    public Camera controlpanelCamera;

    private const float maxRotation = 85f; // Maximum rotation angle
    private const float minRotation = -85f; // Minimum rotation angle

    void Start()
    {
        originalRotation = transform.rotation;

        // Get the control panel interactable script
        controlPanelInteractable = GetComponentInParent<ControlPanelInteractable>();
        if (controlPanelInteractable == null)
        {
            Debug.LogError("ControlPanelInteractable script not found");
        }
    }

    void Update()
    {
        // Check if the mouse button is pressed down
        if (Input.GetMouseButtonDown(0))
        {
            // Get the mouse position in world space
            Ray ray = controlpanelCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the mouse is within the dial's collider bounds
                if (hit.collider == GetComponent<Collider>())
                {
                    isRotating = true;

                    // Store the initial mouse position
                    Plane plane = new Plane(Vector3.up, transform.position);
                    float distance;
                    if (plane.Raycast(ray, out distance))
                    {
                        initialMousePosition = ray.GetPoint(distance);

                        // Calculate the initial angle
                        Vector3 direction = initialMousePosition - transform.position;
                        direction.y = 0; // Ignore Y axis to rotate around Z axis
                        initialAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

                        // Store the initial dial angle
                        initialDialAngle = transform.rotation.eulerAngles.z;
                    }
                }
            }
        }

        // Check if the mouse button is released
        if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
            rotationHandled = false;

            // Reset the rotation
            transform.rotation = originalRotation;
        }

        // Rotate the object if the mouse button is held down
        if (isRotating)
        {
            RotateObject();
        }
    }

    void RotateObject()
    {
        // Get the current mouse position in world space
        Ray ray = controlpanelCamera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 currentMousePosition = ray.GetPoint(distance);

            // Calculate the direction from the center of the dial to the current mouse position
            Vector3 direction = currentMousePosition - transform.position;
            direction.y = 0; // Ignore Y axis to rotate around Z axis
            float currentAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

            // Calculate the delta angle (reverse rotation)
            float deltaAngle = initialAngle - currentAngle;

            // Apply the rotation difference to the initial dial angle
            float newDialAngle = initialDialAngle + deltaAngle;

            // Normalize the angle to be within the -180 to 180 range
            newDialAngle = NormalizeAngle(newDialAngle);

            // Clamp the newDialAngle to within the desired range
            newDialAngle = Mathf.Clamp(newDialAngle, minRotation, maxRotation);

            // Update the rotation
            transform.rotation = Quaternion.Euler(0, 0, newDialAngle);

            // Check if the dial has reached the maximum or minimum angle and call OnDialRotated accordingly
            if (Mathf.Abs(newDialAngle - maxRotation) < 0.01f && !rotationHandled)
            {
                Debug.Log("Max Rotation");
                rotationHandled = true;
                controlPanelInteractable.OnDialRotated(true);
            }
            else if (Mathf.Abs(newDialAngle - minRotation) < 0.01f && !rotationHandled)
            {
                Debug.Log("Min Rotation");
                rotationHandled = true;
                controlPanelInteractable.OnDialRotated(false);
            }
        }
    }

    // Helper method to normalize angles to the -180 to 180 range
    float NormalizeAngle(float angle)
    {
        while (angle > 180f) angle -= 360f;
        while (angle < -180f) angle += 360f;
        return angle;
    }
}
