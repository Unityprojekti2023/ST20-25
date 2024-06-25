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

    public GameObject caliper;

    [Header("Other Variables")]
    public bool isMainCamActive = true;
    public GameObject crosshair;

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
            Cursor.lockState = CursorLockMode.None;
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

    /*private void SetInitialCameraState()
    {
        mainCamera.SetActive(true);
        controlpanelCamera.SetActive(false);
        insideCamera.SetActive(false);
        helpCamera.SetActive(false);
        caliperCamera.gameObject.SetActive(false);
        crosshair.SetActive(true);

        SetButtonInteractability(mainCameraButton);
    }*/

    /*private void ActivateCamera(GameObject camera, Button button)
    {
        foreach (var cam in buttonCameraMap.Values)
        {
            cam.SetActive(cam == camera);
        }

        mainCamera.SetActive(camera == mainCamera);
        controlpanelCamera.SetActive(camera == controlpanelCamera);
        insideCamera.SetActive(camera == insideCamera);
        helpCamera.SetActive(camera == helpCamera);
        caliperCamera.gameObject.SetActive(false);
        caliper.SetActive(false);
        crosshair.SetActive(camera == mainCamera);

        isMainCamActive = camera == mainCamera;

        SetButtonInteractability(button);
    }
    /*
    public void ActivateCaliperCamera(Camera calibrointiKamera = null)
    {
        if (calibrointiKamera == null)
        {
            calibrointiKamera = caliperCamera;
        }
        caliper.SetActive(true);
        mainCamera.SetActive(false);
        controlpanelCamera.SetActive(false);
        insideCamera.SetActive(false);
        helpCamera.SetActive(false);
        //caliperCamera.SetActive(true);
        crosshair.SetActive(false);
        calibrointiKamera.gameObject.SetActive(true);
        isMainCamActive = false;

        SetButtonInteractability(null);

        // Hide all other camera buttons and only show the main camera button
        mainCameraButton.interactable = true;
        panelCameraButton.interactable = false;
        insideCameraButton.interactable = false;
        helpCameraButton.interactable = false;

        /*if (playerController != null)
        {
            playerController.HidePlayerModel();
        }*/
    //}

    // Public methods for activating cameras
    /*
public void ActivateMainCamera() => ActivateCamera(mainCamera, mainCameraButton);
public void ActivateControlpanelCamera() => ActivateCamera(controlpanelCamera, panelCameraButton);
public void ActivateInsideCamera() => ActivateCamera(insideCamera, insideCameraButton);
public void ActivateHelpCamera() => ActivateCamera(helpCamera, helpCameraButton);*/
}
