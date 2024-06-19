using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatheInteractable : MonoBehaviour, IInteractable
{
    [Header("References to other scripts")]
    public ObjectiveManager objectiveManager;
    public MachineScript machineScript;
    public ItemPickup itemPickup;
    public LatheRightTrigger latheRightTrigger;
    public DrillController drillController;

    public void Interact()
    {
        if (DoorController.Instance.isDoorOpen && !machineScript.isMachineActive)
        {
            string blankInInventory = InventoryManager.Instance.HeldItemID();
            //Check if player has uncut item in inventory
            if (blankInInventory.Contains("aihio") && !machineScript.isUncutObjectInCuttingPosition)
            {
                //Check if there is not an uncut item in cutting position
                if (!machineScript.isUncutObjectInCuttingPosition)
                {
                    InventoryManager.Instance.RemoveItem(blankInInventory, $"Item [{blankInInventory}] removed");
                    itemPickup.isUncutItemAlreadyInInventory = false;

                    machineScript.moveUncutObjectToCuttingPosition();
                    objectiveManager.CompleteObjective("Place piece in place");
                }
                //If there is uncut item in cuttin position remove it and add to players inventory
                else if (machineScript.isUncutObjectInCuttingPosition && !itemPickup.isUncutItemAlreadyInInventory)
                {
                    InventoryManager.Instance.AddItem(blankInInventory, $"Item [{blankInInventory}] picked up");
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
                InventoryManager.Instance.AddItem("CutItem", "Item [Cut item] picked up");

                machineScript.removeUncutObjectFromCuttingPosition();
                machineScript.removeCutObject1FromCuttingPosition();
                machineScript.removeCutObject2FromCuttingPosition();

                latheRightTrigger.counter = 0;
                drillController.activeCounter = 0;
                machineScript.isAnimationComplete = false;

                objectiveManager.CompleteObjective("Pick up cut piece");
            }
            //Check if player does not have uncut item in inventory and there is uncut item in the machine.
            else if (!InventoryManager.Instance.CheckHands() && machineScript.isUncutObjectInCuttingPosition)
            {
                InventoryManager.Instance.AddItem("UncutItem", "Item [Uncut item] picked up");
                InventoryManager.Instance.ToggleHands();

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
