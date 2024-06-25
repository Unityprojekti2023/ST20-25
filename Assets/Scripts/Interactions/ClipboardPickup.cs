using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClipboardPickup : MonoBehaviour, IInteractable
{
    [Header("References to other objects")]
    public GameObject clipboard;
    private GameObject heldClipboard;
    public Renderer clipboardImageSlot;
    public TMPro.TextMeshProUGUI clibBoardTextSlot;

    [Header("Other variables")]
    public Vector3 newPosition;
    public Vector3 newRotation;
    private bool clipboardBeenPickedUp = false;
    public Transform attachmentPoint;
    public Camera heldClipboardCamera;

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
            InventoryManager.Instance.AddItemToInventory("clipboard","Item [Clipboard] picked up");
            ObjectiveManager.Instance.CompleteObjective("Pick up the clipboard");

            // Instantiate the clipboard at the attachment point
            heldClipboard = Instantiate(clipboard, attachmentPoint.position, attachmentPoint.rotation, attachmentPoint);

            // Hide the clipboard's children
            foreach (Transform child in clipboard.transform)
            {
                child.gameObject.SetActive(false);
            }
            MoveObject(clipboard);

            clipboardBeenPickedUp = true;
        }
        else if (InventoryManager.Instance.HasItem("clipboard"))
        {
            // Destroy held clipboard object
            Destroy(heldClipboard);

            foreach (Transform child in clipboard.transform)
            {
                if (!child.gameObject.name.Contains("CMR")) // Check if the child is clipboards camera and enable all other children
                {
                    child.gameObject.SetActive(true);
                }
            }
            InventoryManager.Instance.RemoveItem("clipboard","Item [Clipboard] removed from inventory");
            //ObjectiveManager.Instance.CompleteObjective("Place the clipboard on the table"); //Enable later when the objective is added
        }
        else
        {
            Debug.Log("Trying to activate clipboard camera");
            CameraController.Instance.SwitchToCamera(5);
        }
    }

    private void Update()
    {
        if (InventoryManager.Instance.HasItem("clipboard") && !clipboardBeenPickedUp && Input.GetKeyDown(KeyCode.Mouse1))
        {
            ObjectiveManager.Instance.CompleteObjective("Inspect the drawing");
            CameraController.Instance.SwitchToCamera(5);
        }
        
    }

    private void MoveObject(GameObject gameObject)
    {
        gameObject.transform.position = newPosition;
        gameObject.transform.rotation = Quaternion.Euler(newRotation);
    }
}
