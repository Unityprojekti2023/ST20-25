using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private TextInformation textInfo;
    // Variable to check if hands are full
    public bool handsFull = false;

    private List<string> inventory = new();

    private void Start()
    {
        textInfo = FindObjectOfType<TextInformation>();
    }

    public void AddItem(string itemID, string itemInfoText = "")
    {
        Debug.Log("Picked item " + itemID);
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
        textInfo.UpdateText(itemInfoText);
        inventory.Remove(itemID);
    }

    public bool AreHandsFull()
    {
        return handsFull;
    }
}
