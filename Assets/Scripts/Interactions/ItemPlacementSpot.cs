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
            //itemPickup.isUncutItemAlreadyInInventory = false;

            //Show hidden item
            hiddenItem.SetActive(true);

            //Remove item from players inventory
            inventoryManager.RemoveItem(requiredItemID);

            textInfo.UpdateText("Item [Cut item] removed");
        }
        else
        {
            textInfo.UpdateText("Cut item not found");
        }
    }
}
