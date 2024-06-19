using TMPro;
using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    [Header("References to other scripts")]
    public InventoryManager inventoryManager;
    public EscapeMenu escapeMenu;
    public TextInformation textInfo;
    public ObjectiveManager objectiveManager;
    private TaskManager taskManager;

    public string itemID = "UncutItem";
    public bool isUncutItemAlreadyInInventory = false;
    public bool isAnItemActiveAlready = false;

    private void Start()
    {
        taskManager = FindObjectOfType<TaskManager>();
    }

    public void Interact()
    {
        // Check if there are still items in the pile
        if (transform.childCount > 0 && !isUncutItemAlreadyInInventory && !escapeMenu.isGamePaused && !isAnItemActiveAlready)
        {
            // Check if material is correct
            string currentMaterial = taskManager.GetCurrentMaterialName();
            Debug.Log($"Picked up item should have been: {currentMaterial}");


            isAnItemActiveAlready = true;
            isUncutItemAlreadyInInventory = true;

            // Get the topmost item in the pile
            GameObject topItem = transform.GetChild(transform.childCount - 1).gameObject;

            Renderer itemRenderer = topItem.GetComponent<Renderer>();
            {
                string materialName = itemRenderer.material.name;
                Debug.Log($"Material name: {materialName}");
            }

            if (itemRenderer != null && itemRenderer.material.name.Contains(taskManager.GetMaterialType(currentMaterial)))
            {

                textInfo.UpdateText($"Item [{itemID}] picked up it was the correct material");
            }
            else
            {
                textInfo.UpdateText($"Item [{itemID}] picked up it was the wrong material");
                return;
            }


            // Add the item to the player's inventory
            inventoryManager.AddItem(itemID);
            //textInfo.UpdateText($"Item [{itemID}] picked up");
            objectiveManager.CompleteObjective($"Pick up {itemID} piece");


            // Hide the top item
            topItem.SetActive(false);

            // Optionally, you can destroy the item instead of just hiding it
            Destroy(topItem);
        }
    }
}
