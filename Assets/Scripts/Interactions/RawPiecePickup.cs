using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RawPiecePickup : MonoBehaviour, IInteractable
{
    [Header("References to other scripts")]
    private TaskManager taskManager;
    private GameObject topItem;

    public string itemID;

    private void Start()
    {
        taskManager = FindObjectOfType<TaskManager>();
    }

    public void Interact()
    {
        // Get the top item from the pile
        topItem = transform.GetChild(transform.childCount - 1).gameObject;

        // Check if there are still items in the pile and the player's hands are not full
        if (transform.childCount > 0 && !InventoryManager.Instance.handsFull)
        {
            // Instantiate the item in the player's hands
            GameObject item = Instantiate(topItem);
            // Add the item to the player's inventory
            InventoryManager.Instance.AddItemToInventory(itemID, $"Item [{itemID}] picked up",item);

            // Hide the topItem
            topItem.SetActive(false);

            // TODO: Is there better logic for checking if the picked blank was correct material? By name containing Directory Key of the material from taskManager?
            Renderer itemRenderer = topItem.GetComponent<Renderer>();
            {
                _ = itemRenderer.material.name;
            }

            
            // Check if material is correct
            string currentMaterial = taskManager.GetCurrentMaterialName();

            // Check if the item is the correct material
            if (itemRenderer != null && itemRenderer.material.name.Contains(taskManager.GetMaterialType(currentMaterial)))
            {
                // If the item is the correct material, complete the objective
                ObjectiveManager.Instance.CompleteObjective($"Pick up correct raw piece");
            }
            else
            {
                // Else deduct points
                ObjectiveManager.Instance.DeductPoints(50); //TODO: Anyway to make point deduction more dynamic and easy from ObjectiveManager? Ie. Have low, medium and high point deduction methods?
                return;
            }

        }
        else if (InventoryManager.Instance.handsFull && InventoryManager.Instance.HasItem(itemID))
        {
            Destroy(InventoryManager.Instance.heldItem);
            if (topItem != null && topItem.activeSelf == false)
            {
                topItem.SetActive(true);
                topItem = null;

                InventoryManager.Instance.RemoveItem(itemID, $"Item [{itemID}] removed from inventory");
            }
            else
            {
                Debug.Log("No item to place");
            }
        }
    }
}
