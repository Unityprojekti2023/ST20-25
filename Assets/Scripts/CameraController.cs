using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Cameras")]
    public GameObject mainCamera;
    public GameObject controlpanelCamera;
    public GameObject insideCamera;

    [Header("Other Objects")]
    public GameObject player;

    [Header("Other Variables")]
    public bool isMainCamActive = true;

    [Header("References to other scripts")]
    public ControlpanelTrigger controlpanelTrigger;

    void Start()
    {
        mainCamera.SetActive(true);
        controlpanelCamera.SetActive(false);
        insideCamera.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) 
        {
            mainCameraActive();
        }

        if (Input.GetKeyDown(KeyCode.K) && controlpanelTrigger.isPlayerNearControlpanel == true)
        {
            controlpanelCameraActive();
            isMainCamActive = false;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            insideCameraActive();
            isMainCamActive = false;
        }
    }

    private void mainCameraActive()
    {
        mainCamera.SetActive(true);
        controlpanelCamera.SetActive(false);
        insideCamera.SetActive(false);
    }

    private void controlpanelCameraActive()
    {
        mainCamera.SetActive(false);
        controlpanelCamera.SetActive(true);
        insideCamera.SetActive(false);
    }

    private void insideCameraActive()
    {
        mainCamera.SetActive(false);
        controlpanelCamera.SetActive(false);
        insideCamera.SetActive(true);
    }
}
