using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CaliperController : MonoBehaviour
{
    [Header("References to other gameobjects")]
    public Camera caliperCamera;
    public TextMeshPro measurementText; // Use Text if using Unity UI
    public GameObject caliper;
    private GameObject slidingParts;
    private bool isCaliperAttached = false;
    private bool isCaliperRotated = false;
    private Vector3 offset;
    private Vector3 slidingPartStartPosition;

    // Store the initial local position of the sliding part


    void Start()
    {
        // Find the sliding parts of the caliper
        slidingParts = caliper.transform.Find("SlidingParts").gameObject;
        if (slidingParts == null)
        {
            Debug.LogError("Sliding parts not found.");
        }
        else
        {
            slidingPartStartPosition = slidingParts.transform.localPosition;
        }
    }

    void Update()
    {
        // Check if the caliper camera is active
        if (!caliperCamera.gameObject.activeSelf)
        {
            isCaliperAttached = false;
        }

        // If the caliper is attached to the mouse, update its position
        if (isCaliperAttached)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Rotate caliper between vertical and horizontal
                RotateCaliper();
            }

            // Detach the caliper if the right mouse button is clicked
            if (Input.GetMouseButtonDown(1))
            {
                isCaliperAttached = !isCaliperAttached;
            }

            float scrollInput = Input.GetAxis("Mouse ScrollWheel"); // mouse scroll input
            Vector3 newPosition = slidingParts.transform.localPosition + 0.5f * scrollInput * Vector3.right;
            newPosition.x = Mathf.Clamp(newPosition.x, -7.8f, 0f);
            // Round the position to 2 decimal places
            newPosition.x = (float)Math.Round(newPosition.x, 2);

            slidingParts.transform.localPosition = newPosition;

            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10.0f; // Set the distance from the camera
            Vector3 worldPosition = caliperCamera.ScreenToWorldPoint(mousePosition);
            caliper.transform.position = worldPosition + offset;

            // Update the measurement in real-time
            UpdateMeasurement();
        }
    }

    public void ToggleCaliperAttachment()
    {
        isCaliperAttached = !isCaliperAttached;

        if (isCaliperAttached)
        {
            // Calculate the offset between the caliper and the mouse position
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10.0f; // Set the distance from the camera
            Vector3 worldPosition = caliperCamera.ScreenToWorldPoint(mousePosition);
            offset = caliper.transform.position - worldPosition;
        }
    }

    void UpdateMeasurement()
    {
        // Calculate the distance between the sliding part's current position and the start position
        float distance = Vector3.Distance(slidingPartStartPosition, slidingParts.transform.localPosition);
        // Convert the distance to millimeters (if needed) and update the TextMeshPro or UI Text

        measurementText.text = $"{distance * 10:F2}";
    }

    void RotateCaliper()
    {
        isCaliperRotated = !isCaliperRotated;

        // Handle the rotation of the caliper between vertical and horizontal
        if (isCaliperRotated)
        {
            caliper.transform.localRotation = Quaternion.Euler(270f, 270f, 0);
        }
        else
        {
            caliper.transform.localRotation = Quaternion.Euler(270f, 0, 0);
        }
    }
}
