using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    public float moveSpeed = 5.0f;
    public float sensitivity = 2.0f;
    public float maxYRotation = 80.0f;

    private float rotationX = 0;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        transform.Rotate(0, mouseX, 0);

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -maxYRotation, maxYRotation);

        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        float moveForward = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float moveSideways = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        Vector3 move = transform.forward * moveForward + transform.right * moveSideways;
        controller.Move(move);
    }
}