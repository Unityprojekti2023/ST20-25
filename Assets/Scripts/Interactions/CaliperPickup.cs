using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaliperPickup : MonoBehaviour, IInteractable
{
    public GameObject caliber;

    void Start()
    {
        if (caliber == null)
        {
            Debug.LogError("Caliber object not found.");
        }
    }
    public void Interact()
    {
        if (!InventoryManager.Instance.handsFull)
        {
            InventoryManager.Instance.AddItemToInventory(caliber.name,"Caliper equipped");
            caliber.SetActive(false);
            RayInteractor.instance.UpdateInteractionText(transform.name, "Hold to place caliper: [LMB] or [E]", InteractableType.HandleHoldInteraction);
            
            ObjectiveManager.Instance.CompleteObjective("Equip caliper");
        }
        else if(InventoryManager.Instance.handsFull && InventoryManager.Instance.HasItem(caliber.name))
        {
            InventoryManager.Instance.RemoveItemFromInventory(caliber.name,"Caliper unequipped");
            caliber.SetActive(true);
            RayInteractor.instance.UpdateInteractionText(transform.name, "Hold to pickup caliper: [LMB] or [E]", InteractableType.HandleHoldInteraction);

            
            ObjectiveManager.Instance.CompleteObjective("Unequip caliper");
        }
        else
        {
            Debug.Log("Player hands are full.");
        }
        

    }
}
