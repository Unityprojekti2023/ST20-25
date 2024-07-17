using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CaliperController : MonoBehaviour
{
    [Header("References to other gameobjects")]
    public Camera caliperCamera;
    public TextMeshPro measurementText;
    public GameObject caliper;
    private GameObject slidingParts;

    Vector3 offset;
    Vector3 slidingPartStartPosition;
    bool isCaliperAttached = false;
    bool isCaliperRotated = false;
    bool isFirstAttach = true;  //TODO: Is there a better way to handle not rotating the caliper on the first attach?

    void Start()
    {
        // Hide the caliper
        caliper.SetActive(false);
        //  Find the sliding parts of the caliper
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
        if (caliperCamera.gameObject.activeSelf)
        {
            // Detach the caliper if the right mouse button is clicked
            if (Input.GetMouseButtonDown(1))
            {
                ToggleCaliperAttachment();
                Cursor.visible = !Cursor.visible;
            }

            // If the caliper is attached to the mouse, update its position
            if (isCaliperAttached)
            {
                if (Input.GetMouseButtonDown(0) && !isFirstAttach)
                {
                    //Rotate caliper between vertical and horizontal
                    RotateCaliper();
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

                isFirstAttach = false;
            }
        }
        else if (!caliperCamera.gameObject.activeSelf && !isFirstAttach)
        {
            isCaliperAttached = false;
            isFirstAttach = true;

            if (Cursor.visible)
                Cursor.visible = false;
            // Hide the caliper
            if (caliper.activeSelf)
                caliper.SetActive(false);
        }
    }

    public void ToggleCaliperAttachment()
    {
        isCaliperAttached = !isCaliperAttached;
        caliper.SetActive(true);

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
            caliper.transform.localRotation = Quaternion.Euler(270f, 90f, 0);
        }
        else
        {
            caliper.transform.localRotation = Quaternion.Euler(270f, 0, 0);
        }
    }
}
