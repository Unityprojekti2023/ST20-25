using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    [Header("References to other scripts")]
    private TaskManager taskManager;
    private GameObject topItem;
    private GameObject heldItem;

    public string itemID;
    public bool isUncutItemAlreadyInInventory = false;

    [Header("References to other objects")]
    public Transform attachmentPoint;

    private void Start()
    {
        taskManager = FindObjectOfType<TaskManager>();
    }

    public void Interact()
    {

        // Get the top item from the pile
        topItem = transform.GetChild(transform.childCount - 1).gameObject;

        // Check if there are still items in the pile
        if (transform.childCount > 0 && !InventoryManager.Instance.CheckIfHandsFull())
        {
            // Check if material is correct
            string currentMaterial = taskManager.GetCurrentMaterialName();

            // Add the item to the player's inventory
            InventoryManager.Instance.AddItemToInventory(itemID, $"Item [{itemID}] picked up");

            // Hide the topItem
            topItem.SetActive(false);

            Renderer itemRenderer = topItem.GetComponent<Renderer>();
            {
                _ = itemRenderer.material.name;
            }

            if (itemRenderer != null && itemRenderer.material.name.Contains(taskManager.GetMaterialType(currentMaterial)))
            {
                ObjectiveManager.Instance.CompleteObjective($"Pick up correct raw piece");
            }
            else
            {
                ObjectiveManager.Instance.DeductPoints(50);
                return;
            }

        }

        // Place item pack into the pile if hands are full
        else if (InventoryManager.Instance.CheckIfHandsFull() && InventoryManager.Instance.HasItem(itemID))
        {
            Destroy(heldItem);
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

    // TODO: Method to be called destroy the top item in the pile when placed in the lathe
    public void DestroyTopItem()
    {
        Destroy(topItem);
    }
}
