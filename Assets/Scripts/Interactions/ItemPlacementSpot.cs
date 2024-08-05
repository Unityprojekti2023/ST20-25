using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemPlacementSpot : MonoBehaviour, IInteractable
{
    private CaliperController caliperController;
    public Transform attachemntPoint;

    private void Start()
    {
        caliperController = FindObjectOfType<CaliperController>();
        if (caliperController == null)
        {
            Debug.LogError("CaliperController script not found.");
        }
    }

    public void Interact()
    {
        bool areHandFull = InventoryManager.Instance.handsFull;
        // Get item from the player's hands
        string heldItem = InventoryManager.Instance.GetHeldItemID();

        if (heldItem.Contains("cut") && attachemntPoint.childCount == 0)
        {
            // Remove item from player's hands
            InventoryManager.Instance.RemoveItemFromInventory(heldItem, $"Piece placed down", attachemntPoint);
            ObjectiveManager.Instance.CompleteObjective("Place cut piece on the table");
        }
        else if(heldItem.Contains("Caliper"))
        {
            CameraController.Instance.SwitchToCamera(5);
            caliperController.ToggleCaliperAttachment();
            ObjectiveManager.Instance.CompleteObjective("Use caliper to measure the piece");
        }
        else if(!areHandFull && attachemntPoint.childCount > 0)
        {
            // Add item to player's hands
            InventoryManager.Instance.AddItemToInventory("cut item", $"Piece picked up", attachemntPoint.GetChild(0).gameObject);
        }
        else
        {
            Debug.Log("Couldn't place or switch camera");
        }
    }
}
