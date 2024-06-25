using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatheInteractable : MonoBehaviour, IInteractable
{
    [Header("References to other scripts")]
    public MachineScript machineScript;
    public ItemPickup itemPickup;
    public LatheRightTrigger latheRightTrigger;
    public DrillController drillController;

    public void Interact()
    {
        if (!machineScript.isMachineActive)
        {
            // Get the item ID of the item in the player's hands
            string blankInInventory = InventoryManager.Instance.HeldItemID();
            //Check if item in players hands is blank
            if (blankInInventory.Contains("blank") && !machineScript.isUncutObjectInCuttingPosition)
            {
                //Check if there is not an uncut item in cutting position
                if (!machineScript.isUncutObjectInCuttingPosition)
                {
                    InventoryManager.Instance.RemoveItem(blankInInventory, $"Item [{blankInInventory}] removed");
                    itemPickup.isUncutItemAlreadyInInventory = false;

                    machineScript.moveUncutObjectToCuttingPosition();
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
