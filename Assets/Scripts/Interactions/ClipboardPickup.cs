using TMPro;
using UnityEngine;

public class ClipboardPickup : MonoBehaviour, IInteractable
{
    [Header("References to other scripts")]
    public BoxColliderHighlighter boxColliderHighlighter;

    [Header("References to other objects")]
    public GameObject clipboard;
    public Renderer clipboardImageSlot;
    public Transform attachmentPoint;
    public TextMeshProUGUI clibBoardTextSlot;
    private bool inspecting = false;

    public void Interact()
    {
        if (!InventoryManager.Instance.handsFull)
        {
            // Add the item to the player's inventory
            InventoryManager.Instance.AddItemToInventory("clipboard", "Item [Clipboard] picked up", "Inspect : RMB");
            ObjectiveManager.Instance.CompleteObjective("Pick up the clipboard");

            // Move the clipboard to the attachment point
            clipboard.transform.SetPositionAndRotation(attachmentPoint.position, attachmentPoint.rotation);
            clipboard.transform.parent = attachmentPoint;

            // Show the box collider highlighter
            boxColliderHighlighter.ShowHighlighter();
        }
        else
            return;
    }

    private void Update()
    {
        if (InventoryManager.Instance.IsItemInInventory("clipboard") && Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (!inspecting)
            {
                inspecting = true;
                // Switch to clipboard camera on
                ObjectiveManager.Instance.CompleteObjective("Inspect the drawing");

                CameraController.Instance.ToggleClipboardCamera(true);
            }
            else if (inspecting)
            {
                inspecting = false;
                // Switch to clipboard camera off
                CameraController.Instance.ToggleClipboardCamera(false);
            }
            else
                return;
        }
        else
            return;
    }
}
