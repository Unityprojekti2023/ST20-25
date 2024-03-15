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

    [Header("Other Variables")]
    public bool isMainCamActive = true;
    public GameObject crosshair;

    [Header("References to other scripts")]
    PlayerController playerController;

    [Header("Buttons for camera switching")]
    public Button mainCameraButton;
    public Button panelCameraButton;
    public Button insideCameraButton;

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

        mainCameraButton.onClick.AddListener(MainCameraActive);
        panelCameraButton.onClick.AddListener(ControlpanelCameraActive);        
        insideCameraButton.onClick.AddListener(InsideCameraActive);
        
        playerController = FindObjectOfType<PlayerController>();
    }

    public void MainCameraActive()
    {
        mainCamera.SetActive(true);
        isMainCamActive = true;
        controlpanelCamera.SetActive(false);
        insideCamera.SetActive(false);
        crosshair.SetActive(true);

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
        crosshair.SetActive(false);
        panelCameraButton.interactable = false;
        insideCameraButton.interactable = true;

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
        crosshair.SetActive(false);
        insideCameraButton.interactable = false;
        panelCameraButton.interactable = true;

        if (playerController != null)
        {
            playerController.HidePlayerModel();         // Hide player model when switching to the inside camera
        }
    }
}

