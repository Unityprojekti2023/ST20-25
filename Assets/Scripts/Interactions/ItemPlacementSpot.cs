using TMPro;
using UnityEngine;
public class ItemPlacementSpot : MonoBehaviour, IInteractable
{
    public string requiredItemID = "CutItem"; // The item ID required for placement
    public InventoryManager inventoryManager;
    public TextInformation textInfo;

    public GameObject hiddenItem;

    private void Start()
    {
        hiddenItem.SetActive(false);
    }

    public void Interact()
    {
        // Check if the player has the required item in the inventory
        if (inventoryManager.HasItem(requiredItemID))
        {
            // Activate the hidden item
            hiddenItem.SetActive(true);

            // Optionally, you can perform additional actions when placing the item, such as removing it from the player's inventory
            inventoryManager.RemoveItem(requiredItemID);

            textInfo.UpdateText("Item [Cut item] removed");

            Debug.Log("Item placed successfully!");
        }
        else
        {
            textInfo.UpdateText("Cut item not found");
            Debug.Log("Player does not have the required item.");
        }
    }
}
