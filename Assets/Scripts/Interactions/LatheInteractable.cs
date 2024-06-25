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
    private string blankInInventory;

    public void Interact()
    {
        if (!machineScript.isMachineActive)
        {
            Debug.Log("Machine is not active");
            if (InventoryManager.Instance.handsFull)
            {
                Debug.Log("Player hands are not full");
                // Get the item ID of the item in the player's hands
                blankInInventory = InventoryManager.Instance.HeldItemID();
            }
            else
                return;

            //Check if item in players hands is blank
            if (blankInInventory.Contains("blank") && !machineScript.isUncutObjectInCuttingPosition)
            {
                Debug.Log("Item in players hands is blank");
                //Check if there is not an uncut item in cutting position
                if (!machineScript.isUncutObjectInCuttingPosition)
                {
                    InventoryManager.Instance.RemoveItem(blankInInventory, $"Item [{blankInInventory}] removed");
                    InventoryManager.Instance.handsFull = false;


                    //Move the item to the cutting position from players hands
                    itemPickup.heldItem.transform.SetPositionAndRotation(attachmentPointOfLathe.position, attachmentPointOfLathe.rotation);
                    itemPickup.heldItem.transform.parent = attachmentPointOfLathe;
                    //Set the uncut item to active
                    itemPickup.heldItem.SetActive(true);

                    //machineScript.moveUncutObjectToCuttingPosition();
                    ObjectiveManager.Instance.CompleteObjective("Place piece in place");
                }
                //If there is uncut item in cuttin position remove it and add to players inventory
                else if (machineScript.isUncutObjectInCuttingPosition && !itemPickup.isUncutItemAlreadyInInventory)
                {
                    InventoryManager.Instance.AddItemToInventory(blankInInventory, $"Item [{blankInInventory}] picked up");
                    itemPickup.isUncutItemAlreadyInInventory = true;

                    machineScript.removeUncutObjectFromCuttingPosition();
                    machineScript.removeCutObject1FromCuttingPosition();
                    machineScript.removeCutObject2FromCuttingPosition();
                }
                else
                {
                    Debug.Log("Something went wrong");
                }
            }
            //Check if machine has run it's cutting animation
            else if (machineScript.isAnimationComplete)
            {
                //Add cut item to player inventory
                InventoryManager.Instance.AddItemToInventory("CutItem", "Item [Cut item] picked up");

                machineScript.removeUncutObjectFromCuttingPosition();
                machineScript.removeCutObject1FromCuttingPosition();
                machineScript.removeCutObject2FromCuttingPosition();

                latheRightTrigger.counter = 0;
                drillController.activeCounter = 0;
                machineScript.isAnimationComplete = false;

                ObjectiveManager.Instance.CompleteObjective("Pick up cut piece");
            }
            //Check if player does not have uncut item in inventory and there is uncut item in the machine.
            else if (!InventoryManager.Instance.CheckIfHandsFull() && machineScript.isUncutObjectInCuttingPosition)
            {
                InventoryManager.Instance.AddItemToInventory("UncutItem", "Item [Uncut item] picked up");

                machineScript.removeUncutObjectFromCuttingPosition();
                machineScript.removeCutObject1FromCuttingPosition();
                machineScript.removeCutObject2FromCuttingPosition();
            }
            else
            {
                Debug.Log("Cannot interact under current conditions.");
            }
        }
        else
        {
            Debug.Log("Cannot interact under current conditions.");
        }
    }
}
