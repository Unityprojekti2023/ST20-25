using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipboardPickup: MonoBehaviour, IInteractable
{
    [Header("References to other scripts")]
    public InventoryManager inventoryManager;
    public TextInformation textInfo;
    public ObjectiveManager objectiveManager;


    public void Interact()
    {
            // Add the item to the player's inventory
            inventoryManager.AddItem("clipboard");
            textInfo.UpdateText("Item [Clipboard] picked up");
            objectiveManager.CompleteObjective("Pick up the clipboard");
    }
}
