using UnityEngine;

public class ClipboardPickup : MonoBehaviour, IInteractable
{
    [Header("References to other objects")]
    public GameObject clipboard;
    public Renderer clipboardImageSlot;
    public TMPro.TextMeshProUGUI clibBoardTextSlot;

    [Header("Other variables")]
    private bool clipboardBeenPickedUp = false;
    public Transform attachmentPoint;
    public Camera heldClipboardCamera;
    private bool inspecting = false;

    void Start()
    {

        if (clipboard == null)
        {
            Debug.LogError("Clipboard reference not set in ClipboardPickup!");
            return;
        }

        clipboard.SetActive(true);
    }

    public void Interact()
    {
        if (!InventoryManager.Instance.HasItem("clipboard") && !clipboardBeenPickedUp)
        {
            // Add the item to the player's inventory
            InventoryManager.Instance.AddItemToInventory("clipboard", "Item [Clipboard] picked up");
            ObjectiveManager.Instance.CompleteObjective("Pick up the clipboard");

            // Move the clipboard to the attachment point
            clipboard.transform.SetPositionAndRotation(attachmentPoint.position, attachmentPoint.rotation);
            clipboard.transform.parent = attachmentPoint;

            clipboardBeenPickedUp = true;
        }
        else
        {
            Debug.Log("Trying to activate clipboard camera");
            CameraController.Instance.SwitchToCamera(5);
        }
    }

    private void Update()
    {
        if (InventoryManager.Instance.HasItem("clipboard") && !inspecting && Input.GetKeyDown(KeyCode.Mouse1))
        {
            inspecting = true;
            ObjectiveManager.Instance.CompleteObjective("Inspect the drawing");
            // Switch to clipboard camera
            CameraController.Instance.AddCameraAndSwitchToIt(heldClipboardCamera);
        }
        // Switch back to player camera
        else if (InventoryManager.Instance.HasItem("clipboard") && inspecting && Input.GetKeyDown(KeyCode.Mouse1))
        {
            inspecting = false;
            CameraController.Instance.RemoveLatestCamera();
        }
        else
            return;
    }
}
