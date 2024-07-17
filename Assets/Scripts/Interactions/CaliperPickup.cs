using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaliperPickup : MonoBehaviour, IInteractable
{
    public GameObject caliber;
    public Transform originalPosition;

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
            InventoryManager.Instance.AddItemToInventory(caliber.name, "Caliper equipped", caliber);
            //Rotate caliper to horizontal position
            caliber.transform.localEulerAngles = new Vector3(0, 90, 0);
            RayInteractor.instance.UpdateInteractionText(transform.name, "Hold to place caliper: [LMB] or [E]", InteractableType.HandleHoldInteraction);
            //caliber.SetActive(false);

            ObjectiveManager.Instance.CompleteObjective("Equip caliper");
        }
        else if (InventoryManager.Instance.handsFull && InventoryManager.Instance.IsItemInInventory(caliber.name))
        {
            InventoryManager.Instance.RemoveItemFromInventory(caliber.name, "Caliper unequipped", originalPosition);

            caliber.transform.localRotation = Quaternion.identity;
            RayInteractor.instance.UpdateInteractionText(transform.name, "Hold to pickup caliper: [LMB] or [E]", InteractableType.HandleHoldInteraction);
            //caliber.SetActive(true);


            ObjectiveManager.Instance.CompleteObjective("Unequip caliper");
        }
        else
        {
            Debug.Log("Player hands are full.");
        }


    }
}
