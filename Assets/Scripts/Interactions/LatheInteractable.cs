using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatheInteractable : MonoBehaviour, IInteractable
{
    [Header("References to other scripts")]
    public DoorController doorController;
    public InventoryManager inventoryManager;
    public MachineScript machineScript;

    public void Interact()
    {
        if (doorController.isDoorOpen && !machineScript.isMachineActive)
        {
            if (inventoryManager.HasItem("UncutItem"))
            {
                if (!machineScript.isUncutObjectInCuttingPosition)
                {
                    inventoryManager.RemoveItem("UncutItem");
                    machineScript.MoveObjectsToCuttingPosition();
                }
                else if (machineScript.isUncutObjectInCuttingPosition)
                {
                    inventoryManager.AddItem("UncutItem");
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
                machineScript.RemoveObjectsFromCuttingPosition();
            }
            else
            {
                inventoryManager.AddItem("UncutItem");
                machineScript.RemoveObjectsFromCuttingPosition();
            }
        }
        else
        {
            Debug.Log("Cannot interact under current conditions.");
        }
    }
}
