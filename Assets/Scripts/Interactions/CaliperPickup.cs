using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaliperPickup : MonoBehaviour, IInteractable
{
    private RayInteractor rayInteractor;
    public GameObject caliber;

    void Start()
    {
        rayInteractor = FindObjectOfType<RayInteractor>();
        if (rayInteractor == null)
        {
            Debug.LogError("RayInteractor script not found.");
        }
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
            rayInteractor.UpdateInteractionText(transform.name, "Hold to place caliper: [LMB] or [E]", InteractableType.HandleHoldInteraction);
            
            ObjectiveManager.Instance.CompleteObjective("Equip caliper");
        }
        else if(InventoryManager.Instance.handsFull && InventoryManager.Instance.HasItem(caliber.name))
        {
            InventoryManager.Instance.RemoveItemFromInventory(caliber.name,"Caliper unequipped");
            caliber.SetActive(true);
            rayInteractor.UpdateInteractionText(transform.name, "Hold to pickup caliper: [LMB] or [E]", InteractableType.HandleHoldInteraction);

            
            ObjectiveManager.Instance.CompleteObjective("Unequip caliper");
        }
        else
        {
            Debug.Log("Player hands are full.");
        }
        

    }
}
