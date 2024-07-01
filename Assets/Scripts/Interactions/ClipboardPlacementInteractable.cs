using UnityEngine;

public class ClipboardPlacementInteractable : MonoBehaviour, IInteractable
{
    public GameObject clipboard;
    public void Interact()
    {
        if (InventoryManager.Instance.HasItem("clipboard"))
        {
            PlaceClipboard();
        }
        else
        {
            Debug.Log("No clipboard in inventory");
            return;
        }

    }

    void PlaceClipboard()
    {
        // Move the clipboard to the attachment point
        clipboard.transform.SetPositionAndRotation(transform.position, transform.rotation);
        // Set the clipboard as a child of the table
        clipboard.transform.parent = transform;

        // Enable all children of the clipboard except the camera
        foreach (Transform child in clipboard.transform)
        {
            if (!child.gameObject.name.Contains("Camera")) // Check if the child is clipboards camera and enable all other children
            {
                child.gameObject.SetActive(true);
            }
        }
        InventoryManager.Instance.RemoveItemFromInventory("clipboard", "Item [Clipboard] removed from inventory");
        //ObjectiveManager.Instance.CompleteObjective("Place the clipboard on the table"); // TODO: Enable later when the objective is added

        // Disable the clipboard's collider from messing with raycasts
        transform.GetComponent<BoxCollider>().enabled = false;
    }

}
