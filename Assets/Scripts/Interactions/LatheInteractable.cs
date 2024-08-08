using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LatheInteractable : MonoBehaviour, IInteractable
{
    [Header("References to other scripts")]
    private LatheController latheController;
    public Transform attachmentPointOfLathe;
    public HelpControlPanelManager helpControlPanelManager;
    private string latheItemID;

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

        // Check if player's hands are full
        if (InventoryManager.Instance.handsFull && attachmentPointOfLathe.childCount == 0)
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

                ObjectiveManager.Instance.CompleteObjective("Place piece into the machine");
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
                helpControlPanelManager.StartSecondPart();
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

    private void AddLathesItemToInventory(string itemID)
    {
        // Add the item to the players inventory
        InventoryManager.Instance.AddItemToInventory(itemID, "Item added to inventory", attachmentPointOfLathe.GetChild(0).gameObject);
        // TODO: This is not rotating cut item to horizontal position only blank items
        InventoryManager.Instance.heldItem.transform.localEulerAngles = new Vector3(0, 90, 0);

        // Update the text information
        RayInteractor.instance.UpdateInteractionText(transform.name, "Place item: [LMB] or [E]");
    }
}
