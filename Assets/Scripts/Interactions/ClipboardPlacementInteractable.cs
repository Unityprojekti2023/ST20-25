using System.Collections;
using System.Collections.Generic;
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
        clipboard.transform.position = transform.position;
        clipboard.transform.rotation = transform.rotation;
        clipboard.transform.parent = transform;

        foreach (Transform child in clipboard.transform)
        {
            if (!child.gameObject.name.Contains("CMR")) // Check if the child is clipboards camera and enable all other children
            {
                child.gameObject.SetActive(true);
            }
        }
        Debug.Log("Clipboard removed from inventory");
        InventoryManager.Instance.RemoveItem("clipboard", "Item [Clipboard] removed from inventory");
        //ObjectiveManager.Instance.CompleteObjective("Place the clipboard on the table"); //Enable later when the objective is added

        transform.GetComponent<BoxCollider>().enabled = false;
    }

}
