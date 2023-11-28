using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatheInteractable : MonoBehaviour, IInteractable
{
    [Header("References to other scripts")]
    public DoorController doorController;
    public InventoryManager inventoryManager;
    public MachineScript machineScript;
    public TextInformation textInfo;

    public void Interact()
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

                    textInfo.UpdateText("Item [Uncut item] removed");
                    machineScript.MoveObjectsToCuttingPosition();
                }
                //If there is uncut item in cuttin position remove it and add to players inventory
                else if (machineScript.isUncutObjectInCuttingPosition)
                {
                    inventoryManager.AddItem("UncutItem");

                    textInfo.UpdateText("Item [Uncut item] picked up");
                    machineScript.RemoveObjectsFromCuttingPosition();
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
                machineScript.RemoveObjectsFromCuttingPosition();
            }
            //Check if player does not have uncut item in inventory and there is uncut item in the machine.
            else if (!inventoryManager.HasItem("UncutItem") && machineScript.isUncutObjectInCuttingPosition)
            {
                inventoryManager.AddItem("UncutItem");

                textInfo.UpdateText("Item [Uncut item] picked up");
                machineScript.RemoveObjectsFromCuttingPosition();
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
