using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Refactor this script to use the IInteractable interface and remove the unnecessary references to other scripts
public class LatheInteractable : MonoBehaviour, IInteractable
{
    [Header("References to other scripts")]
    public MachineScript machineScript;
    public RawPiecePickup itemPickup;
    public LatheRightTrigger latheRightTrigger;
    public DrillController drillController;
    public Transform attachmentPointOfLathe;
    private string latheItemID;

    public void Interact()
    {
        //Check if player hands are full
        if (InventoryManager.Instance.handsFull)
        {
            //If full, check if the item in the player's hands is a blank and check if attachemnt point is empty
            if (InventoryManager.Instance.GetHeldItemID().Contains("blank") && attachmentPointOfLathe.childCount == 0)
            {
                // Set lahteItemID to the item in the player's hands
                latheItemID = InventoryManager.Instance.GetHeldItemID();

                //Remove item from player's hands
                InventoryManager.Instance.RemoveItem(latheItemID, $"Item [{latheItemID}] removed", attachmentPointOfLathe);
            }
            else
            {
                Debug.Log("Player hands are full.");
            }
        }
        // else if players hands are not full
        else if (!InventoryManager.Instance.handsFull)
        {
            //Check if attachemnt point is not empty
            if (attachmentPointOfLathe.childCount > 0)
            {
                // Add the item to the player's inventory
                InventoryManager.Instance.AddItemToInventory(latheItemID, $"Item [{latheItemID}] picked up",attachmentPointOfLathe.GetChild(0).gameObject);
            }
            else
            {
                Debug.Log("Attachment point is empty.");
            }
        }
        else
        {
            Debug.Log("Player hands are not full.");
        }
    }
}
