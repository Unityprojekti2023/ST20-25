using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClipboardPickup : MonoBehaviour, IInteractable
{
    [Header("References to other objects")]
    public GameObject clipboard;
    private GameObject heldClipboard;

    [Header("References to other scripts")]
    public InventoryManager inventoryManager;
    public TextInformation textInfo;
    public ObjectiveManager objectiveManager;
    public CameraController cameraController;

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
        if (!inventoryManager.HasItem("clipboard") && !clipboardBeenPickedUp)
        {
            // Add the item to the player's inventory
            inventoryManager.AddItem("clipboard");
            textInfo.UpdateText("Item [Clipboard] picked up");
            objectiveManager.CompleteObjective("Pick up the clipboard");

            // Instantiate the clipboard at the attachment point
            heldClipboard = Instantiate(clipboard, attachmentPoint.position, attachmentPoint.rotation, attachmentPoint);
            //heldClipboardCamera =  heldClipboard.GetComponentInChildren<Camera>();

            if(heldClipboardCamera != null)
            {
                Debug.Log("Clipboard has camera");
            }
            else
            {
                Debug.Log("Clipboard does not have camera");
            }


            // Adjust position and rotation of the clipboard if needed
            //heldClipboard.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

            // Hide the clipboard's children
            foreach (Transform child in clipboard.transform)
            {
                child.gameObject.SetActive(false);
            }
            MoveObject(clipboard);

            clipboardBeenPickedUp = true;
        }
        else if (inventoryManager.HasItem("clipboard"))
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
            inventoryManager.RemoveItem("clipboard");
            textInfo.UpdateText("Item [Clipboard] removed from inventory");
            //objectiveManager.CompleteObjective("Place the clipboard on the table"); //Enable later when the objective is added
        }
        else
        {
            Debug.Log("Trying to activate clipboard camera");
            cameraController.ActivateCaliperCamera(heldClipboardCamera);
        }
    }

    private void Update()
    {
        if (inventoryManager.HasItem("clipboard") && !clipboardBeenPickedUp && Input.GetKeyDown(KeyCode.Mouse1))
        {
            cameraController.ActivateCaliperCamera(heldClipboardCamera);
        }
    }

    private void MoveObject(GameObject gameObject)
    {
        gameObject.transform.position = newPosition;
        gameObject.transform.rotation = Quaternion.Euler(newRotation);
    }
}
