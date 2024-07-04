using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelInteraction : MonoBehaviour, IInteractable
{
    public GameObject shovel;
    Vector3 shovelPosition;
    Vector3 shovelRotation;

    void Start()
    {
        // Hide child of shovel named Scraps
        shovel.transform.Find("Scraps").gameObject.SetActive(false);
    }

    public void Interact()
    {
        if (!InventoryManager.Instance.handsFull)
        {
            // Save shovels position and rotation before picking it up
            shovelPosition = shovel.transform.position;
            shovelRotation = shovel.transform.eulerAngles;

            InventoryManager.Instance.AddItemToInventory("Shovel", "Shovel picked up", shovel);

            ObjectiveManager.Instance.CompleteObjective("Equip shovel");
        }
        else if (InventoryManager.Instance.handsFull && InventoryManager.Instance.HasItem("Shovel"))
        {
            InventoryManager.Instance.RemoveItemFromInventory("Shovel", "Shovel placed back", transform);

            // Apply saved position and rotation to shovel
            shovel.transform.position = shovelPosition;
            shovel.transform.eulerAngles = shovelRotation;

            ObjectiveManager.Instance.CompleteObjective("Unequip shovel");
        }

    }
}
