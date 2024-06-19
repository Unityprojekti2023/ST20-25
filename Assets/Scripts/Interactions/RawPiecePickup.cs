using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    [Header("References to other scripts")]
    public InventoryManager inventoryManager;
    public EscapeMenu escapeMenu;
    public TextInformation textInfo;
    public ObjectiveManager objectiveManager;

    public string itemID = "UncutItem";
    public bool isUncutItemAlreadyInInventory = false;
    public bool isAnItemActiveAlready = false;

    public void Interact()
    {
        // Check if there are still items in the pile
        if (transform.childCount > 0 && !isUncutItemAlreadyInInventory && !escapeMenu.isGamePaused && !isAnItemActiveAlready)
        {
            isAnItemActiveAlready = true;
            isUncutItemAlreadyInInventory = true;

            // Get the topmost item in the pile
            GameObject topItem = transform.GetChild(transform.childCount - 1).gameObject;

            // Add the item to the player's inventory
            inventoryManager.AddItem(itemID);
            textInfo.UpdateText($"Item [{itemID}] picked up");
            objectiveManager.CompleteObjective($"Pick up {itemID} piece");


            // Hide the top item
            topItem.SetActive(false);

            // Optionally, you can destroy the item instead of just hiding it
            Destroy(topItem);
        }
    }
}
