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

    public GameObject hiddenItem;
    public GameObject hiddenItem2;

    private void Start()
    {
        hiddenItem.SetActive(false);
        hiddenItem2.SetActive(false);
    }

    public void Interact()
    {
        //Making sure game isnt paused
        if(!escapeMenu.isGamePaused) 
        {
            //Check if the player has the required item in the inventory
            if (inventoryManager.HasItem(requiredItemID))
            {
                switch(mouseControlPanelInteractable.whichCutItemWasLathed)
                {
                    case "CutObject1":
                        hiddenItem.SetActive(true);
                        inventoryManager.RemoveItem(requiredItemID);
                    break;

                    case "CutObject2":
                        hiddenItem2.SetActive(true);
                        inventoryManager.RemoveItem(requiredItemID);
                    break;
                }

                textInfo.UpdateText("Item [Cut item 1] removed");
                itemPickup.isAnItemActiveAlready = false;
                mouseControlPanelInteractable.whichCutItemWasLathed = " ";
                //itemPickup.isUncutItemAlreadyInInventory = false;
            }
            else
            {
                textInfo.UpdateText("Cut item not found");
            }
        }
    }
}
