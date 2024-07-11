using UnityEngine;

public class ClipboardPlacementInteractable : MonoBehaviour, IInteractable
{
    [Header("References to other scripts")]
    public BoxColliderHighlighter boxColliderHighlighter;

    [Header("References to other gameobjects")]
    public GameObject clipboard;

    public void Interact()
    {
        if (InventoryManager.Instance.IsItemInInventory("clipboard"))
        {
            // Make sure clipboard camera is off
            if (CameraController.Instance.IsCameraActive(4))
                CameraController.Instance.ToggleClipboardCamera();
            
            PlaceClipboard();
        }
        else if (transform.childCount > 0)
        {
            CameraController.Instance.SwitchToCamera(4);
        }
        else
        {
            Debug.Log("No clipboard in inventory");
            return;
        }

    }

    void PlaceClipboard()
    {
        // Hide the box collider highlighter
        boxColliderHighlighter.HideHighlighter();

        // Move the clipboard to the attachment point
        clipboard.transform.SetPositionAndRotation(transform.position, transform.rotation);
        // Set the clipboard as a child of the table
        clipboard.transform.parent = transform;

        InventoryManager.Instance.RemoveItemFromInventory("clipboard", "Item [Clipboard] removed from inventory");
        RayInteractor.instance.UpdateInteractionText(transform.name, "Inspect the assigment: [LMB] or [E]");
        ObjectiveManager.Instance.CompleteObjective("Place the clipboard on the worktable");

        // Disable the clipboard's collider from messing with raycasts
        clipboard.transform.GetComponent<BoxCollider>().enabled = false;
    }

}
