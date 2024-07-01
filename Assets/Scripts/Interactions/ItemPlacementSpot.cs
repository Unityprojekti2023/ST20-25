using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemPlacementSpot : MonoBehaviour, IInteractable
{
    private CaliperController caliperController;
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
        caliperController = FindObjectOfType<CaliperController>();
        if (caliperController == null)
        {
            Debug.LogError("CaliperController script not found.");
        }
    }

    public void Interact()
    {
        // Get item from the player's hands
        string heldItem = InventoryManager.Instance.GetHeldItemID();

        if (heldItem.Contains("cut") && attachemntPoint.childCount == 0)
        {
            // Remove item from player's hands
            InventoryManager.Instance.RemoveItemFromInventory(heldItem, $"Item [{heldItem}] removed", attachemntPoint);
            ObjectiveManager.Instance.CompleteObjective("Place cut piece on the table");
        }
        else if(heldItem.Contains("Caliper"))
        {
            Debug.Log("Switching to caliper camera");
            CameraController.Instance.SwitchToCamera(4);
            caliperController.ToggleCaliperAttachment();
        }
        else
        {
            Debug.Log("Couldn't place or switch camera");
        }
    }
}
