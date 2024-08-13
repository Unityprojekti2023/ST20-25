using UnityEngine;
using UnityEngine.UIElements;

public class ControlpanelTrigger : MonoBehaviour, IInteractable
{
    private bool wasCamera3Active;

    void Start()
    {
        // Initialize the flag to the current state of camera 3
        wasCamera3Active = CameraController.Instance.IsCameraActive(3);
        // Set the initial state of the BoxCollider based on the initial camera state
        this.GetComponent<BoxCollider>().enabled = !wasCamera3Active;
    }

    void Update()
    {
        bool isCamera3Active = CameraController.Instance.IsCameraActive(3);

        // Check if the camera state has changed
        if (isCamera3Active != wasCamera3Active)
        {
            // If the camera state has changed, update the BoxCollider state
            this.GetComponent<BoxCollider>().enabled = !isCamera3Active;
            // Update the flag to the new state
            wasCamera3Active = isCamera3Active;
        }
    }
    public void Interact()
    {
        CameraController.Instance.SwitchToCamera(1);

    }

}

