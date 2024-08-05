using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [Header("Cameras and Buttons")]
    public List<Camera> cameras;
    public Button[] cameraButtons;
    public Button drawingButton;
    public GameObject clipboardSpot;

    [Header("References to other gameobjects")]
    public GameObject crosshair;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI objectiveText;
    public TextMeshProUGUI secondaryInteractionText;
    bool toggleClipCam;
    int activeCameraIndex = 0;
    private enum CameraIndex
    {
        Main,
        ControlPanel,
        Lathe,
        Notes,
        Clipboard,
        Measuring
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Hide all buttons
        foreach (var button in cameraButtons)
        {
            if (button.gameObject.activeSelf)
                button.gameObject.SetActive(false);
        }
        drawingButton.gameObject.SetActive(false);

        // Ensure that only one camera is active at the start
        for (int i = 0; i < cameras.Count; i++)
        {
            cameras[i].gameObject.SetActive(i == activeCameraIndex);
        }

        // Initialize UI buttons listeners
        for (int i = 0; i < cameraButtons.Length; i++)
        {
            int index = i;
            cameraButtons[i].onClick.AddListener(() => SwitchToCamera(index));
        }

        // Initialize drawing button listener to ToggleClipboardCamera
        drawingButton.onClick.AddListener(() => ToggleClipboardCamera());
    }

    public void SwitchToCamera(int index)
    {
        // Check if the index is out of bounds
        if (index < 0 || index >= cameras.Count)
        {
            Debug.LogError("Invalid camera index");
            return;
        }

        cameras[activeCameraIndex].gameObject.SetActive(false);     // Deactivate the current camera
        cameras[index].gameObject.SetActive(true);                  // Activate the new camera
        activeCameraIndex = index;                                  // Update the active camera index
        Cursor.visible = true;                                      // Makes sure that the cursor is visible when not locked

        switch ((CameraIndex)index)
        {
            case CameraIndex.Main:
                HandleMainCamera();
                break;
            case CameraIndex.ControlPanel:
                HandleControlPanelCamera(index);
                break;
            case CameraIndex.Lathe:
                HandleControlPanelCamera(index);
                break;
            case CameraIndex.Notes:
                HandleControlPanelCamera(index);
                break;
            case CameraIndex.Clipboard:
                HandleClipboardCamera();
                break;
            case CameraIndex.Measuring:
                HandleMeasuringCamera();
                break;
        }
    }


    private void HandleControlPanelCamera(int cameraIndex)
    {
        // Show camera buttons canvas
        foreach (var button in cameraButtons)
        {
            button.gameObject.SetActive(true);
        }

        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        crosshair.SetActive(false);

        // Hide the score text
        scoreText.gameObject.SetActive(false);

        // Set buttons interactable
        SetButtonInteractability(cameraButtons[cameraIndex]);
    }

    private void HandleMainCamera()
    {
        secondaryInteractionText.text = "";
        drawingButton.gameObject.SetActive(false);
        // Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        // Show the crosshair
        crosshair.SetActive(true);
        // Show the score text
        scoreText.gameObject.SetActive(true);
        // Show the objective text
        objectiveText.gameObject.SetActive(true);

        // Hide all buttons
        foreach (var button in cameraButtons)
        {
            if (button.gameObject.activeSelf)
                button.gameObject.SetActive(false);
        }
    }
    private void HandleClipboardCamera()
    {
        // Hide Objective text
        objectiveText.gameObject.SetActive(false);
        // Show only first button
        cameraButtons[0].gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true;
        crosshair.SetActive(false);
    }

    private void HandleMeasuringCamera()
    {

        secondaryInteractionText.text = "Press: [RMB] to show mouse";

        // Hide Objective text
        objectiveText.gameObject.SetActive(false);
        // Show only first button
        cameraButtons[0].gameObject.SetActive(true);
        // Check if clipboard has been placed to it's spot
        if (clipboardSpot.transform.childCount > 0)
        {
            drawingButton.gameObject.SetActive(true);
        }

        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = false;
        crosshair.SetActive(false);
    }

    // Method to toggle clipboard camera on and off without changing the active camera
    public void ToggleClipboardCamera()
    {
        toggleClipCam = !toggleClipCam;
        if (toggleClipCam)
        {
            cameras[4].gameObject.SetActive(toggleClipCam);
            // Hide Objective text
            objectiveText.gameObject.SetActive(false);
        }
        else
        {
            cameras[4].gameObject.SetActive(false);
            // Hide Objective text
            objectiveText.gameObject.SetActive(true);
        }
    }

    // Method to check which camera is active
    public bool IsCameraActive(int index)
    {
        return cameras[index].gameObject.activeSelf;
    }

    private void SetButtonInteractability(Button activeButton)
    {
        // Set all but the active button to interactable
        foreach (var button in cameraButtons)
        {
            button.interactable = button != activeButton;
        }
    }
}
