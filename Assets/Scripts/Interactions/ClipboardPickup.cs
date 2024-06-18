using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClipboardPickup: MonoBehaviour, IInteractable
{
    [Header("References to other objects")]
    public GameObject clipboard;

    [Header("References to other scripts")]
    public InventoryManager inventoryManager;
    public TextInformation textInfo;
    public ObjectiveManager objectiveManager;

    [Header("Other variables")]
    public Vector3 newPosition;
    public Vector3 newRotation;

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
        // Add the item to the player's inventory
        inventoryManager.AddItem("clipboard");
        textInfo.UpdateText("Item [Clipboard] picked up");
        objectiveManager.CompleteObjective("Pick up the clipboard");
        
        /*clipboard.GetComponent<Collider>().enabled = false; // Disable the clipboard's collider
        // Hide the clipboard's children
        foreach (Transform child in clipboard.transform)
        {
            child.gameObject.SetActive(false);
        }*/
        MoveObject(clipboard);
    }

    private void MoveObject(GameObject gameObject){
        gameObject.transform.position = newPosition;
        gameObject.transform.rotation = Quaternion.Euler(newRotation);
    }
}
