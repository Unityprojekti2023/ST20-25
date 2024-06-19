using TMPro;
using UnityEngine;

public class ItemPlacementSpot : MonoBehaviour, IInteractable
{
    public string requiredItemID = "CutItem"; // The item ID required for placement
    public InventoryManager inventoryManager;
    public TextInformation textInfo;
    public EscapeMenu escapeMenu;
    public MouseControlPanelInteractable mouseControlPanelInteractable;
    public ItemPickup itemPickup;
    public ObjectiveManager objectiveManager;

    public GameObject hiddenItem;
    public GameObject hiddenItem2;
    public GameObject hiddenItemMalfunction;
    public GameObject hiddenItem2Malfunction;

    private void Start()
    {
        hiddenItem.SetActive(false);
        hiddenItem2.SetActive(false);
        hiddenItemMalfunction.SetActive(false);
        hiddenItem2Malfunction.SetActive(false);
    }

    public void Interact()
    {
        //Check if the player has the required item in the inventory
        switch ("CutObject1")
        {
            //When placing cutobject on the table, there is 50% chance for it to be malfunctioned,
            //correct blueprint will appear and you can measure if the object is correct
            case "CutObject1":
                if (Random.value < 0.5f)
                {
                    hiddenItem.SetActive(true);
                    Debug.Log("1");
                }
                else
                {
                    hiddenItemMalfunction.SetActive(true);
                    Debug.Log("1h");
                }
                inventoryManager.RemoveItem(requiredItemID);
                break;

            case "CutObject2":
                if (Random.value < 0.5f)
                {
                    hiddenItem2.SetActive(true);
                    Debug.Log("2");
                }
                else
                {
                    hiddenItem2Malfunction.SetActive(true);
                    Debug.Log("2h");
                }
                inventoryManager.RemoveItem(requiredItemID);
                break;
        }

        textInfo.UpdateText("Item [Cut item 1] removed");
        itemPickup.isAnItemActiveAlready = false;
        mouseControlPanelInteractable.whichCutItemWasLathed = " ";
        //itemPickup.isUncutItemAlreadyInInventory = false;
        objectiveManager.CompleteObjective("Place cut piece on the table");
    }
}
