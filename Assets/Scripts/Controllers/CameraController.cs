using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [Header("Cameras and Buttons")]
    public List<Camera> cameras;
    public Button[] cameraButtons;

    private int activeCameraIndex = 0;

    [Header("References to other gameobjects")]
    public GameObject crosshair;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI objectiveText;


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

    //TODO: check if this script can be simplified

    void Start()
    {
        // Hide all buttons
        foreach (var button in cameraButtons)
        {
            if (button.gameObject.activeSelf)
                button.gameObject.SetActive(false);
        }

        // Hide the secondary interaction text

        // Ensure that only one camera is active at a time
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
    }

    public void SwitchToCamera(int index)
    {
        // Check if the index is out of bounds
        if (index < 0 || index >= cameras.Count)
        {
            Debug.LogError("Invalid camera index");
            return;
        }

        // Deactivate the current camera
        cameras[activeCameraIndex].gameObject.SetActive(false);
        // Activate the new camera
        cameras[index].gameObject.SetActive(true);

        // Update the active camera index
        activeCameraIndex = index;

        if (activeCameraIndex == 0)
        {
            Cursor.lockState = CursorLockMode.Locked;

            // Hide all buttons
            foreach (var button in cameraButtons)
            {
                if (button.gameObject.activeSelf)
                    button.gameObject.SetActive(false);
            }
            crosshair.SetActive(true);
            scoreText.gameObject.SetActive(true);
            objectiveText.gameObject.SetActive(true);
        }
        // Check if active camera is Caliper Camera
        // TODO: Is there smarter way to do this?
        else if (index == 4 || index == 5)
        {
            // Hide Objective text
            objectiveText.gameObject.SetActive(false);

            // Show only first button
            cameraButtons[0].gameObject.SetActive(true);

            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
            Cursor.visible = false;
            crosshair.SetActive(false);
        }
        else
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
            SetButtonInteractability(cameraButtons[index]);
        }
    }

    // Method to toggle clipboard camera on and off without changing the active camera
    public void ToggleClipboardCamera(bool turnOn)
    {
        if (turnOn)
        {
            cameras[5].gameObject.SetActive(turnOn);
            // Hide Objective text
            objectiveText.gameObject.SetActive(false);
        }
        else
        {
            cameras[5].gameObject.SetActive(false);
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
