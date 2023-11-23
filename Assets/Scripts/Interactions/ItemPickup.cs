using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    public string itemID = "UncutItem";
    public InventoryManager inventoryManager;

    public TextInformation textInfo;

    public void Interact()
    {
        // Check if there are still items in the pile
        if (transform.childCount > 0)
        {
            // Get the topmost item in the pile
            GameObject topItem = transform.GetChild(transform.childCount - 1).gameObject;

            // Add the item to the player's inventory
            inventoryManager.AddItem(itemID);
            textInfo.UpdateText("Item [Uncut item] picked up");

            // Hide the top item
            topItem.SetActive(false);

            // Optionally, you can destroy the item instead of just hiding it
            Destroy(topItem);
        }
    }
}
