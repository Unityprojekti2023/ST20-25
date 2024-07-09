using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    private TextInformation textInfo;
    public GameObject heldItem;
    public TextMeshProUGUI secondaryInteractionText;
    public Transform inventoryAttachmentPoint;
    // Variable to check if hands are full
    public bool handsFull = false;

    private List<string> inventory = new(); //TODO: Does this need to be list if only 1 item can be held at a time?

    private void Start()
    {
        textInfo = FindObjectOfType<TextInformation>();
        if (textInfo == null)
        {
            Debug.LogError("TextInformation script not found.");
        }
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

    public void AddItemToInventory(string itemID, string itemInfoText)
    {
        AddItemToInventory(itemID, itemInfoText, null, "");
    }

    public void AddItemToInventory(string itemID, string itemInfoText, GameObject item)
    {
        AddItemToInventory(itemID, itemInfoText, item, "");
    }

    public void AddItemToInventory(string itemID, string itemInfoText, string secondaryInteractionText)
    {
        AddItemToInventory(itemID, itemInfoText, null, secondaryInteractionText);
    }

    public void AddItemToInventory(string itemID, string itemInfoText = "", GameObject item = null, string secondaryInteractionText = "")
    {
        if (!handsFull)
        {
            if (item != null)
            {
                heldItem = item;
                MoveHeldItemToAttachmentPoint(inventoryAttachmentPoint);

                // Set the heldItem hidden for now
                //heldItem.SetActive(false);
            }
            if (secondaryInteractionText != "")
            {
                this.secondaryInteractionText.text = secondaryInteractionText;
            }

            Debug.Log("Picked item " + itemID);
            handsFull = true;
            textInfo.UpdateText(itemInfoText);
            inventory.Add(itemID);
        }
        else
            textInfo.UpdateText("Hands are full");
    }

    public void RemoveItemFromInventory(string itemID, string itemInfoText = "", Transform transform = null)
    {
        if (handsFull)
        {
            if (transform != null)
            {
                MoveHeldItemToAttachmentPoint(transform);
            }

            if (secondaryInteractionText.text != "")
            {
                secondaryInteractionText.text = "";
            }

            Debug.Log("Removed item " + itemID);
            handsFull = false;
            heldItem = null;
            textInfo.UpdateText(itemInfoText);
            inventory.Remove(itemID);
        }
        else
        {
            textInfo.UpdateText("Hands are empty");
            Debug.Log("Trying to remove item from empty hands, but hands are empty.");
        }
    }

    public bool IsItemInInventory(string itemID)
    {
        return inventory.Contains(itemID);
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
        heldItem.transform.parent = attachmentPoint;
        heldItem.transform.localPosition = Vector3.zero;

        heldItem.transform.localRotation = Quaternion.identity;

        // Set the heldItem to active to make sure it is visible
        heldItem.SetActive(true);
    }
}
