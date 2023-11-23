using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private List<string> inventory = new();

    public void AddItem(string itemID)
    {
        Debug.Log("Picked item " + itemID);
        inventory.Add(itemID);
    }

    public bool HasItem(string itemID)
    {
        return inventory.Contains(itemID);
    }

    public void RemoveItem(string itemID)
    {
        Debug.Log("Removed item " + itemID);
        inventory.Remove(itemID);
    }
}
