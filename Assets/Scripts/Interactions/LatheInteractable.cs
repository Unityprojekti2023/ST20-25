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
            if (inventoryManager.HasItem("UncutItem"))
            {
                if (!machineScript.isUncutObjectInCuttingPosition)
                {
                    inventoryManager.RemoveItem("UncutItem");

                    textInfo.UpdateText("Item [Uncut item] removed");
                    machineScript.MoveObjectsToCuttingPosition();
                }
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
            else if (machineScript.isAnimationComplete)
            {
                inventoryManager.AddItem("CutItem");

                textInfo.UpdateText("Item [Cut item] picked up");
                machineScript.RemoveObjectsFromCuttingPosition();
            }
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
