using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    private TextInformation textInfo;
    public GameObject heldItem;
    // Variable to check if hands are full
    public bool handsFull = false;

    private List<string> inventory = new();

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

    public void AddItemToInventory(string itemID, string itemInfoText = "")
    {
        Debug.Log("Picked item " + itemID);
        handsFull = true;
        textInfo.UpdateText(itemInfoText);
        inventory.Add(itemID);
    }

    public bool HasItem(string itemID)
    {
        return inventory.Contains(itemID);
    }

    public void RemoveItem(string itemID, string itemInfoText = "")
    {
        Debug.Log("Removed item " + itemID);
        handsFull = false;
        textInfo.UpdateText(itemInfoText);
        inventory.Remove(itemID);
    }

    public string HeldItemID()
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
    }

    public bool CheckIfHandsFull()
    {
        return handsFull;
    }
}
