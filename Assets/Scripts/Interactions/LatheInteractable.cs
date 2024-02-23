using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatheInteractable : MonoBehaviour, IInteractable
{
    [Header("References to other scripts")]
    public DoorController doorController;
    public InventoryManager inventoryManager;
    public ObjectiveManager objectiveManager;
    public MachineScript machineScript;
    public TextInformation textInfo;
    public ItemPickup itemPickup;
    public EscapeMenu escapeMenu;
    public LatheRightTrigger latheRightTrigger;
    public DrillController drillController;

    public void Interact()
    {
        if (!escapeMenu.isGamePaused) 
        {
            if (doorController.isDoorOpen && !machineScript.isMachineActive)
            {
                //Check if player has uncut item in inventory
                if (inventoryManager.HasItem("UncutItem"))
                {
                    //Check if there is not an uncut item in cutting position
                    if (!machineScript.isUncutObjectInCuttingPosition)
                    {
                        inventoryManager.RemoveItem("UncutItem");
                        itemPickup.isUncutItemAlreadyInInventory = false;

                        textInfo.UpdateText("Item [Uncut item] removed");
                        machineScript.moveUncutObjectToCuttingPosition();
                    }
                    //If there is uncut item in cuttin position remove it and add to players inventory
                    else if (machineScript.isUncutObjectInCuttingPosition && !itemPickup.isUncutItemAlreadyInInventory)
                    {
                        inventoryManager.AddItem("UncutItem");
                        itemPickup.isUncutItemAlreadyInInventory = true;

                        textInfo.UpdateText("Item [Uncut item] picked up");
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
                    inventoryManager.AddItem("CutItem");

                    textInfo.UpdateText("Item [Cut item] picked up");
                    machineScript.removeUncutObjectFromCuttingPosition();
                    machineScript.removeCutObject1FromCuttingPosition();
                    machineScript.removeCutObject2FromCuttingPosition();
                    
                    latheRightTrigger.counter = 0;
                    drillController.activeCounter = 0;
                    machineScript.isAnimationComplete = false;
                }
                //Check if player does not have uncut item in inventory and there is uncut item in the machine.
                else if (!inventoryManager.HasItem("UncutItem") && machineScript.isUncutObjectInCuttingPosition && !itemPickup.isUncutItemAlreadyInInventory)
                {
                    inventoryManager.AddItem("UncutItem");
                    itemPickup.isUncutItemAlreadyInInventory = true;

                    textInfo.UpdateText("Item [Uncut item] picked up");
                    machineScript.removeUncutObjectFromCuttingPosition();
                    machineScript.removeCutObject1FromCuttingPosition();
                    machineScript.removeCutObject2FromCuttingPosition();
                }
                else
                {
                    textInfo.UpdateText("No items on player");
                }
            }
            else
            {
                textInfo.UpdateText("No items in inventory");
                Debug.Log("Cannot interact under current conditions.");
            }
        }
    }
}
