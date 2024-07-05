using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerInteractor : MonoBehaviour, IInteractable
{
    private Transform door;
    private Quaternion targetRotation;
    private readonly float RotationSpeed = 45f; // Rotation speed degrees per second
    private bool isRotating = false;
    private bool isDoorOpen = false; // Track whether the door is currently open

    void Start()
    {
        // Find the door object in the locker
        door = transform.parent;
        if (door == null)
        {
            Debug.LogError("Door object not found");
        }
    }

    public void Interact()
    {
        // Toggle the door open and close
        if (isRotating) return; // Prevent interaction while door is rotating

        if (isDoorOpen)
        {
            // Set the target rotation to close the door
            targetRotation = Quaternion.Euler(-90, 0, 0);
        }
        else
        {
            // Set the target rotation to open the door
            targetRotation = Quaternion.Euler(-90, 0, -90);
        }

        isDoorOpen = !isDoorOpen; // Toggle the door state
        isRotating = true; // Start the rotation process
    }

    void Update()
    {
        if (door != null && isRotating)
        {
            // Smoothly rotate the door towards the target rotation
            door.rotation = Quaternion.RotateTowards(door.rotation, targetRotation, RotationSpeed * Time.deltaTime);
            // Check if the door has reached the target rotation
            if (Quaternion.Angle(door.rotation, targetRotation) < 0.1f)
            {
                door.rotation = targetRotation; // Snap to the target rotation
                isRotating = false;
            }
        }
    }
}