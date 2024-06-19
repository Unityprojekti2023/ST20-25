using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    [Header("References to other scripts")]
    private TaskManager taskManager;
    private GameObject topItem;
    private GameObject heldItem;

    public string itemID = "UncutItem";
    public bool isUncutItemAlreadyInInventory = false;
    public bool isAnItemActiveAlready = false;

    [Header("References to other objects")]
    public Transform attachmentPoint;

    private void Start()
    {
        taskManager = FindObjectOfType<TaskManager>();
    }

    public void Interact()
    {
        // Check if there are still items in the pile
        if (transform.childCount > 0 && !InventoryManager.Instance.CheckHands())
        {
            // Check if material is correct
            string currentMaterial = taskManager.GetCurrentMaterialName();

            // Get the top item from the pile
            topItem = transform.GetChild(transform.childCount - 1).gameObject;

            isAnItemActiveAlready = true;

            // Add the item to the player's inventory
            InventoryManager.Instance.AddItem(itemID, $"Item [{itemID}] picked up");
            InventoryManager.Instance.ToggleHands();

            // Instantiate the clipboard at the attachment point
            heldItem = Instantiate(topItem, attachmentPoint.position, attachmentPoint.rotation, attachmentPoint);

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
        else if (InventoryManager.Instance.CheckHands())
        {
            Destroy(heldItem);
            topItem.SetActive(true);
            topItem = null;
            InventoryManager.Instance.ToggleHands();
        }
    }

    // TODO: Method to be called destroy the top item in the pile when placed in the lathe
    public void DestroyTopItem()
    {
        Destroy(topItem);
    }
}
