using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{   public static CameraController Instance;
    [Header("Cameras")]
    public GameObject mainCamera;
    public GameObject controlpanelCamera;
    public GameObject insideCamera;
    public GameObject helpCamera;
    public GameObject caliperCamera;
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

    void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
          Destroy(gameObject);
        }
    }
    void Start()
    {   
        mainCamera.SetActive(true);
        controlpanelCamera.SetActive(false);
        insideCamera.SetActive(false);
        helpCamera.SetActive(false);
        caliperCamera.SetActive(false);

        mainCameraButton.onClick.AddListener(MainCameraActive);
        panelCameraButton.onClick.AddListener(ControlpanelCameraActive);        
        insideCameraButton.onClick.AddListener(InsideCameraActive);
        helpCameraButton.onClick.AddListener(HelpCameraActive);

        playerController = FindObjectOfType<PlayerController>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            MainCameraActive();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            ControlpanelCameraActive();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            InsideCameraActive();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            HelpCameraActive();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            CaliperCameraActive();
        }
    }
    public void MainCameraActive()
    {
        mainCamera.SetActive(true);
        isMainCamActive = true;
        controlpanelCamera.SetActive(false);
        insideCamera.SetActive(false);
        helpCamera.SetActive(false);
        caliperCamera.SetActive(false);
        crosshair.SetActive(true);
        caliper.SetActive(false);

        if (playerController != null)
        {
            playerController.ShowPlayerModel();         // Show player model when switching to the main camera
        }
    }

    public void ControlpanelCameraActive()
    {
        mainCamera.SetActive(false);
        isMainCamActive = false;
        controlpanelCamera.SetActive(true);
        insideCamera.SetActive(false);
        helpCamera.SetActive(false);
        caliperCamera.SetActive(false);
        crosshair.SetActive(false);
        panelCameraButton.interactable = false;
        insideCameraButton.interactable = true;
        helpCameraButton.interactable = true;

        if (playerController != null)
        {
            playerController.HidePlayerModel();         // Hide player model when switching to the control panel camera
        }
    }

    public void InsideCameraActive()
    {
        mainCamera.SetActive(false);
        isMainCamActive = false;
        controlpanelCamera.SetActive(false);
        insideCamera.SetActive(true);
        helpCamera.SetActive(false);
        caliperCamera.SetActive(false);
        crosshair.SetActive(false);
        insideCameraButton.interactable = false;
        panelCameraButton.interactable = true;
        helpCameraButton.interactable = true;

        if (playerController != null)
        {
            playerController.HidePlayerModel();         // Hide player model when switching to the help camera
        }
    }

    public void HelpCameraActive()
    {
        mainCamera.SetActive(false);
        isMainCamActive = false;
        controlpanelCamera.SetActive(false);
        insideCamera.SetActive(false);
        helpCamera.SetActive(true);
        caliperCamera.SetActive(false);
        crosshair.SetActive(false);
        insideCameraButton.interactable = true;
        panelCameraButton.interactable = true;
        helpCameraButton.interactable = false;

        if (playerController != null)
        {
            playerController.HidePlayerModel();         // Hide player model when switching to the inside camera
        }
    }

    public void CaliperCameraActive()
    {
        caliper.SetActive(true);
        mainCamera.SetActive(false);
        isMainCamActive = false;
        controlpanelCamera.SetActive(false);
        insideCamera.SetActive(false);
        helpCamera.SetActive(false);
        caliperCamera.SetActive(true);
        crosshair.SetActive(false);

        insideCameraButton.interactable = false;
        panelCameraButton.interactable = false;
        helpCameraButton.interactable = false;


        if (playerController != null)
        {
            playerController.HidePlayerModel();         // Hide player model when switching to the inside camera
        }
    }
}

