using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    public string itemID = "UncutItem";
    public InventoryManager inventoryManager;

    public void Interact()
    {
        // Add the item to the player's inventory
        inventoryManager.AddItem(itemID);

        //This line will hide the pickup item when interacted with.
        //gameObject.SetActive(false);
    }
}
