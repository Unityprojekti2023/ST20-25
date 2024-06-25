using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [Header("Cameras")]
    public GameObject mainCamera;
    public GameObject controlpanelCamera;
    public GameObject insideCamera;
    public GameObject helpCamera;
    public Camera caliperCamera;
    public GameObject caliper;

    [Header("Other Variables")]
    public bool isMainCamActive = true;
    public GameObject crosshair;

    [Header("References to other scripts")]
    PlayerController playerController;

    [Header("Buttons for camera switching")]
    public Button mainCameraButton;
    public Button panelCameraButton;
    public Button insideCameraButton;
    public Button helpCameraButton;

    private Dictionary<Button, GameObject> buttonCameraMap;

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
        buttonCameraMap = new Dictionary<Button, GameObject>
        {
            { mainCameraButton, mainCamera },
            { panelCameraButton, controlpanelCamera },
            { insideCameraButton, insideCamera },
            { helpCameraButton, helpCamera }
        };

        foreach (var entry in buttonCameraMap)
        {
            entry.Key.onClick.AddListener(() => ActivateCamera(entry.Value, entry.Key));
        }

        SetInitialCameraState();

        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        // TODO: Delete this code block closer to the end of the project
        if (Input.GetKeyDown(KeyCode.H)) ActivateCamera(mainCamera, mainCameraButton);
        if (Input.GetKeyDown(KeyCode.J)) ActivateCamera(controlpanelCamera, panelCameraButton);
        if (Input.GetKeyDown(KeyCode.K)) ActivateCamera(insideCamera, insideCameraButton);
        if (Input.GetKeyDown(KeyCode.L)) ActivateCamera(helpCamera, helpCameraButton);
        if (Input.GetKeyDown(KeyCode.P)) ActivateCaliperCamera(caliperCamera);
    }

    private void SetInitialCameraState()
    {
        mainCamera.SetActive(true);
        controlpanelCamera.SetActive(false);
        insideCamera.SetActive(false);
        helpCamera.SetActive(false);
        caliperCamera.gameObject.SetActive(false);
        crosshair.SetActive(true);

        SetButtonInteractability(mainCameraButton);
    }

    private void ActivateCamera(GameObject camera, Button button)
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

    private void SetButtonInteractability(Button activeButton)
    {
        foreach (var button in buttonCameraMap.Keys)
        {
            button.interactable = button != activeButton;
        }
    }

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
    }

        // Public methods for activating cameras
    public void ActivateMainCamera() => ActivateCamera(mainCamera, mainCameraButton);
    public void ActivateControlpanelCamera() => ActivateCamera(controlpanelCamera, panelCameraButton);
    public void ActivateInsideCamera() => ActivateCamera(insideCamera, insideCameraButton);
    public void ActivateHelpCamera() => ActivateCamera(helpCamera, helpCameraButton);
}
