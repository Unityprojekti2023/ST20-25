using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlpanelTrigger : MonoBehaviour, IInteractable
{
    public bool isPlayerNearControlpanel = false;
    public CameraController cameraController;

    void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
    }

    public void Interact()
    {
        // Change camera view to control panel camera
        if (cameraController != null)
        {
            cameraController.ControlpanelCameraActive();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNearControlpanel = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNearControlpanel = false;
        }
    }
}

