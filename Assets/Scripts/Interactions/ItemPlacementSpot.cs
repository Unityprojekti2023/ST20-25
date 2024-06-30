using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemPlacementSpot : MonoBehaviour, IInteractable
{
    public TextInformation textInfo;
    private RayInteractor rayInteractor;
    public Transform attachemntPoint;

    private void Start()
    {
        rayInteractor = FindObjectOfType<RayInteractor>();
        if (rayInteractor == null)
        {
            Debug.LogError("RayInteractor script not found.");
        }
    }

    public void Interact()
    {
        // Get item from the player's hands
        string heldItem = InventoryManager.Instance.GetHeldItemID();

        if (heldItem.Contains("cut") && attachemntPoint.childCount == 0)
        {
            // Remove item from player's hands
            InventoryManager.Instance.RemoveItem(heldItem, $"Item [{heldItem}] removed", attachemntPoint);

            textInfo.UpdateText("Item [Cut item] removed");
            ObjectiveManager.Instance.CompleteObjective("Place cut piece on the table");
        }
        else
        {
            Debug.Log("Player hands are full.");
        }
    }
}
