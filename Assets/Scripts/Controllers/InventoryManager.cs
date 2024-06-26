using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    private TextInformation textInfo;
    public GameObject heldItem;
    public Transform inventoryAttachmentPoint;
    // Variable to check if hands are full
    public bool handsFull = false;

    private List<string> inventory = new(); //TODO: Does this need to be list if only 1 item can be held at a time?

    private void Start()
    {
        textInfo = FindObjectOfType<TextInformation>();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItemToInventory(string itemID, string itemInfoText = "", GameObject item = null)
    {
        if (item != null)
        {
            heldItem = item;
            heldItem.transform.SetPositionAndRotation(inventoryAttachmentPoint.position, inventoryAttachmentPoint.rotation);
            heldItem.transform.parent = inventoryAttachmentPoint;

            // Set the heldItem hidden for now
            heldItem.SetActive(false);
        }

        Debug.Log("Picked item " + itemID);
        handsFull = true;
        textInfo.UpdateText(itemInfoText);
        inventory.Add(itemID);
    }

    public bool HasItem(string itemID)
    {
        return inventory.Contains(itemID);
    }

    public void RemoveItem(string itemID, string itemInfoText = "", Transform transform = null)
    {
        if (transform != null)
        {
            MoveHeldItemToAttachmentPoint(transform);
        }

        Debug.Log("Removed item " + itemID);
        handsFull = false;
        heldItem = null;
        textInfo.UpdateText(itemInfoText);
        inventory.Remove(itemID);
    }

    public string GetHeldItemID()
    {
        if (inventory.Count > 0)
        {
            return inventory[^1];
        }
        return "";
    }

    // Method to move heldItem to provided attachement point
    public void MoveHeldItemToAttachmentPoint(Transform attachmentPoint)
    {
        heldItem.transform.SetPositionAndRotation(attachmentPoint.position, attachmentPoint.rotation);
        heldItem.transform.parent = attachmentPoint;

        // Set the heldItem to active to make sure it is visible
        heldItem.SetActive(true);
    }

    // TODO: Is this needed or just check the handsFull variable?
    public bool CheckIfHandsFull()
    {
        return handsFull;
    }
}
