using UnityEngine;

public class ControlpanelTrigger : MonoBehaviour, IInteractable
{
    CameraController cameraController;
    public EscapeMenu escapeMenu;

    void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
    }

    public void Interact()
    {
        // Change camera view to control panel camera
        if (cameraController != null && !escapeMenu.isGamePaused)
        {
            CameraController.Instance.SwitchToCamera(1);
        }
    }
}

