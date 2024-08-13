using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BlankPickup : MonoBehaviour, IInteractable
{
    [Header("References to other scripts")]
    private TaskManager taskManager;
    public GameObject topItem;

    public string itemID;

    private void Start()
    {
        taskManager = FindObjectOfType<TaskManager>();
    }

    public void Interact()
    {
        // Check if there are still items in the pile and the player's hands are not full
        if (transform.childCount > 0 && !InventoryManager.Instance.handsFull)
        {
            // Get the top item from the pile
            topItem = transform.GetChild(transform.childCount - 1).gameObject;

            if (topItem.activeSelf == false)
            {
                Debug.Log("Top item is inactive, destroying it and getting the next one.");
                // Get the next active item from the pile
                for (int i = transform.childCount - 1; i >= 0; i--)
                {
                    if (transform.GetChild(i).gameObject.activeSelf)
                    {
                        topItem = transform.GetChild(i).gameObject;
                        break;
                    }
                }
            }

            // Instantiate the item in the player's hands
            GameObject item = Instantiate(topItem);
            // Add the item to the player's inventory
            InventoryManager.Instance.AddItemToInventory(itemID, $"Item [{itemID}] picked up", item);
            // Rotate the item to horizontal position
            item.transform.localEulerAngles = new Vector3(0, 90, 0);

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
                ObjectiveManager.Instance.CompleteObjective($"Pick up correct blank");
            }
            else
            {
                // Else deduct points
                ObjectiveManager.Instance.DeductPoints(ScoreValues.LOW);
                return;
            }

        }
        else if (InventoryManager.Instance.IsItemInInventory(itemID))
        {
            if (topItem != null && topItem.activeSelf == false)
            {
                Destroy(InventoryManager.Instance.heldItem);
                topItem.SetActive(true);
                topItem = null;
                InventoryManager.Instance.RemoveItemFromInventory(itemID, $"Item [{itemID}] removed from inventory");
            }
            else if (topItem == null)
            {
                Destroy(InventoryManager.Instance.heldItem);
                // Get the latest hidden item from the pile
                for (int i = transform.childCount - 1; i >= 0; i--)
                {
                    if (transform.GetChild(i).gameObject.activeSelf == false)
                    {
                        topItem = transform.GetChild(i).gameObject;
                        break;
                    }
                }
                topItem.SetActive(true);
                InventoryManager.Instance.RemoveItemFromInventory(itemID, $"Item [{itemID}] removed from inventory");
            }
            else
            {
                Debug.Log("No item to place");
            }
        }
    }
}
