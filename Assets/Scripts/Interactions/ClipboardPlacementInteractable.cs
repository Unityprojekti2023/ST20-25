using UnityEngine;

public class ClipboardPlacementInteractable : MonoBehaviour, IInteractable
{
    [Header("References to other scripts")]
    public BoxColliderHighlighter boxColliderHighlighter;

    [Header("References to other gameobjects")]
    public GameObject clipboard;

    void Start()
    {
        if (clipboard == null)
        {
            Debug.LogError("Clipboard reference not set in ClipboardPlacementInteractable!");
            return;
        }
        if (boxColliderHighlighter == null)
        {
            Debug.LogError("BoxColliderHighlighter reference not set in ClipboardPlacementInteractable!");
            return;
        }

        clipboard.SetActive(true);
    }

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
        // Hide the box collider highlighter
        boxColliderHighlighter.HideHighlighter();
        
        // Move the clipboard to the attachment point
        clipboard.transform.SetPositionAndRotation(transform.position, transform.rotation);
        // Set the clipboard as a child of the table
        clipboard.transform.parent = transform;

        InventoryManager.Instance.RemoveItemFromInventory("clipboard", "Item [Clipboard] removed from inventory");
        //ObjectiveManager.Instance.CompleteObjective("Place the clipboard on the table"); // TODO: Enable later when the objective is added

        // Disable the clipboard's collider from messing with raycasts
        transform.GetComponent<BoxCollider>().enabled = false;
    }

}
