using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//TODO: Refactor this script to use the IInteractable interface and remove the unnecessary references to other scripts
public class LatheInteractable : MonoBehaviour, IInteractable
{
    [Header("References to other scripts")]
    private LatheController latheController;
    public Transform attachmentPointOfLathe;
    private string latheItemID;

    //TODO: Add function to pick cut item from lathe
    void Start()
    {
        // Find the LatheController script in the scene
        latheController = FindObjectOfType<LatheController>();
        if (latheController == null)
        {
            Debug.LogError("LatheController script not found.");
        }
    }

    public void Interact()
    {
        // TODO: Implement logic to stop player from picking up the item if the Lathe is running

        // Check if player's hands are full
        if (InventoryManager.Instance.handsFull)
        {
            //If full, check if the item in the player's hands is a blank and check if attachemnt point is empty
            string heldItem = InventoryManager.Instance.GetHeldItemID();

            if (heldItem.Contains("blank") || heldItem.Contains("cut") && attachmentPointOfLathe.childCount == 0)
            {
                // Set lahteItemID to the item in the player's hands
                latheItemID = InventoryManager.Instance.GetHeldItemID();

                // Remove item from player's hands
                InventoryManager.Instance.RemoveItemFromInventory(latheItemID, $"Item [{latheItemID}] removed", attachmentPointOfLathe);
                // Update the text information
                RayInteractor.instance.UpdateInteractionText(transform.name, "Pickup item: [LMB] or [E]");

                ObjectiveManager.Instance.CompleteObjective("Place piece in place");
            }
            else
            {
                Debug.Log("Player hands are full.");
            }
        }
        else if (!InventoryManager.Instance.handsFull)
        {
            //Check if attachemnt point is not empty
            if (attachmentPointOfLathe.childCount > 0 && !attachmentPointOfLathe.GetChild(0).name.Contains("blank"))
            {
                latheController.HideCutItem();

                AddLathesItemToInventory("cut item");
                
                ObjectiveManager.Instance.CompleteObjective("Pick up cut piece");
            }
            else if (attachmentPointOfLathe.childCount > 0 && attachmentPointOfLathe.GetChild(0).name.Contains("blank"))
            {
                AddLathesItemToInventory(latheItemID);
            }
            else
            {
                Debug.Log("Attachment point is empty.");
            }
        }
        else
        {
            Debug.Log("Player hands are not full and there are no item in the lathe.");
        }
    }

    private void AddLathesItemToInventory(string itemID = "item")
    {
        // Add the item to the players inventory
        InventoryManager.Instance.AddItemToInventory(itemID, "Item added to inventory", attachmentPointOfLathe.GetChild(0).gameObject);

        // Update the text information
        RayInteractor.instance.UpdateInteractionText(transform.name, "Place item: [LMB] or [E]");
    }

    // Set latheItemID to cut items
    public void SetLatheItemID(string itemID)
    {
        latheItemID = itemID;
    }
}
