using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [Header("Cameras and Buttons")]
    public Camera[] cameras;
    public Button[] cameraButtons;

    private int activeCameraIndex = 0;

    [Header("References to other gameobjects")]
    public GameObject caliper;
    public GameObject crosshair;
    public Canvas canvas; // Reference to Camera Button Canvas

    [Header("Other Variables")]
    public bool isMainCamActive = true;
    

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
        // Hide camera buttons canvas
        canvas.gameObject.SetActive(false);

        // Ensure that only one camera is active at a time
        for (int i = 0; i < cameras.Length; i++)
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
        if (index < 0 || index >= cameras.Length)
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
            isMainCamActive = true;
            crosshair.SetActive(true);
        }
        else
        {
            // Show camera buttons canvas
            canvas.gameObject.SetActive(true);

            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
            isMainCamActive = false;
            crosshair.SetActive(false);

            // Set buttons interactable
            SetButtonInteractability(cameraButtons[index]);
        }
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
